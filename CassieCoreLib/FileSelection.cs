using System;
using System.Collections.Generic;
using System.IO;

namespace CassieCoreLib
{
    public class FileSelection
    {
        private string _operationPath;

        public FileSelection(string pathForSchema)
        {
            if (Directory.Exists(pathForSchema))
            {
                _operationPath = pathForSchema;
            }
            else
            {
                PathSelector();
            }
        }

        public FileSelection()
        {
            PathSelector();
        }

        public List<SchemaMigrationTask> GetFiles()
        {
            var files = new List<SchemaMigrationTask>();

            DirectoryInfo directory = new DirectoryInfo(_operationPath);
            FileInfo[] filesToCheck = directory.GetFiles();

            for (var i = 0; i < filesToCheck.Length; i++)
            {
                var fileToCheck = filesToCheck[i];
                var fileName = fileToCheck.Name;
                var fileNameParts = fileName.Split(".");
                if (fileNameParts.Length > 3)
                {
                    DateTime dateOfMigration;
                    bool parsedYa = DateTime.TryParse(fileNameParts[0], out dateOfMigration);
                    var upDown = Enum.TryParse(fileNameParts[fileNameParts.Length - 2], out Migration migration);

                    if (parsedYa
                        && dateOfMigration < DateTime.Now
                        && fileToCheck.Extension == ".cql"
                        && upDown)
                    {
                        var migrationTask = new SchemaMigrationTask(
                            fileToCheck,
                            File.ReadAllText(fileToCheck.FullName),
                            migration,
                            dateOfMigration);

                        files.Add(migrationTask);
                    }
                }
            }

            return files;
        }

        private void PathSelector()
        {
            _operationPath = Directory.GetCurrentDirectory();
        }

        public string OperationsPath()
        {
            return _operationPath;
        }

        public bool ProspectiveMigrationsExist()
        {
            var files = Directory.GetFiles(_operationPath);
            return files.Length > 0;
        }

        public MigrationPath ShowMigrationPath()
        {
            var files = GetFiles();
            return new MigrationPath(files);
        }
    }
}