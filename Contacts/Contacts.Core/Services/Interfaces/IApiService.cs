using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contacts.Core.Models;
using Contacts.Core.Models.Base;

namespace Contacts.Core.Services.Interfaces
{
    public interface IApiService
    {
        Task<ClientResponse<List<Contact>>> GetContacts(string url, TimeSpan timeout);
    }
}