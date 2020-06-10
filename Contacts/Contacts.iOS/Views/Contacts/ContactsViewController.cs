using Contacts.Core.ViewModels.Contacts;
using Contacts.iOS.Views.Base;
using Contacts.iOS.Views.Cells;
using Contacts.iOS.Views.Sources;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace Contacts.iOS.Views.Contacts
{
    [Register("ContactsViewController")]
    public class ContactsViewController : BaseMvxTableViewController<ContactsViewModel>
    {
        private ContactsTableViewSource _source;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.BackgroundColor = UIColor.White;

            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            NavigationController.NavigationBar.BarTintColor = Colors.Primary;
            NavigationController.NavigationBar.Translucent = false;
            NavigationController.NavigationBar.Opaque = true;
            NavigationController.NavigationBar.TintColor = UIColor.White;

            NavigationItem.BackBarButtonItem = null;


            TableView.RegisterClassForCellReuse(typeof(ContactItemCell), ContactItemCell.Key);
            _source = new ContactsTableViewSource(TableView, ContactItemCell.Key);
            TableView.Source = _source;

            var set = this.CreateBindingSet<ContactsViewController, ContactsViewModel>();
            set.Bind(_source).To(vm => vm.Contacts);
            set.Bind(_source).For(s => s.SelectionChangedCommand).To(vm => vm.ContactClickCommand);
            set.Bind().For(v => v.Title).To(vm => vm.Title);
            set.Apply();
        }
    }
}