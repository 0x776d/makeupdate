using FileUpdateModelLib;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelUpdaterTest
    {
        public FileUpdateModelUpdaterTest()
        {
        }

        [Fact]
        public void FileUpdateModelUpdaterCreateReference_PassingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            Assert.NotNull(modelUpdater);

            Directory.Delete(source, true);
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelCreateReferenceInvalidDestination_FailingTest()
        {
            FileUpdateModelUpdater modelUpdater;

            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);

            Exception ex = Assert.Throws<Exception>(() => modelUpdater = new FileUpdateModelUpdater(destination, source));

            Assert.Equal("Destination directory does not exist!", ex.Message);

            Directory.Delete(source, true);
        }

        [Fact]
        public void FileUpdateModelCreateReferenceInvalidSource_FailingTest()
        {
            FileUpdateModelUpdater modelUpdater;

            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()); ;
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(destination);

            Exception ex = Assert.Throws<Exception>(() => modelUpdater = new FileUpdateModelUpdater(destination, source));

            Assert.Equal("Source directory does not exist!", ex.Message);

            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelUpdaterClearDestination_PassingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            for (int i = 0; i < 5; i++)
            {
                File.Create(Path.Combine(destination, Path.GetRandomFileName())).Close();
                Directory.CreateDirectory(Path.Combine(destination, Path.GetRandomFileName()));
            }

            modelUpdater.ClearDestination();

            Assert.True(Directory.GetFiles(destination).Length == 0);
            Assert.True(Directory.GetDirectories(destination).Length == 0);

            Directory.Delete(source, true);
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelUpdaterClearNotExistingDestination_FailingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            Directory.Delete(source, true);
            Directory.Delete(destination, true);

            Exception ex = Assert.Throws<Exception>(() => modelUpdater.ClearDestination());

            Assert.Equal("Destination folder not found!", ex.Message);
        }

        [Fact]
        public void FileUpdateModelUpdaterClearDestinationWhenFilesAreUsedByAnotherProcess_FailingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            var file = File.Create(Path.Combine(destination, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelUpdater.ClearDestination());

            Assert.Equal("File cannot be deleted because it is used by another process!", ex.Message);

            file.Close();

            Directory.Delete(source, true);
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelUpdaterClearDestinationWhenFilesAreUsedInDirectoryByAnotherProcess_FailingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            var directoryName = Path.GetRandomFileName();

            Directory.CreateDirectory(Path.Combine(destination, directoryName));
            var file = File.Create(Path.Combine(destination, directoryName, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelUpdater.ClearDestination());

            Assert.Equal("Directory cannot be deleted because there are files in it which are used by another process!", ex.Message);

            file.Close();

            Directory.Delete(source, true);
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelUpdaterLoadSourceToDestination_PassingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            var filename = Path.GetRandomFileName();
            File.Create(Path.Combine(source, filename)).Close();

            modelUpdater.LoadSourceToDestination();

            Assert.NotNull(Directory.GetFiles(destination).First(x => x.EndsWith(filename)));
            Assert.NotNull(Directory.GetFiles(source).First(x => x.EndsWith(filename)));

            Directory.Delete(destination, true);
            Directory.Delete(source, true);
        }

        [Fact]
        public void FileUpdateModelUpdaterLoadSourceToDestinationWithFilesUsedByAnotherProcess_FailingTest()
        {
            string source = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(source);
            Directory.CreateDirectory(destination);

            FileUpdateModelUpdater modelUpdater = new FileUpdateModelUpdater(destination, source);

            var file = File.Create(Path.Combine(source, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelUpdater.LoadSourceToDestination());

            Assert.Equal("Loading source files to destination failed!", ex.Message);

            file.Close();

            Directory.Delete(source, true);
            Directory.Delete(destination, true);
        }
    }
}
