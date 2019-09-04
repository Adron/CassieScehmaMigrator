using System;
using System.Net;
using Cassandra;

namespace CassieCoreLib
{
    public class DestinationDatabase
    {
        public string KeyspaceCasmaHistory = "casma_history";
        
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
                    "insert into casma_history.history (step, details, stamp, success) values ('conn_validate','Verifying the connection to the database cluster.', toTimestamp(now()), true)", KeyspaceCasmaHistory);
                connectionStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                RunCassieQueryScript("insert into casma_history.history (step, details, stamp, success) values ('conn_validated','Verified the connection to the database cluster.', toTimestamp(now()), " + 
                                     connectionStatus + ")",KeyspaceCasmaHistory);
            }
            return connectionStatus;
        }

        private void CaSMaInception(IPAddress address)
        {
            DatabaseAddress = address;
            RunCassieQueryScript("create keyspace if not exists casma_history with replication = {'class':'SimpleStrategy','replication_factor':1};", "");
            RunCassieQueryScript($"create table if not exists history (step text,details text,stamp timestamp,success boolean,primary key ((success), step, details, stamp));", KeyspaceCasmaHistory);
            
        }
        
        public Boolean RunCassieQueryScript(string command, string keyspace)
        {
            try
            {
                var cluster = Cluster.Builder()
                    .AddContactPoints(DatabaseAddress)
                    .Build();

                var session = string.IsNullOrWhiteSpace(keyspace) ? cluster.Connect() : cluster.Connect(keyspace);
                var result = session.Execute(command);
                Console.WriteLine(result.ToString());
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
            
            return true;
        }
    }
}