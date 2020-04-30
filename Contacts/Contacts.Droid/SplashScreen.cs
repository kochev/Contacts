using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Contacts.Droid
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Label = "@string/ApplicationName",
        Theme = "@style/MyTheme.Splash")]
    public class SplashActivity : MvxSplashScreenAppCompatActivity
    {
    }
}