using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.Core.DatabaseModels;
using Contacts.Core.Extensions;
using Contacts.Core.Models.Base;
using Contacts.Core.Models.Navigation;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Realms;

namespace Contacts.Core.ViewModels.Contacts
{
    public class ContactsViewModel : BaseViewModel
    {
        private IRealmCollection<ContactDto> _allContacts;
        private MvxCommand<ContactItemViewModel> _contactClickCommand;
        private MvxObservableCollection<ContactItemViewModel> _contacts;
        private MvxCommand _refreshCommand;
        private MvxCommand _searchCloseCommand;
        private string _searchQuery;
        private IDisposable _token;

        public ContactsViewModel()
        {
            Title = "Контакты";
            Contacts = new MvxObservableCollection<ContactItemViewModel>();
        }

        public MvxInteraction<ErrorResponse> ShowErrorInteraction { get; } =
            new MvxInteraction<ErrorResponse>();

        public MvxInteraction<bool> StartRefreshInteraction { get; } =
            new MvxInteraction<bool>();

        public ICommand RefreshCommand
        {
            get { return _refreshCommand = _refreshCommand ?? new MvxCommand(DoRefresh); }
        }

        public ICommand ContactClickCommand
        {
            get { return _contactClickCommand = _contactClickCommand ?? new MvxCommand<ContactItemViewModel>(DoContactClick); }
        }

        public MvxObservableCollection<ContactItemViewModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetProperty(ref _searchQuery, value);
                if (_searchQuery?.Length > 0 && SearchOpened)
                    Contacts = new MvxObservableCollection<ContactItemViewModel>(_allContacts
                        .Where(dto => dto.Name.ToLower().Contains(_searchQuery.ToLower()) ||
                                      new string(dto.Phone.Where(char.IsDigit).ToArray()).Contains(_searchQuery.ToLower()) ||
                                      dto.Phone.Contains(_searchQuery.ToLower())).Select(dto => new ContactItemViewModel(dto)));
                else if (SearchOpened && _searchQuery.IsNullOrEmptyOrWhiteSpace()) SearchCloseCommand.Execute(null);
            }
        }

        public ICommand SearchCloseCommand
        {
            get
            {
                return _searchCloseCommand = _searchCloseCommand ?? new MvxCommand(() =>
                {
                    Contacts = new MvxObservableCollection<ContactItemViewModel>(_allContacts.Select(contactDto =>
                        new ContactItemViewModel(contactDto)));
                });
            }
        }

        public bool SearchOpened { get; set; }

        private async void DoContactClick(ContactItemViewModel itemViewModel)
        {
            await NavigationService.Navigate<ContactViewModel, ContactNavigationParams>(new ContactNavigationParams
                {Contact = itemViewModel.Contact});
        }

        private async void DoRefresh()
        {
            StartRefreshInteraction?.Raise(true);
            await LoadData();
        }

        public override async void Prepare()
        {
            base.Prepare();

            _allContacts = DatabaseService.GetAll<ContactDto>().OrderBy(feed => feed.Name).AsRealmCollection();
            Contacts = new MvxObservableCollection<ContactItemViewModel>(_allContacts.Select(contactDto =>
                new ContactItemViewModel(contactDto)));

            _token = _allContacts.SubscribeForNotifications(UpdateContactsCallback);

            if (LocalSettings.LastUpdateDatetime < DateTime.Now.AddMinutes(-1))
                await LoadData();
        }

        private async Task LoadData()
        {
            ShowProgress = true;
            try
            {
                await LoadContactsByUrl(RequestHelpers.ContactsUrl1);
                await LoadContactsByUrl(RequestHelpers.ContactsUrl2);
                await LoadContactsByUrl(RequestHelpers.ContactsUrl3);

                LocalSettings.LastUpdateDatetime = DateTime.Now;
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                ShowProgress = false;
            }
        }

        private async Task LoadContactsByUrl(string url)
        {
            try
            {
                var data = await ApiService.GetContacts(url, RequestHelpers.LongTimeout);
                if (data.Error == null)
                {
                    if (data.Content?.Count > 0) DatabaseService.AddOrUpdate(data.Content.Select(contact => new ContactDto(contact)));
                }
                else
                {
                    ShowErrorInteraction?.Raise(data.Error);
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        private void UpdateContactsCallback(IRealmCollection<ContactDto> sender, ChangeSet changes, Exception error)
        {
            try
            {
                if (changes == null || Contacts.Count == 0) return;

                if (changes.InsertedIndices?.Length > 0)
                    foreach (var index in changes.InsertedIndices)
                        Contacts.Add(new ContactItemViewModel(sender[index]));

                if (changes.DeletedIndices?.Length > 0)
                    foreach (var index in changes.DeletedIndices.Reverse())
                        Contacts.RemoveAt(index);

                if (changes.ModifiedIndices?.Length > 0)
                    foreach (var index in changes.ModifiedIndices)
                        Contacts[index].Update();

                if (changes.NewModifiedIndices?.Length > 0)
                    foreach (var index in changes.NewModifiedIndices)
                        Contacts[index].Update();


                Contacts = new MvxObservableCollection<ContactItemViewModel>(Contacts.OrderBy(model => model.Name));
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}