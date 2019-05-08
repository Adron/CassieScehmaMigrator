using System.Collections.Generic;
using System.IO;

namespace CassieCoreLibTests
{
    public static class testHelpers
    {
        public static void tearDownMigrationsTested(List<string> createThese, string migrationPath)
        {
            foreach (var fileName in createThese)
            {
                File.Delete(fileName);
            }

            Directory.Delete(migrationPath);
        }

        public static string setupMigrationsForTests(out List<string> createThese)
        {
            var migrationPath = "migrations";
            Directory.CreateDirectory(migrationPath);
            DirectoryInfo directory = new DirectoryInfo(migrationPath);

            createThese = new List<string>();
            createThese.AddRange(new[]
            {
                Path.Combine(directory.FullName, "01 01 2003-doing-stuff.up.cql"),
                Path.Combine(directory.FullName, "01 02 2010-doing-more-stuff.up.cql"),
                Path.Combine(directory.FullName, "01 05 2001-doing-whatever.up.cql"),
                Path.Combine(directory.FullName, "01 08 2006-doing-something.up.cql"),
                Path.Combine(directory.FullName, "march 01 2009-doing-other-bits.up.cql"),
                Path.Combine(directory.FullName, "4 21 1902-doing-stuff.down.cql"),
                Path.Combine(directory.FullName, "12 12 2016-doing-more-stuff.down.cql"),
                Path.Combine(directory.FullName, "january 08 2006-doing-something.down.cql"),
                Path.Combine(directory.FullName, "may 01 2006-doing-other-bits.down.cql")
            });
            foreach (var fileName in createThese)
            {
                File.Create(fileName);
            }

            return migrationPath;
        }
    }
}