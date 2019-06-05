using System;
using System.Collections.Generic;
using System.IO;

namespace CassieCoreLibTests
{
    public class TestHelpers
    {
        public TestHelpers()
        {
            CreateThese = new List<string>();
        }

        public List<string> CreateThese { get; }

        public void TearDownMigrationsTested(string migrationPath)
        {
            Directory.Delete(migrationPath, true);
        }

        public string SetupMigrationsForTests(bool settingUp, Guid migrationIdentifier)
        {
            var migrationPath = "migration-" + migrationIdentifier;

            Directory.CreateDirectory(migrationPath);
            DirectoryInfo directory = new DirectoryInfo(migrationPath);

            if (settingUp)
            {
                var migrationUpValid1 = Path.Combine(directory.FullName, "01 01 2003.doing-stuff.up.cql");
                var migrationUpValid2 = Path.Combine(directory.FullName, "01 02 2010.doing-more-stuff.up.cql");
                var migrationUpValid3 = Path.Combine(directory.FullName, "01 05 2001.doing-whatever.up.cql");
                var migrationUpValid4 = Path.Combine(directory.FullName, "01 08 2006.doing-something.up.cql");
                var migrationUpValid5 = Path.Combine(directory.FullName, "march 01 2009.doing-other-bits.up.cql");
                var migrationDownValid1 = Path.Combine(directory.FullName, "4 21 1902.doing-stuff.down.cql");
                var migrationDownValid2 = Path.Combine(directory.FullName, "12 12 2016.doing-more-stuff.down.cql");
                var migrationDownValid3 = Path.Combine(directory.FullName, "january 08 2006.doing-something.down.cql");
                var migrationDownValid4 = Path.Combine(directory.FullName, "may 01 2006.doing-other-bits.down.cql");
                var migrationNoGo1 = Path.Combine(directory.FullName, "thisfile.wouldnt.be.processed.but.here.we.are.txt");
                var migrationNoGo2 = Path.Combine(directory.FullName, "01 05 2000.not-a-file-to-process.up.sql");
                var migrationNoGo3 = Path.Combine(directory.FullName, "01 01 2001.thisthing.nope.cql");
                
                File.WriteAllText(migrationUpValid1, TestStationMigrationContents.MigrationUpValid1Cql);
                File.WriteAllText(migrationUpValid2, TestStationMigrationContents.MigrationUpValid2Cql);
                File.WriteAllText(migrationUpValid3, TestStationMigrationContents.MigrationUpValid3Cql);
                File.WriteAllText(migrationUpValid4, TestStationMigrationContents.MigrationUpValid4Cql);
                File.WriteAllText(migrationUpValid5, TestStationMigrationContents.MigrationUpValid5Cql);
                
                File.WriteAllText(migrationDownValid1, TestStationMigrationContents.MigrationDownValid1Cql);
                File.WriteAllText(migrationDownValid2, TestStationMigrationContents.MigrationDownValid2Cql);
                File.WriteAllText(migrationDownValid3, TestStationMigrationContents.MigrationDownValid3Cql);
                File.WriteAllText(migrationDownValid4, TestStationMigrationContents.MigrationDownValid4Cql);
                
                File.WriteAllText(migrationNoGo1, "Nothing to see here.");
                File.WriteAllText(migrationNoGo2, "Still nothing to see here either.");
                File.WriteAllText(migrationNoGo3, "This file should be skipped.");
            }

            return migrationPath;
        }
    }

    public static class TestStationMigrationContents
    {
        public static readonly string MigrationUpValid1Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationUpValid2Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationUpValid3Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationUpValid4Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationUpValid5Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationDownValid1Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationDownValid2Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationDownValid3Cql = "SELECT * FROM cheese;";
        public static readonly string MigrationDownValid4Cql = "SELECT * FROM cheese;";
        
    }
}