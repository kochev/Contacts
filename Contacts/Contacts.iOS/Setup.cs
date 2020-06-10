using System;
using System.Collections.Generic;
using Contacts.Core;
using Contacts.Core.Services.Implementation;
using Contacts.Core.Services.Interfaces;
using Contacts.Core.ViewModels.Contacts;
using Contacts.iOS.Services.Base;
using Contacts.iOS.Views.Contacts;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Views;

namespace Contacts.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override IMvxViewsContainer InitializeViewLookup(IDictionary<Type, Type> viewModelViewLookup)
        {
            var container = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            viewModelViewLookup = new Dictionary<Type, Type>
            {
                {typeof(ContactsViewModel), typeof(ContactsViewController)},
                {typeof(ContactViewModel), typeof(ContactViewController)}
            };
            container.AddAll(viewModelViewLookup);
            return container;
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton(typeof(IRequestService), new RequestService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IAlertService), new AlertService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IApiService), new ApiService());
        }
    }
}