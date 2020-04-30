using System.Threading.Tasks;
using Contacts.Core.Services.Implementation;
using Contacts.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace Contacts.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        private string _errorSubtitle;
        private string _errorTitle;
        private bool _isUiEnabled;
        private bool _showError;
        private bool _showProgress;
        private string _title;

        public BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            ViewDispatcher = Mvx.IoCProvider.Resolve<IMvxViewDispatcher>();
            AlertService = Mvx.IoCProvider.Resolve<IAlertService>();
            ApiService = Mvx.IoCProvider.Resolve<IApiService>();
            DatabaseService = Mvx.IoCProvider.Resolve<DatabaseService>();
        }

        public DatabaseService DatabaseService { get; }
        public IApiService ApiService { get; }

        public IAlertService AlertService { get; }

        public IMvxViewDispatcher ViewDispatcher { get; }
        public IMvxNavigationService NavigationService { get; }

        public virtual MvxAsyncCommand CloseCommand => new MvxAsyncCommand(CloseThisAsync);

        public bool IsUiEnabled
        {
            get => _isUiEnabled;
            set => SetProperty(ref _isUiEnabled, value);
        }

        public bool ShowError
        {
            get => _showError;
            set => SetProperty(ref _showError, value);
        }

        public string ErrorTitle
        {
            get => _errorTitle;
            set => SetProperty(ref _errorTitle, value);
        }

        public string ErrorSubtitle
        {
            get => _errorSubtitle;
            set => SetProperty(ref _errorSubtitle, value);
        }

        public bool ShowProgress
        {
            get => _showProgress;
            set => SetProperty(ref _showProgress, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public virtual async Task CloseThisAsync()
        {
            await NavigationService.Close(this);
        }
    }
}