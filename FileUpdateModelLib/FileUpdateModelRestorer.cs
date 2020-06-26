using System;
using System.IO;

namespace FileUpdateModelLib
{
    public class FileUpdateModelRestorer
    {
        private readonly string _destination;

        public FileUpdateModelRestorer(string destination)
        {
            if (!Directory.Exists(destination))
                throw new Exception("Destination directory does not exist!");

            _destination = destination;
            BackupDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        public string BackupDirectory { get; }

        public void Backup()
        {
            Directory.CreateDirectory(BackupDirectory);
            DirectoryInfo directory = new DirectoryInfo(_destination);

            try
            {
                foreach (var file in directory.GetFiles())
                {
                    file.CopyTo(Path.Combine(BackupDirectory, file.Name));
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Destination folder not found!");
            }
            catch (Exception)
            {
                throw new Exception("Not able to create backup files!");
            }
        }

        public void ClearBackup()
        {
            try
            {
                Directory.Delete(BackupDirectory, true);
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("No backup found!");
            }
            catch (Exception)
            {
                throw new Exception("Not able to clear backup!");
            }
        }

        public void Rollback()
        {
            ClearDestination();
            RestoreFiles();
        }

        private void ClearDestination()
        {
            DirectoryInfo directory = new DirectoryInfo(_destination);

            try
            {
                foreach (var file in directory.GetFiles())
                {
                    file.Delete();
                }

                foreach (var dir in directory.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Destination folder not found!");
            }
            catch (Exception)
            {
                throw new Exception("Not able to clear destination folder!");
            }
        }

        private void RestoreFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(BackupDirectory);

            try
            {
                foreach (var file in directory.GetFiles())
                {
                    file.CopyTo(Path.Combine(_destination, file.Name));
                }
            }
            catch (Exception)
            {
                throw new Exception("Not able to restore files!");
            }
        }
    }
}
