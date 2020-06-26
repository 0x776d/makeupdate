using ArgumentsLib;
using FileUpdateModelLib;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelCheckerTest
    {
        private readonly string _source;
        private readonly string _destination;
        private string[] _args;

        private Arguments _arguments;
        private FileUpdateModelConfig _updateModelConfig;

        public FileUpdateModelCheckerTest()
        {
            _source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            _destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(_source);
            Directory.CreateDirectory(_destination);

            _args = new string[]
            {
                "-source",
                $"{_source}",
                "-destination",
                $"{_destination}",
                "-program",
                "UpdateTestApp.exe",
                "-nozip",
                "-start"
            };

            _arguments = new Arguments(@".\Marshaler", "source*,destination*,program*,skipversion,nobackup,nozip,start", _args);
            _updateModelConfig = new FileUpdateModelConfig(_arguments);
        }

        [Fact]
        public void FileUpdateModelCheckerCreateReference_PassingTest()
        {
            FileUpdateModelChecker updateModelChecker = new FileUpdateModelChecker(_updateModelConfig);

            Assert.NotNull(updateModelChecker);
        }

        [Fact]
        public void FileUpdateModelCheckerUnpackSource_PassingTest()
        {
            FileUpdateModelChecker updateModelChecker = new FileUpdateModelChecker(_updateModelConfig);

            CreateTestZipFile(_updateModelConfig.Source);

            updateModelChecker.UnpackSource();

            Assert.True(Directory.GetFiles(updateModelChecker.SourceDirectory).Count(x => x == Path.Combine(updateModelChecker.SourceDirectory, "test.txt")) == 1);

            Directory.Delete(updateModelChecker.SourceDirectory, true);
        }

        private void CreateTestZipFile(string path)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = archive.CreateEntry("test.txt");
                }

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
    }
}
