using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace Contacts.Droid.Views.Base
{
    [MvxFragmentPresentation]
    public abstract class BaseFragment<T> : MvxFragment<T> where T : MvxViewModel
    {
        protected abstract int LayoutResource { get; }
        public View RootView { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            RootView = this.BindingInflate(LayoutResource, null);
            return RootView;
        }
    }
}