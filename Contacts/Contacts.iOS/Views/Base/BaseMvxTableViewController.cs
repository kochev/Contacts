using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using ObjCRuntime;
using UIKit;

namespace Contacts.iOS.Views.Base
{
    public class BaseMvxTableViewController<T> : MvxTableViewController<T> where T : MvxViewModel
    {
        private UIActivityIndicatorView _activityIndicator;
        private UIView _disablingView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
            {
                Center = View.Center,
                HidesWhenStopped = false
            };

            _disablingView = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Hidden = true,
                Alpha = 0.5f,
                BackgroundColor = UIColor.Black
            };
            _disablingView.AddSubview(_activityIndicator);
        }

        public void SetStatusBarColor(UIColor color)
        {
            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar != null && statusBar.RespondsToSelector(new Selector("setBackgroundColor:")))
                statusBar.BackgroundColor = color;
        }
    }
}