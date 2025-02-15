using System.Runtime.Serialization;

namespace BlImplementation
{
    [Serializable]
    internal class DependenceTaskExist : Exception
    {
        public DependenceTaskExist()
        {
        }

        public DependenceTaskExist(string? message) : base(message)
        {
        }

        public DependenceTaskExist(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DependenceTaskExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}