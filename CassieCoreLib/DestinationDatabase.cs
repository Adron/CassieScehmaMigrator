using System;
using System.Net;
using Cassandra;

namespace CassieCoreLib
{
    public class DestinationDatabase
    {
        public DestinationDatabase(string address)
        {
            Construct(IPAddress.Parse(address));
        }
        
        public DestinationDatabase(IPAddress address)
        {
            Construct(address);
        }

        private void Construct(IPAddress address)
        {
            DatabaseAddress = address;
        }

        public IPAddress DatabaseAddress { get; set; }
        
        public bool VerifyConnection()
        {
            try
            {
                var cluster = Cluster.Builder()
                    .AddContactPoints(DatabaseAddress)
                    .Build();

                var session = cluster.Connect();

                var command =
                    "create keyspace if not exists casma_verification with replication { 'class':'SimpleStrategy', 'replication_factor':1};";

                var rs = session.Execute(command);
                return true;
            }
            catch (NoHostAvailableException noHostError)
            {
                Console.WriteLine("Check the IP to ensure it is correct and in the pool of nodes in the cluster.");
                Console.WriteLine(noHostError);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}