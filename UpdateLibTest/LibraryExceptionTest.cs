using UpdateLib;
using UpdateModelLib;
using Xunit;

namespace UpdateLibTest
{
    public class LibraryExceptionTest
    {
        public LibraryExceptionTest()
        {

        }

        [Theory]
        [InlineData(ErrorCode.OK, null, "TILT: Should not be reached!")]
        [InlineData(ErrorCode.GLOBAL, null, "There was an ERROR with ''")]
        [InlineData(ErrorCode.INVALID_MODEL, null, "Model Error '' unexpected")]
        public void LibraryExceptionCreateReference_PassingTest(ErrorCode errorCode, string parameter, string errorMessage)
        {
            LibraryException exception = new LibraryException(errorCode, parameter);

            Assert.Equal(errorCode, exception.ErrorCode);
            Assert.Equal(parameter, exception.ErrorParameter);
            Assert.Equal(errorMessage, exception.ErrorMessage());
        }
    }
}
