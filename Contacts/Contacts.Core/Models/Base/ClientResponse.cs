using System.Net;

namespace Contacts.Core.Models.Base
{
    public class ClientResponse<T>
    {
        public T Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse Error { get; set; }
        public bool IsCanceled { get; set; }
    }
}