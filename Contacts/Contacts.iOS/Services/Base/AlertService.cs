using System;
using Contacts.Core.Services.Interfaces;
using UIKit;

namespace Contacts.iOS.Services.Base
{
    public class AlertService : IAlertService
    {
        public void Show(string title, string message)
        {
            var alert = new UIAlertView();
            alert.InvokeOnMainThread(() =>
            {
                alert.Message = message;
                alert.Title = title;
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.AddButton("OK");
                alert.CancelButtonIndex = 0;
                alert.Clicked += delegate(object sender, UIButtonEventArgs btnArgs)
                {
                    if (btnArgs.ButtonIndex == 1)
                    {
                    }
                };
                alert.Show();
            });
        }

        public void Show(string title, string message, Action positiveCallback, Action negativeCallback, string positiveText = "Ок",
            string negativeText = "Отмена")
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(negativeText, UIAlertActionStyle.Default,
                action => negativeCallback?.Invoke()));
            alertController.AddAction(UIAlertAction.Create(positiveText, UIAlertActionStyle.Default,
                action => positiveCallback?.Invoke()));

            Extensions.TopViewController.PresentViewController(alertController, true, null);
        }

        public void ShowLongToast(string message)
        {
            //can't rea
        }
    }
}