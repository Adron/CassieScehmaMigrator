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
                pathSelector();
            }
        }

        public FileSelection()
        {
            pathSelector();
        }

        private void pathSelector()
        {
            _operationPath = Directory.GetCurrentDirectory();
        }

        public string OperationsPath()
        {
            return _operationPath;
        }

        public bool MigrationsExist()
        {
            var files = Directory.GetFiles(_operationPath);
            return files.Length > 0;
        }
    }
}