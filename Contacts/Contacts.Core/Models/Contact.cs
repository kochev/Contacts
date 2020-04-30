using System;
using System.ComponentModel;

namespace Contacts.Core.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public float Height { get; set; }
        public string Biography { get; set; }
        public Temperament Temperament { get; set; }
        public EducationPeriod EducationPeriod { get; set; }
    }

    public enum Temperament
    {
        [Description("melancholic")] Melancholic = 1,
        [Description("phlegmatic")] Phlegmatic,
        [Description("sanguine")] Sanguine,
        [Description("choleric")] Choleric
    }


    public class EducationPeriod
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}