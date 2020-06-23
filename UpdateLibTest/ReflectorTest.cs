using UpdateLib;
using UpdateModelLib;
using Xunit;

namespace UpdateLibTest
{
    public class ReflectorTest
    {
        private const string _PATH = @".\Model";
        private Reflector _reflector;

        public ReflectorTest()
        {
            _reflector = new Reflector(_PATH);
        }

        [Fact]
        public void ReflectorCreateReference_PassingTest()
        {
            Reflector reflector = new Reflector(_PATH);

            Assert.NotNull(reflector);
        }

        [Fact]
        public void ReflectorCreateReferenceWithEmptyPath_FailingTest()
        {
            Reflector reflector;

            LibraryException ex = Assert.Throws<LibraryException>(() => reflector = new Reflector(string.Empty));

            Assert.Equal(ErrorCode.GLOBAL, ex.ErrorCode);
            Assert.Equal($"There was an ERROR with 'Model Directory: {string.Empty} not found!'", ex.ErrorMessage());
        }

        [Fact]
        public void ReflectorCreateReferenceWithNullPath_FailingTest()
        {
            Reflector reflector;

            LibraryException ex = Assert.Throws<LibraryException>(() => reflector = new Reflector(null));

            Assert.Equal(ErrorCode.GLOBAL, ex.ErrorCode);
            Assert.Equal($"There was an ERROR with 'Model Directory: {string.Empty} not found!'", ex.ErrorMessage());
        }

        [Fact]
        public void ReflectorCreateReferenceWithDirectoryWithoutDllFiles_FailingTest()
        {
            string path = @"..\";
            Reflector reflector;

            LibraryException ex = Assert.Throws<LibraryException>(() => reflector = new Reflector(path));

            Assert.Equal(ErrorCode.GLOBAL, ex.ErrorCode);
            Assert.Equal($"There was an ERROR with 'Model Directory: {path} does not contain *UpdateModelLib.dll files!'", ex.ErrorMessage());
        }

        [Fact]
        public void ReflectorGetInstance_PassingTest()
        {
            string model = "file";
            Assert.NotNull(_reflector.GetInstance(model));
        }

        [Fact]
        public void ReflectorGetInstanceWithEmptyModel_FailingTest()
        {
            LibraryException ex = Assert.Throws<LibraryException>(() => _reflector.GetInstance(string.Empty));

            Assert.Equal(ErrorCode.INVALID_MODEL, ex.ErrorCode);
            Assert.Equal($"Model Error 'Modelname is NULL or Empty!' unexpected", ex.ErrorMessage());
        }

        [Fact]
        public void ReflectorGetInstanceWithNullModel_FailingTest()
        {
            LibraryException ex = Assert.Throws<LibraryException>(() => _reflector.GetInstance(null));

            Assert.Equal(ErrorCode.INVALID_MODEL, ex.ErrorCode);
            Assert.Equal($"Model Error 'Modelname is NULL or Empty!' unexpected", ex.ErrorMessage());
        }

        [Fact]
        public void ReflectorGetInstanceWithNotExistingModel_FailingTest()
        {
            string model = "hallo";

            LibraryException ex = Assert.Throws<LibraryException>(() => _reflector.GetInstance(model));

            Assert.Equal(ErrorCode.INVALID_MODEL, ex.ErrorCode);
            Assert.Equal($"Model Error 'No existing UpdateModelLib with model '{model}'' unexpected", ex.ErrorMessage());
        }
    }
}
