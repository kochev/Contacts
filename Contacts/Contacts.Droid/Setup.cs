using System;
using System.Collections.Generic;
using Contacts.Core;
using Contacts.Core.Services.Implementation;
using Contacts.Core.Services.Interfaces;
using Contacts.Core.ViewModels.Contacts;
using Contacts.Droid.Services;
using Contacts.Droid.Views.Contacts;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace Contacts.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IMvxViewsContainer InitializeViewLookup(IDictionary<Type, Type> viewModelViewLookup)
        {
            var container = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            viewModelViewLookup = new Dictionary<Type, Type>
            {
                {typeof(ContactsViewModel), typeof(ContactsActivity)}
            };
            container.AddAll(viewModelViewLookup);
            return container;
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.RegisterSingleton(typeof(IAlertService), new AlertService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IRequestService), new RequestService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IApiService), new ApiService());
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.IoCProvider.RegisterSingleton(typeof(DatabaseService), new DatabaseService());
        }
    }
}