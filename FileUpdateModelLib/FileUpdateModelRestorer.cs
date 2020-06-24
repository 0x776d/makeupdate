using System.IO;

namespace FileUpdateModelLib
{
    public class FileUpdateModelRestorer
    {
        private readonly string _tempBackupDirectory;
        private FileUpdateModelConfig _fileUpdateConfig;

        public FileUpdateModelRestorer(FileUpdateModelConfig config)
        {
            _fileUpdateConfig = config;
            _tempBackupDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_tempBackupDirectory);
        }

        public void Backup()
        {
            DirectoryInfo directory = new DirectoryInfo(_fileUpdateConfig.Destination);

            foreach (var file in directory.GetFiles())
            {
                file.CopyTo(Path.Combine(_tempBackupDirectory, file.Name));
            }
        }

        public void ClearBackup()
        {
            Directory.Delete(_tempBackupDirectory, true);
        }

        public void Rollback()
        {
            ClearDestination();
            RestoreFiles();
        }

        private void ClearDestination()
        {
            DirectoryInfo directory = new DirectoryInfo(_fileUpdateConfig.Destination);

            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (var dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void RestoreFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(_tempBackupDirectory);

            foreach (var file in directory.GetFiles())
            {
                file.CopyTo(Path.Combine(_fileUpdateConfig.Destination, file.Name));
            }
        }
    }
}
