namespace SbJwlLauncher.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class JwlManagerException : Exception
    {
        public JwlManagerException()
        {
        }

        public JwlManagerException(string message)
        : base(message)
        {
        }

        public JwlManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected JwlManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
