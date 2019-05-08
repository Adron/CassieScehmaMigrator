using System.Collections.Generic;
using System.IO;
using CassieCoreLib;
using Xunit;

namespace CassieCoreLibTests
{
    public class CassieCoreLibTests
    {
        [Fact]
        public void verify_selected_path_available()
        {
            // Read path from config, if not available, default to path of current execution call.
            var configurationPath = "/this/does/not/exist";
            var fileSelection = new FileSelection(configurationPath);
            Assert.Equal(fileSelection.OperationsPath(), Directory.GetCurrentDirectory());
        }

        [Fact]
        public void verify_selected_path_available_and_exists()
        {
            var newPath = "newpath";
            Directory.CreateDirectory(newPath);
            var fileSelection = new FileSelection(newPath);
            Assert.Equal(fileSelection.OperationsPath(), newPath);
            Directory.Delete(newPath);
        }

        [Fact]
        public void verify_path_avialable()
        {
            var fileSystem = new FileSelection();
            Assert.Equal(fileSystem.OperationsPath(), Directory.GetCurrentDirectory());
        }

        [Fact]
        public void verify_migration_files_exist()
        {
            var migrationPath = testHelpers.setupMigrationsForTests(out var createThese);

            var fileSystem = new FileSelection(migrationPath);
            Assert.True(fileSystem.MigrationsExist());
                
            testHelpers.tearDownMigrationsTested(createThese, migrationPath);
        }

        [Fact]
        public void verify_migration_files_do_not_exist()
        {
            var migrationPath = testHelpers.setupMigrationsForTests(out var createThese);
            
            Assert.Null(migrationPath);
            
            testHelpers.tearDownMigrationsTested(createThese, migrationPath);
        }

        [Fact]
        public void verify_list_of_file_to_process()
        {
            var migrationPath = testHelpers.setupMigrationsForTests(out var createThese);
            
            Assert.Null(migrationPath);
            
            testHelpers.tearDownMigrationsTested(createThese, migrationPath);
        }
    }
}