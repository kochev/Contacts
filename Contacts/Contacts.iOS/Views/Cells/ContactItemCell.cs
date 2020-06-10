using System;
using Contacts.Core.ViewModels.Contacts;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace Contacts.iOS.Views.Cells
{
    [Register("ContactItemCell")]
    public class ContactItemCell : MvxTableViewCell
    {

        public static readonly NSString Key = new NSString("ContactItemCell");

        private ContactItemCell(IntPtr handle) : base(handle)
        {

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<ContactItemCell, ContactItemViewModel>();

                set.Apply();
            });
        }
    }
}