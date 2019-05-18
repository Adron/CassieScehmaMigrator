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
                CreateThese.AddRange(new[]
                {
                    Path.Combine(directory.FullName, "01 01 2003.doing-stuff.up.cql"),
                    Path.Combine(directory.FullName, "01 02 2010.doing-more-stuff.up.cql"),
                    Path.Combine(directory.FullName, "01 05 2001.doing-whatever.up.cql"),
                    Path.Combine(directory.FullName, "01 08 2006.doing-something.up.cql"),
                    Path.Combine(directory.FullName, "march 01 2009.doing-other-bits.up.cql"),
                    Path.Combine(directory.FullName, "4 21 1902.doing-stuff.down.cql"),
                    Path.Combine(directory.FullName, "12 12 2016.doing-more-stuff.down.cql"),
                    Path.Combine(directory.FullName, "january 08 2006.doing-something.down.cql"),
                    Path.Combine(directory.FullName, "may 01 2006.doing-other-bits.down.cql"),
                    Path.Combine(directory.FullName, "thisfile.wouldnt.be.processed.but.here.we.are.txt"),
                    Path.Combine(directory.FullName, "01 05 2000.not-a-file-to-process.up.sql"),
                    Path.Combine(directory.FullName, "01 01 2001.thisthing.nope.cql")
                });
                foreach (var fileName in CreateThese)
                {
                    File.Create(fileName);
                }
            }

            return migrationPath;
        }
    }
}