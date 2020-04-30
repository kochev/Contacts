using Contacts.Core.DatabaseModels;
using MvvmCross.ViewModels;

namespace Contacts.Core.ViewModels.Contacts
{
    public class ContactItemViewModel : MvxNotifyPropertyChanged
    {
        private float _height;
        private string _name;
        private string _phone;

        public ContactItemViewModel(ContactDto contactDto)
        {
            Contact = contactDto;
            Name = Contact.Name;
            Phone = Contact.Phone;
            Height = Contact.Height;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public float Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public ContactDto Contact { get; }

        public void Update()
        {
            Name = Contact.Name;
            Phone = Contact.Phone;
            Height = Contact.Height;
        }
    }
}