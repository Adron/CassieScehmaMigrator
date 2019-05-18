using System;
using System.Collections.Generic;
using System.IO;

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

        public MigrationPath(List<SchemaMigrationTask> migrationTasks)
        {
            foreach (var task in migrationTasks)
            {
                // TODO: Build the tasks and get the CQL to process/execute.
            }

            _ready = true;
        }

        public bool Ready()
        {
            return _ready;
        }
    }
}