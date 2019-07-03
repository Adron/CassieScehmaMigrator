using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CassieCoreLibTests
{
    public class TestHelpers
    {
        public static List<string> CreateThese { get; set; }

        public static void TearDownMigrationsTested(string migrationPath)
        {
            Directory.Delete(migrationPath, true);
        }

        public static string SetupMigrationsForTests(bool settingUp, Guid migrationIdentifier)
        {  
            CreateThese = new List<string>();
            var migrationForCassie = $"migration-{migrationIdentifier}-for-cassie";
            
            Directory.CreateDirectory(migrationForCassie);
            var directory = new DirectoryInfo(migrationForCassie);

            if (!settingUp) return migrationForCassie;
            
            TestStationMigrationContents.CreateFilesAndContentsForIntegrationTest(directory);
            
            return migrationForCassie;
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
        
         public static void CreateFilesAndContentsForIntegrationTest(DirectoryInfo directory)
        {
            var migrationUpValid1 = Path.Combine(directory.FullName, "01 05 2001.doing-whatever.up.cql");
            var migrationUpValid2 = Path.Combine(directory.FullName, "01 01 2003.doing-stuff.up.cql");
            var migrationUpValid3 = Path.Combine(directory.FullName, "01 08 2006.doing-something.up.cql");
            var migrationUpValid4 = Path.Combine(directory.FullName, "march 01 2009.doing-other-bits.up.cql");
            var migrationUpValid5 = Path.Combine(directory.FullName, "01 02 2010.doing-more-stuff.up.cql");
            var migrationDownValid1 = Path.Combine(directory.FullName, "4 21 1902.doing-stuff.down.cql");
            var migrationDownValid2 = Path.Combine(directory.FullName, "12 12 2016.doing-more-stuff.down.cql");
            var migrationDownValid3 = Path.Combine(directory.FullName, "january 08 2006.doing-something.down.cql");
            var migrationDownValid4 = Path.Combine(directory.FullName, "may 01 2006.doing-other-bits.down.cql");
            var migrationNoGo1 = Path.Combine(directory.FullName, "thisfile.wouldnt.be.processed.but.here.we.are.txt");
            var migrationNoGo2 = Path.Combine(directory.FullName, "01 05 2000.not-a-file-to-process.up.sql");
            var migrationNoGo3 = Path.Combine(directory.FullName, "01 01 2001.thisthing.nope.cql");

            WriteFilesAndContents(migrationUpValid1, migrationUpValid2, migrationUpValid3, migrationUpValid4, migrationUpValid5,
                migrationDownValid1, migrationDownValid2, migrationDownValid3, migrationDownValid4, migrationNoGo1,
                migrationNoGo2, migrationNoGo3);
        }

        private static void WriteFilesAndContents(string migrationUpValid1, string migrationUpValid2, string migrationUpValid3,
            string migrationUpValid4, string migrationUpValid5, string migrationDownValid1, string migrationDownValid2,
            string migrationDownValid3, string migrationDownValid4, string migrationNoGo1, string migrationNoGo2,
            string migrationNoGo3)
        {
            File.WriteAllText(migrationUpValid1, MigrationUpValid1Cql);
            File.WriteAllText(migrationUpValid2, MigrationUpValid2Cql);
            File.WriteAllText(migrationUpValid3, MigrationUpValid3Cql);
            File.WriteAllText(migrationUpValid4, MigrationUpValid4Cql);
            File.WriteAllText(migrationUpValid5, MigrationUpValid5Cql);

            File.WriteAllText(migrationDownValid1, MigrationDownValid1Cql);
            File.WriteAllText(migrationDownValid2, MigrationDownValid2Cql);
            File.WriteAllText(migrationDownValid3, MigrationDownValid3Cql);
            File.WriteAllText(migrationDownValid4, MigrationDownValid4Cql);

            File.WriteAllText(migrationNoGo1, "Nothing to see here.");
            File.WriteAllText(migrationNoGo2, "Still nothing to see here either.");
            File.WriteAllText(migrationNoGo3, "This file should be skipped.");
        }
    }
}