using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CassieCoreLib
{
    public enum Migration
    {
        Up,
        Down,
        Invalid
    }

    public class SchemaMigrationTask
    {
        public SchemaMigrationTask(FileInfo taskFile, string cql, Migration migrationType, DateTime orderDate)
        {
            TaskFile = taskFile;
            Cql = cql;
            MigrationType = migrationType;
            OrderDate = orderDate;
        }

        public FileInfo TaskFile { get; set; }
        public string Cql { get; set; }
        public Migration MigrationType { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class MigrationPath 
    {
        private bool _ready;
        private List<SchemaMigrationTask> migrations;
        public MigrationPath(List<SchemaMigrationTask> migrationTasks)
        {
            migrations = new List<SchemaMigrationTask>();
            foreach (var task in migrationTasks)
            {
                migrations.Add(task);
            }

            _ready = true;
        }

        public bool Ready()
        {
            return _ready;
        }

        public bool Migrate(Migration upDown)
        {
            var db = new DestinationDatabase("127.0.0.1");
            try
            {
                foreach (var migration in Path(upDown))
                {
                    db.RunCassieQueryScript(migration.Cql, "");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return true;
        }

        public List<SchemaMigrationTask> Path(Migration upDown)
        {
            var migrationPath = new List<SchemaMigrationTask>();
            
            foreach (var migration in migrations){
                if (migration.MigrationType == upDown)
                {
                    migrationPath.Add(migration);
                }
            }
            
            return migrationPath.OrderBy(o => o.OrderDate).ToList();
        }
    }
}
