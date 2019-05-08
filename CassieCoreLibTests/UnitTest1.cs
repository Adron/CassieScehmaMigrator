using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CassieCoreLibTests
{
    public class UnitTest1
    {
        public class FileSelection
        {
            private string configurationPath = "/this/path";
            private string operationPath;
            
            public FileSelection(string pathForSchema)
            {
                if (Directory.Exists(pathForSchema))
                {
                    operationPath = pathForSchema;
                }
                else
                {
                    pathSelector();
                }
            }

            public FileSelection()
            {
                pathSelector();
            }

            private void pathSelector()
            {
                // TODO: Check for configuration value.
                
                // TODO: If no configuration value, choose default of path where executing.
                operationPath = Directory.GetCurrentDirectory();
            }

            public string OperationsPath()
            {
                return operationPath;
            }
        }

        [Fact]
        public void verify_selected_path_available()
        {
            // Read path from config, if not available, default to path of current execution call.

            var configurationPath = "/this/does/not/exist";
            var fileSelection = new FileSelection(configurationPath);
            Assert.Equal(fileSelection.OperationsPath(), Directory.GetCurrentDirectory());
        }

    }
}