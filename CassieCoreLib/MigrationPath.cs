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
            CQL = cql;
            MigrationType = migrationType;
            OrderDate = orderDate;
        }

        public FileInfo TaskFile { get; set; }
        public string CQL { get; set; }
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
            var path = Path(upDown);
            
            // Connect to database.
            // Run CQL for all migrations.
            // Finish.
            
            
            
            
            return false;
        }

        public List<SchemaMigrationTask> Path(Migration upDown)
        {
            List<SchemaMigrationTask> migrationPath = new List<SchemaMigrationTask>();
            
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