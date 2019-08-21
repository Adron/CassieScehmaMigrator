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
            var connectionStatus = false;
            
            try
            {
                RunCassieQueryScript(
                    "insert into casma_history.history (step, details, stamp, success) values ('conn_validate','Verifying the connection to the database cluster.', toTimestamp(now()), true)");
                RunCassieQueryScript(
                    "SELECT step FROM casma_history.history WHERE step='conn_validate' LIMIT 1 ALLOW FILTERING");
                connectionStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                RunCassieQueryScript("insert into casma_history.history (step, details, stamp, success) values ('conn_validated','Verified the connection to the database cluster.', toTimestamp(now()), " + 
                                     connectionStatus + ")");
            }
            return connectionStatus;
        }

        private void CaSMaInception(IPAddress address)
        {
            DatabaseAddress = address;
            RunCassieQueryScript("create keyspace if not exists casma_history with replication = {'class':'SimpleStrategy','replication_factor':1};");
            RunCassieQueryScript($"create table casma_history.history(stamp timestamp,step text primary key,details text,success boolean);");
        }

        private Boolean RunCassieQueryScript(string command)
        {
            var success = false;
            
            try
            {
                var cluster = Cluster.Builder()
                    .AddContactPoints(DatabaseAddress)
                    .Build();

                var session = cluster.Connect();
                var result = session.Execute(command);
                Console.WriteLine(result.ToString());
                success = true;
            }
            catch (NoHostAvailableException noHostError)
            {
                Console.WriteLine("Check the IP to ensure it is correct and in the pool of nodes in the cluster.");
                Console.WriteLine(noHostError);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return success;
        }
    }
}