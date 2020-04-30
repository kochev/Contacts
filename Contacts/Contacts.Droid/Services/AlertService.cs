using System;
using Android.Support.V7.App;
using Android.Widget;
using Contacts.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace Contacts.Droid.Services
{
    public class AlertService : IAlertService
    {
        public void Show(string title, string message)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            context?.RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(context);
                builder.SetPositiveButton("OK", (sender, args) => { (sender as AlertDialog)?.Cancel(); });
                builder.SetCancelable(false);
                builder.SetTitle(title);
                builder.SetMessage(message);

                var dialog = builder.Create();
                dialog.Show();
            });
        }

        public void Show(string title, string message, Action positiveCallback, Action negativeCallback, string positiveText = "Ок",
            string negativeText = "Отмена")
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            context?.RunOnUiThread(() =>
            {
                var alert = new AlertDialog.Builder(context);

                if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
                    alert.SetMessage(message);

                alert.SetTitle(title);
                alert.SetPositiveButton(positiveText,
                    (senderAlert, args) =>
                    {
                        (senderAlert as AlertDialog)?.Hide();
                        positiveCallback?.Invoke();
                    });
                alert.SetNegativeButton(negativeText, (senderAlert, args) =>
                {
                    (senderAlert as AlertDialog)?.Hide();
                    negativeCallback?.Invoke();
                });
                var createdDialog = alert.Create();
                createdDialog.Show();
            });
        }

        public void ShowLongToast(string message)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            context?.RunOnUiThread(() => { Toast.MakeText(context, message, ToastLength.Long).Show(); });
        }
    }
}