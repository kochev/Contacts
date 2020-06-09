using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Contacts.Core
{
    public class LocalSettings
    {
        #region Setting Constants

        private const string LastUpdateDatetimeKey = "last_update_datetime";

        #endregion

        private static ISettings AppSettings => CrossSettings.Current;

        public static DateTime LastUpdateDatetime
        {
            get => AppSettings.GetValueOrDefault(LastUpdateDatetimeKey, DateTime.MinValue);
            set => AppSettings.AddOrUpdateValue(LastUpdateDatetimeKey, value);
        }
    }
}