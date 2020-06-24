using ArgumentsLib;
using FileUpdateModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelUpdaterTest
    {
        private readonly string _source;
        private readonly string _destination;
        private string[] _args;
        private FileUpdateModelConfig _fileUpdateConfig;
        private Arguments _arguments;

        public FileUpdateModelUpdaterTest()
        {
            _source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            _destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

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
            _fileUpdateConfig = new FileUpdateModelConfig(_arguments);
        }

        [Fact]
        public void FileUpdateModelUpdaterCreateReference_PassingTest()
        {
            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(_fileUpdateConfig);

            Assert.NotNull(modelUpdater);
        }

        [Fact]
        public void FileUpdateModelUpdaterClearDestination_PassingTest()
        {
            Directory.CreateDirectory(_destination);

            for (int i = 0; i < 5; i++)
            {
                File.Create(Path.Combine(_destination, Path.GetRandomFileName()));
                Directory.CreateDirectory(Path.Combine(_destination, Path.GetRandomFileName()));
            }
        }
    }
}
