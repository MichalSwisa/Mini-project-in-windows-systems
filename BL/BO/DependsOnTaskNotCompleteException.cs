using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    internal class DependsOnTaskNotCompleteException : Exception
    {
        public DependsOnTaskNotCompleteException():base()
        {
        }

        public DependsOnTaskNotCompleteException(string? message) : base(message)
        {
        }

        public DependsOnTaskNotCompleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DependsOnTaskNotCompleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}