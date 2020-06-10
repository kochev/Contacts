using System;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace Contacts.iOS.Views.Base
{
    public class BaseViewController<T> : MvxViewController<T> where T : class, IMvxViewModel
    {
        private UIView _progressView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _progressView = Extensions.ProcessView(View.Bounds, UIColor.FromRGB(245, 241, 238), Colors.Primary);
            _progressView.Hidden = true;
        }
    }
}