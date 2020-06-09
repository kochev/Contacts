using Android.App;
using Android.Views;
using Contacts.Core.ViewModels.Contacts;
using Contacts.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Contacts.Droid.Views.Contacts
{
    [Activity]
    [MvxActivityPresentation]
    public class ContactActivity : BaseActivity<ContactViewModel>
    {
        protected override int LayoutResource => Resource.Layout.activity_contact;

        public override async void OnBackPressed()
        {
            await ViewModel.CloseCommand.ExecuteAsync();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}