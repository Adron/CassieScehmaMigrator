using System;
using System.Net;
using Cassandra;

namespace CassieCoreLib
{
    public class DestinationDatabase
    {
        public DestinationDatabase(string address)
        {
            var ip = IPAddress.Parse(address);
            CaSMaInception(ip);
        }
        
        public IPAddress DatabaseAddress { get; set; }
        
        public bool VerifyConnection()
        {
            return false;
        }

        private void CaSMaInception(IPAddress address)
        {
            DatabaseAddress = address;
            RunCreateScripts("create keyspace if not exists casma_history with replication = { 'class':'SimpleStrategy', 'replication_factor':1};");
            RunCreateScripts($"create table history(stamp timestamp,step text primary key,details text,success boolean);");
        }

        private void RunCreateScripts(string command)
        {
            try
            {
                var cluster = Cluster.Builder()
                    .AddContactPoints(DatabaseAddress)
                    .Build();

                var session = cluster.Connect();
                session.Execute(command);
            }
            catch (NoHostAvailableException noHostError)
            {
                Console.WriteLine("Check the IP to ensure it is correct and in the pool of nodes in the cluster.");
                Console.WriteLine(noHostError);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}