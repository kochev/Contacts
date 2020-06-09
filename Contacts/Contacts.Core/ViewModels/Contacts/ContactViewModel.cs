using Contacts.Core.Extensions;
using Contacts.Core.Models;
using Contacts.Core.Models.Navigation;

namespace Contacts.Core.ViewModels.Contacts
{
    public class ContactViewModel : BaseViewModel<ContactNavigationParams>
    {
        private string _biography;
        private string _educationPeriod;
        private string _name;
        private string _phone;
        private string _temperament;

        public ContactViewModel()
        {
            Title = "Информация";
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

        public string Biography
        {
            get => _biography;
            set => SetProperty(ref _biography, value);
        }

        public string Temperament
        {
            get => _temperament;
            set => SetProperty(ref _temperament, value);
        }

        public string EducationPeriod
        {
            get => _educationPeriod;
            set => SetProperty(ref _educationPeriod, value);
        }

        public override void Prepare(ContactNavigationParams @params)
        {
            base.Prepare(@params);
            Name = Params.Contact.Name;
            Phone = Params.Contact.Phone;
            Temperament = ((Temperament) Params.Contact.Temperament).GetEnumDescription();
            EducationPeriod =
                $"{Params.Contact.EducationPeriod.Start.Date:dd.MM.yyy} - {Params.Contact.EducationPeriod.End.Date:dd.MM.yyy}";
            Biography = Params.Contact.Biography;
        }
    }
}