using Android.App;
using Android.OS;
using Contacts.Core.ViewModels.Contacts;
using Contacts.Droid.Views.Base;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace Contacts.Droid.Views.Contacts
{
    [Activity]
    [MvxActivityPresentation]
    public class ContactsActivity : BaseActivity<ContactsViewModel>
    {
        protected override int LayoutResource => Resource.Layout.activity_contacts;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SupportActionBar?.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar?.SetDefaultDisplayHomeAsUpEnabled(false);
        }
    }
}