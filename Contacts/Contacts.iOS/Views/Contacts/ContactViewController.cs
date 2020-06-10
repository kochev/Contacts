using Contacts.Core.ViewModels.Contacts;
using Contacts.iOS.Views.Base;
using Foundation;
using UIKit;

namespace Contacts.iOS.Views.Contacts
{
    [Register("ContactViewController")]
    public class ContactViewController : BaseViewController<ContactViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            View.BackgroundColor = UIColor.White;
        }
    }
}