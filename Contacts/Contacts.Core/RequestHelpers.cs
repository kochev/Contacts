using System;

namespace Contacts.Core
{
    internal class RequestHelpers
    {
        public static TimeSpan ShortTimeout = TimeSpan.FromSeconds(10);
        public static TimeSpan MediumTimeout = TimeSpan.FromSeconds(20);
        public static TimeSpan LongTimeout = TimeSpan.FromSeconds(60);

        #region url's

        public static string ContactsUrl1 =
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-01.json";

        public static string ContactsUrl2 =
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-02.json";

        public static string ContactsUrl3 =
            "https://raw.githubusercontent.com/Newbilius/ElbaMobileXamarinDeveloperTest/master/json/generated-03.json";

        #endregion
    }
}