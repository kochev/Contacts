using System;
using System.Threading.Tasks;
using Contacts.Core.Models.Base;

namespace Contacts.Core.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ClientResponse<T>> Get<T>(string path, TimeSpan timeout, bool needAuthorization = false);
        Task<ClientResponse<T>> Post<T, IT>(string path, IT data, TimeSpan timeout, bool needAuthorization = false);
        Task<ClientResponse<T>> Put<T, IT>(string path, IT data, TimeSpan timeout, bool needAuthorization = false);
        Task<ClientResponse<bool>> Delete(string path, TimeSpan timeout, bool needAuthorization = false);
        Task<ClientResponse<bool>> GetWithoutContent<T>(string path, TimeSpan timeout, bool needAuthorization = false);
    }
}