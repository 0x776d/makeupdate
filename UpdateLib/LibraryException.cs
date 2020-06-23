using UpdateModelLib;

namespace UpdateLib
{
    public class LibraryException : UpdateException
    {
        public LibraryException(ErrorCode errorCode, string errorParameter) : base(errorCode, errorParameter) { }

        public override string ErrorMessage()
        {
            switch (ErrorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not be reached!";
                case ErrorCode.GLOBAL:
                    return $"There was an ERROR with '{ErrorParameter}'";
                case ErrorCode.INVALID_MODEL:
                    return $"Model Error '{ErrorParameter}' unexpected";
                default:
                    return string.Empty;
            }
        }
    }
}
