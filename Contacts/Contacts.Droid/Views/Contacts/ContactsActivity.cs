using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Contacts.Core.Models.Base;
using Contacts.Core.ViewModels.Contacts;
using Contacts.Droid.Views.Base;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace Contacts.Droid.Views.Contacts
{
    [Activity]
    [MvxActivityPresentation]
    public class ContactsActivity : BaseActivity<ContactsViewModel>, IMenuItemOnActionExpandListener
    {
        private LinearLayoutManager _contactsLayoutManager;
        private MvxRecyclerView _contactsRecyclerView;
        private Snackbar _errorSnackbar;
        private SearchView _searchView;
        private IMvxInteraction<ErrorResponse> _showErrorInteraction;
        private IMvxInteraction<bool> _startRefreshInteraction;
        protected override int LayoutResource => Resource.Layout.activity_contacts;

        public IMenuItem SearchItem { get; set; }

        public IMvxInteraction<ErrorResponse> ShowErrorInteraction
        {
            get => _showErrorInteraction;
            set
            {
                if (_showErrorInteraction != null)
                    _showErrorInteraction.Requested -= OnShowErrorInteractionRequested;

                _showErrorInteraction = value;
                _showErrorInteraction.Requested += OnShowErrorInteractionRequested;
            }
        }

        public IMvxInteraction<bool> StartRefreshInteraction
        {
            get => _startRefreshInteraction;
            set
            {
                if (_startRefreshInteraction != null)
                    _startRefreshInteraction.Requested -= OnStartRefreshInteractionRequested;

                _startRefreshInteraction = value;
                _startRefreshInteraction.Requested += OnStartRefreshInteractionRequested;
            }
        }

        public bool OnMenuItemActionCollapse(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.search)
            {
                ViewModel.SearchOpened = false;
                ViewModel.SearchQuery = string.Empty;
                ViewModel.SearchCloseCommand.Execute(null);
            }

            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.search)
                ViewModel.SearchOpened = true;
            return true;
        }

        private void OnStartRefreshInteractionRequested(object sender, MvxValueEventArgs<bool> e)
        {
            RunOnUiThread(() =>
            {
                if (_errorSnackbar.IsShown)
                    _errorSnackbar.Dismiss();
            });
        }

        private void OnShowErrorInteractionRequested(object sender, MvxValueEventArgs<ErrorResponse> e)
        {
            _errorSnackbar.Show();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (SupportActionBar != null)
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(false);
            }

            _contactsRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.activity_contacts_recyclerview);
            _contactsLayoutManager = new LinearLayoutManager(this, OrientationHelper.Vertical, false);
            _contactsRecyclerView.AddItemDecoration(new DividerItemDecoration(this, _contactsLayoutManager.Orientation));
            _contactsRecyclerView.SetLayoutManager(_contactsLayoutManager);

            var refreshLayout = FindViewById<MvxSwipeRefreshLayout>(Resource.Id.activity_contacts_refreshlayout);
            var primaryColor = ContextCompat.GetColor(this, Resource.Color.primaryColor);
            var primaryLightColor = ContextCompat.GetColor(this, Resource.Color.primaryLightColor);
            refreshLayout.SetColorSchemeColors(primaryColor, primaryLightColor, primaryColor);

            _errorSnackbar = Snackbar.Make(_contactsRecyclerView,
                    "Произошла ошибка при загрузке данных. Будут отображены ранее сохраненные контакты.", Snackbar.LengthIndefinite)
                .SetAction("ОК", v => { });
            var snackbarView = _errorSnackbar.View;
            if (snackbarView != null)
            {
                var snackbarTextView = snackbarView.FindViewById<TextView>(Resource.Id.snackbar_text);
                snackbarTextView?.SetMaxLines(3);
            }

            var set = this.CreateBindingSet<ContactsActivity, ContactsViewModel>();
            set.Bind(SupportActionBar).For(actionBar => actionBar.Title).To(viewModel => viewModel.Title).OneWay();
            set.Bind(this).For(view => view.StartRefreshInteraction).To(viewModel => viewModel.StartRefreshInteraction).OneWay();
            set.Bind(this).For(view => view.ShowErrorInteraction).To(viewModel => viewModel.ShowErrorInteraction).OneWay();
            set.Apply();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);

            SearchItem = menu.FindItem(Resource.Id.search);
            SearchItem.SetOnActionExpandListener(this);
            _searchView = SearchItem.ActionView as SearchView;


            var set = this.CreateBindingSet<ContactsActivity, ContactsViewModel>();
            set.Bind(_searchView).For(searchView => searchView.Query).To(viewModel => viewModel.SearchQuery);
            set.Apply();

            return base.OnCreateOptionsMenu(menu);
        }
    }
}