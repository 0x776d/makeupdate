using System;
using System.IO;

namespace FileUpdateModelLib
{
    public class FileUpdateModelUpdater
    {
        private readonly string _source;
        private readonly string _destination;

        public FileUpdateModelUpdater(string destination, string source)
        {
            if (!Directory.Exists(destination))
                throw new Exception("Destination directory does not exist!");

            if (!Directory.Exists(source))
                throw new Exception("Source directory does not exist!");

            _destination = destination;
            _source = source;
        }

        public void ClearDestination()
        {
            DirectoryInfo directory = new DirectoryInfo(_destination);

            try
            {
                foreach (var file in directory.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                        throw new Exception("File cannot be deleted because it is used by another process!");
                    }
                }

                foreach (var dir in directory.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Directory cannot be deleted because there are files in it which are used by another process!");
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Destination folder not found!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadSourceToDestination()
        {
            try
            {
                DirectoryInfo directorySource = new DirectoryInfo(_source);

                foreach (var file in directorySource.GetFiles())
                {
                    file.CopyTo(Path.Combine(_destination, file.Name));
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Source folder not found!");
            }
            catch (Exception)
            {
                throw new Exception("Loading source files to destination failed!");
            }
        }
    }
}
