using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace Contacts.iOS.Views.Sources
{
    public class ContactsTableViewSource : MvxStandardTableViewSource
    {
        public ContactsTableViewSource(UITableView tableView) : base(tableView)
        {
        }

        public ContactsTableViewSource(UITableView tableView, NSString cellIdentifier) : base(tableView, cellIdentifier)
        {
        }

        public ContactsTableViewSource(UITableView tableView, string bindingText) : base(tableView, bindingText)
        {
        }

        public ContactsTableViewSource(IntPtr handle) : base(handle)
        {
        }

        public ContactsTableViewSource(UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier, string bindingText,
            UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None) : base(tableView, style, cellIdentifier,
            bindingText, tableViewCellAccessory)
        {
        }


        public ContactsTableViewSource(UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier,
            IEnumerable<MvxBindingDescription> descriptions,
            UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None) : base(tableView, style, cellIdentifier,
            descriptions, tableViewCellAccessory)
        {
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemsSource?.Count() ?? 0;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            tableView.DeselectRow(indexPath, true);
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }

        public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return ItemsSource.ElementAt(indexPath.Row);
        }
    }
}