using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contacts.Core.Models;
using Contacts.Core.Models.Base;
using Contacts.Core.Services.Interfaces;
using MvvmCross;

namespace Contacts.Core.Services.Implementation
{
    public class ApiService : IApiService
    {
        public ApiService()
        {
            RequestService = Mvx.IoCProvider.Resolve<IRequestService>();
        }

        public IRequestService RequestService { get; }

        public Task<ClientResponse<List<Contact>>> GetContacts(string url, TimeSpan timeout)
        {
            return RequestService.Get<List<Contact>>(url, timeout);
        }
    }
}