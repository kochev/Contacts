using Contacts.Core.ViewModels.Contacts;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Contacts.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ContactsViewModel>();
        }
    }
}