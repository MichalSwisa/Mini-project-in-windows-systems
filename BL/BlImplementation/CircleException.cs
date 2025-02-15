//namespace BlImplementation;
//using BlApi;
//using System.Linq;


using System.Runtime.Serialization;

namespace BlImplementation
{
    [Serializable]
    internal class CircleException : Exception
    {
        public CircleException()
        {
        }

        public CircleException(string? message) : base(message)
        {
        }

        public CircleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CircleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}