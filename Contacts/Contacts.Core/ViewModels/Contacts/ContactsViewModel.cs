using System;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Core.DatabaseModels;
using Microsoft.AppCenter.Crashes;
using MvvmCross.ViewModels;
using Realms;

namespace Contacts.Core.ViewModels.Contacts
{
    public class ContactsViewModel : BaseViewModel
    {
        private MvxObservableCollection<ContactItemViewModel> _contacts;
        private IDisposable _token;

        public ContactsViewModel()
        {
            Contacts = new MvxObservableCollection<ContactItemViewModel>();
        }

        public MvxObservableCollection<ContactItemViewModel> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        public override async void Prepare()
        {
            base.Prepare();

            var contacts = DatabaseService.GetAll<ContactDto>().OrderBy(feed => feed.Name).AsRealmCollection();
            Contacts = new MvxObservableCollection<ContactItemViewModel>(contacts.Select((contactDto, i) =>
                new ContactItemViewModel(contactDto)));
            _token = contacts.SubscribeForNotifications(UpdateContactsCallback);

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
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        private async Task LoadContactsByUrl(string url)
        {
            try
            {
                var data = await ApiService.GetContacts(url, RequestHelpers.LongTimeout);
                if (data.Error == null)
                {
                    if (data.Content?.Count > 0)
                        data.Content.ForEach(contact => DatabaseService.AddOrUpdate(new ContactDto(contact)));
                }
                else
                {
                    AlertService.ShowLongToast($"Произошла ошибка при загрузке данных: {data.Error.Message}");
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
                if (changes == null) return;

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
                        Contacts.Add(new ContactItemViewModel(sender[index]));
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}