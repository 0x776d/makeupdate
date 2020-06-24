using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FileUpdateModelLib
{
    public class FileUpdateModelChecker
    {
        private readonly string _tempSourceDirectory;
        private FileUpdateModelConfig _fileUpdateConfig;

        public FileUpdateModelChecker(FileUpdateModelConfig config)
        {
            _fileUpdateConfig = config;
            _tempSourceDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        public void UnpackSource()
        {
            Directory.CreateDirectory(_tempSourceDirectory);
            ZipFile.ExtractToDirectory(_fileUpdateConfig.Source, _tempSourceDirectory);
        }

        public void ClearSource()
        {
            Directory.Delete(_tempSourceDirectory, true);
        }

        public void CheckVersion()
        {
            if (CheckVersionFile())
                return;

            if (CheckVersionProgram())
                return;
        }

        private bool CheckVersionFile()
        {
            string[] files = Directory.GetFiles(_fileUpdateConfig.Destination).Where(x => x.EndsWith(".txt") || x.EndsWith(".version")).ToArray();

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.ToLower().EndsWith("version.txt") || file.ToLower().EndsWith("version.version"))
                    {
                        string[] destinationVersion = GetFileVersion(_fileUpdateConfig.Destination);
                        string[] sourceVersion;

                        string path;

                        if (_fileUpdateConfig.NoZip)
                            path = _fileUpdateConfig.Source;
                        else
                            path = _tempSourceDirectory;

                        sourceVersion = GetFileVersion(path);

                        return CompareVersion(destinationVersion, sourceVersion);
                    }
                }
            }

            return false;
        }

        private string[] GetFileVersion(string path)
        {
            string[] version;

            using (StreamReader reader = new StreamReader(path + @"\version.*"))
            {
                version = reader.ReadToEnd().Split('.');
            }

            if (version == null || version.Length == 0)
                throw new Exception();

            return version;
        }

        private bool CheckVersionProgram()
        {
            string[] destinationOutput = GetProgrammVersion(_fileUpdateConfig.Destination);
            string[] sourceOutput = GetProgrammVersion(_fileUpdateConfig.Source);

            return CompareVersion(destinationOutput, sourceOutput);
        }

        private string[] GetProgrammVersion(string path)
        {
            string[] output;

            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(path, _fileUpdateConfig.Program),
                    Arguments = "version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                proc.Start();
                output = proc.StandardOutput.ReadToEnd()
                                            .Replace("\n", string.Empty)
                                            .Replace("\r", string.Empty)
                                            .Replace(" ", string.Empty)
                                            .Split('.');
            };

            if (output == null || output.Length == 0)
                throw new Exception();

            return output;
        }

        private bool CompareVersion(string[] destination, string[] source)
        {
            if (destination.Length != source.Length)
                throw new Exception();

            for (int i = 0; i < destination.Length; i++)
            {
                if (int.Parse(source[i]) > int.Parse(destination[i]))
                {
                    return true;
                }
            }
            throw new Exception("Source is older than Destination");
        }
    }
}
