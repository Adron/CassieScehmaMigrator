using System;
using System.IO;
using CassieCoreLib;
using Xunit;

namespace CassieCoreLibTests
{
    public class CassieCoreLibTests
    {
        [Fact]
        public void verify_correct_number_tasks_derived_from_files()
        {
            var testHelper = new TestHelpers();
            var migrationPath = testHelper.SetupMigrationsForTests(true, Guid.NewGuid());
            var result = new FileSelection(migrationPath).GetFiles();
            Assert.Equal(9, result.Count);
            testHelper.TearDownMigrationsTested(migrationPath);
        }

        [Fact]
        public void verify_migration_files_do_not_exist()
        {
            var testHelper = new TestHelpers();
            var migrationPath = testHelper.SetupMigrationsForTests(false, Guid.NewGuid());
            Assert.NotEmpty(migrationPath);
            testHelper.TearDownMigrationsTested(migrationPath);
        }

        [Fact]
        public void verify_migration_files_exist()
        {
            var testHelper = new TestHelpers();
            var migrationPath = testHelper.SetupMigrationsForTests(true, Guid.NewGuid());
            var fileSystem = new FileSelection(migrationPath);
            Assert.True(fileSystem.ProspectiveMigrationsExist());
            testHelper.TearDownMigrationsTested(migrationPath);
        }

        [Fact]
        public void verify_migration_path_list()
        {
            var testHelper = new TestHelpers();
            var migrationPath = testHelper.SetupMigrationsForTests(true, Guid.NewGuid());
            var fileSystem = new FileSelection(migrationPath);
            var result = fileSystem.ShowMigrationPath();
            Assert.True(result.Ready());
            testHelper.TearDownMigrationsTested(migrationPath);
        }

        [Fact]
        public void verify_path_avialable()
        {
            var fileSystem = new FileSelection();
            Assert.Equal(fileSystem.OperationsPath(), Directory.GetCurrentDirectory());
        }

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
    }
}