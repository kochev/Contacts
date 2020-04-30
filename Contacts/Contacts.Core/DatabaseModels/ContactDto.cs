using System;
using Contacts.Core.Models;
using Realms;

namespace Contacts.Core.DatabaseModels
{
    public class ContactDto : RealmObject, IDBEntity
    {
        public ContactDto()
        {
        }

        public ContactDto(Contact contact)
        {
            Name = contact.Name;
            Biography = contact.Biography;
            EducationPeriod = new EducationPeriodDto(contact.EducationPeriod);
            Phone = contact.Phone;
            Height = contact.Height;
            Temperament = (int) contact.Temperament;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public float Height { get; set; }
        public string Biography { get; set; }
        public int Temperament { get; set; }
        public EducationPeriodDto EducationPeriod { get; set; }
        [PrimaryKey] public string Id { get; set; }

        public Contact FromDto()
        {
            return new Contact
            {
                Phone = Phone,
                Temperament = (Temperament) Temperament,
                Biography = Biography,
                Name = Name,
                Height = Height,
                EducationPeriod = EducationPeriod?.FromDto(),
                Id = Id
            };
        }
    }

    public class EducationPeriodDto : RealmObject, IDBEntity
    {
        public EducationPeriodDto()
        {
        }

        public EducationPeriodDto(EducationPeriod period)
        {
            Start = period.Start;
            End = period.End;
        }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        [PrimaryKey] public string Id { get; set; }

        public EducationPeriod FromDto()
        {
            return new EducationPeriod
            {
                End = End.DateTime,
                Start = Start.DateTime
            };
        }
    }
}