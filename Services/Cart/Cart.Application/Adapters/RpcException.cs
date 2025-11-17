
namespace Cart.Application.Adapters
{
    [Serializable]
    internal class RpcException : Exception
    {
        public RpcException()
        {
        }

        public RpcException(string? message) : base(message)
        {
        }

        public RpcException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public object StatusCode { get; internal set; }
    }
}