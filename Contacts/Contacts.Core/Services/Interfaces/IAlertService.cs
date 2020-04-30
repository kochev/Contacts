using System;

namespace Contacts.Core.Services.Interfaces
{
    public interface IAlertService
    {
        void Show(string title, string label);

        void Show(string title, string message, Action positiveCallback, Action negativeCallback,
            string positiveText = "Ок", string negativeText = "Отмена");

        void ShowLongToast(string message);
    }
}