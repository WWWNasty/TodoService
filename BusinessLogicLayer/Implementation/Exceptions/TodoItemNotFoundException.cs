using System;
using System.Runtime.Serialization;

namespace BusinessLogicLayer.Implementation.Exceptions
{
    [Serializable]
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException()
        {
        }

        public TodoItemNotFoundException(string message) : base(message)
        {
        }

        public TodoItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TodoItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}