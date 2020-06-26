using ArgumentsLib;
using FileUpdateModelLib;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelConfigTest
    {
        public FileUpdateModelConfigTest()
        {

        }

        [Fact]
        public void FileUpdateModelConfigCreateReference_PassingTest()
        {
            string[] args = new string[]
            {
                "-source",
                "Sourcepfad",
                "-destination",
                "Zielpfad",
                "-program",
                "UpdateTestApp.exe",
                "-nozip",
                "-start"
            };

            Arguments arguments = new Arguments(@".\Marshaler", "source*,destination*,program*,skipversion,nobackup,nozip,start", args);
            FileUpdateModelConfig config = new FileUpdateModelConfig(arguments);

            Assert.Equal(args[1], config.Source);
            Assert.Equal(args[3], config.Destination);
            Assert.Equal(args[5], config.Program);
            Assert.False(config.SkipVersionCheck);
            Assert.False(config.NoBackup);
            Assert.True(config.NoZip);
            Assert.True(config.StartAfterUpdate);
        }
    }
}
