using System;
using System.Net;
using Dse;

namespace CassieCoreLib
{
    public class DestinationDatabase
    {
        private string _keyspaceCasmaHistory = "casma_history";
        public bool IsApolloDatabase = false;
        
        public DestinationDatabase(string address)
        {
            var ip = IPAddress.Parse(address);
            DatabaseCluster = GetCluster(ip);
            CaSMaInception();
        }

        public DestinationDatabase(ApolloDb apolloDb)
        {
            IsApolloDatabase = true;
            _keyspaceCasmaHistory = apolloDb.Keyspace;
            DatabaseCluster = GetCluster(apolloDb);
            CaSMaInception();
        }

        private Cluster DatabaseCluster { get; set; }

        public bool VerifyConnection()
        {
            var connectionStatus = false;
            
            try
            {
                RunCassieQueryScript(
                    "insert into " + _keyspaceCasmaHistory + ".history (step, details, stamp, success) values ('conn_validate','Verifying the connection to the database cluster.', toTimestamp(now()), true) if not exists;", _keyspaceCasmaHistory);
                connectionStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                RunCassieQueryScript("insert into " + _keyspaceCasmaHistory + ".history (step, details, stamp, success) values ('conn_validated','Verified the connection to the database cluster.', toTimestamp(now()), " + 
                                     connectionStatus + 
                                     ") if not exists;",_keyspaceCasmaHistory);
            }
            return connectionStatus;
        }

        private void CaSMaInception()
        {
            if (!IsApolloDatabase)
            {
                RunCassieQueryScript("create keyspace if not exists casma_history with replication = {'class':'SimpleStrategy','replication_factor':1};", "");
            }
            RunCassieQueryScript($"create table if not exists history (step text,details text,stamp timestamp,success boolean,primary key ((success), step, details, stamp));", _keyspaceCasmaHistory);
        }
        
        public Boolean RunCassieQueryScript(string command, string keyspace)
        {
            try
            {
                var session = string.IsNullOrWhiteSpace(keyspace) ? DatabaseCluster.Connect() : DatabaseCluster.Connect(keyspace);
                var statement = new SimpleStatement(command)
                    .SetConsistencyLevel(ConsistencyLevel.Quorum)
                    .SetRetryPolicy(new DefaultRetryPolicy())
                    .SetPageSize(100);
                
                var result = session.Execute(statement);
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

        private Cluster GetCluster(IPAddress ip)
        {
            var cluster = Cluster.Builder()
                .AddContactPoints(ip)
                .Build();
            return cluster;
        }

        private Cluster GetCluster(ApolloDb apolloDb)
        {
            var cluster = Cluster.Builder()
                .WithCloudSecureConnectionBundle(apolloDb.SecurityBundleFilePath)
                .WithCredentials(apolloDb.Username, apolloDb.Password)
                .Build();
            return cluster;
        }
    }
}