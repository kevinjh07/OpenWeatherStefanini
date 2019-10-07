using Android.App;
using Android.Content.PM;
using Android.OS;

namespace OpenWeatherStefanini.Droid
{
    [Activity(Label = "Open Weather", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(0, 0);
        }
    }
}