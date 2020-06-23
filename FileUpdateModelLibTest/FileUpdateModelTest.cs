using FileUpdateModelLib;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModelTest
    {
        public FileUpdateModelTest()
        {

        }

        [Fact]
        public void FileUpdateModelCreateReference_PassingTest()
        {
            FileUpdateModel fileUpdateModel = new FileUpdateModel();

            Assert.Equal("File".ToLower(), fileUpdateModel.Model);
        }
    }
}
