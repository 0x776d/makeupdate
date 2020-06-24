using System.IO;

namespace FileUpdateModelLib
{
    public class FileUpdateModelUpdater
    {
        private FileUpdateModelConfig _fileUpdateConfig;

        public FileUpdateModelUpdater(FileUpdateModelConfig config)
        {
            _fileUpdateConfig = config;
        }

        public void ClearDestination()
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

        public void LoadSourceToDestination()
        {
            DirectoryInfo directorySource = new DirectoryInfo(_fileUpdateConfig.Source);

            foreach (var file in directorySource.GetFiles())
            {
                file.CopyTo(Path.Combine(_fileUpdateConfig.Destination, file.Name));
            }
        }
    }
}
