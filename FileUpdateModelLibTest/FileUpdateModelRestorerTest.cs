using FileUpdateModelLib;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelRestorerTest
    {
        public FileUpdateModelRestorerTest()
        {

        }

        [Fact]
        public void FileUpdateModelRestorerCreateReference_PassingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            Assert.NotNull(modelRestorer);

            Directory.Delete(destination);
        }

        [Fact]
        public void FileUpdateModelRestorerCreateReferenceWithInvalidDestination_FailingTest()
        {
            FileUpdateModelRestorer modelRestorer;

            Exception ex = Assert.Throws<Exception>(() => modelRestorer = new FileUpdateModelRestorer(null));

            Assert.Equal("Destination directory does not exist!", ex.Message);
        }

        [Fact]
        public void FileUpdateModelRestorerBackup_PassingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            var filename = Path.GetRandomFileName();
            File.Create(Path.Combine(destination, filename)).Close();

            modelRestorer.Backup();

            Assert.NotNull(Directory.GetFiles(modelRestorer.BackupDirectory).First(x => x.EndsWith(filename)));

            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerBackupWithNotExistingDestination_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            Directory.Delete(destination, true);

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.Backup());

            Assert.Equal("Destination folder not found!", ex.Message);
        }

        [Fact]
        public void FileUpdateModelRestorerBackupFileWhichAreUsedByAnotherProcess_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            var file = File.Create(Path.Combine(destination, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.Backup());

            Assert.Equal("Not able to create backup files!", ex.Message);

            file.Close();
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerClearBackup_PassingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            File.Create(Path.Combine(destination, Path.GetRandomFileName())).Close();

            modelRestorer.Backup();
            modelRestorer.ClearBackup();

            Assert.False(Directory.Exists(modelRestorer.BackupDirectory));

            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerClearNotExistingBackup_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.ClearBackup());

            Assert.Equal("No backup found!", ex.Message);

            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerClearBackupWithFilesUsedByAnotherProcess_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);
            modelRestorer.Backup();

            var file = File.Create(Path.Combine(modelRestorer.BackupDirectory, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.ClearBackup());

            Assert.Equal("Not able to clear backup!", ex.Message);

            file.Close();
            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerRollback_PassingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            File.Create(Path.Combine(destination, Path.GetRandomFileName())).Close();

            modelRestorer.Backup();

            DirectoryInfo info = new DirectoryInfo(destination);

            foreach (var file in info.GetFiles())
            {
                file.Delete();
            }

            modelRestorer.Rollback();

            Assert.True(Directory.GetFiles(destination).Length == 1);

            Directory.Delete(destination, true);
        }

        [Fact]
        public void FileUpdateModelRestorerRollbackWithNotExistingDestination_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            Directory.Delete(destination);

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.Rollback());

            Assert.Equal("Destination folder not found!", ex.Message);
        }

        [Fact]
        public void FileUpdateModelRestorerRollbackFilesUsedByAnotherProcessClearDestination_FailingTest()
        {
            string destination = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(destination);

            FileUpdateModelRestorer modelRestorer = new FileUpdateModelRestorer(destination);

            var file = File.Create(Path.Combine(destination, Path.GetRandomFileName()));

            Exception ex = Assert.Throws<Exception>(() => modelRestorer.Rollback());

            Assert.Equal("Not able to clear destination folder!", ex.Message);

            file.Close();
            Directory.Delete(destination, true);
        }
    }
}
