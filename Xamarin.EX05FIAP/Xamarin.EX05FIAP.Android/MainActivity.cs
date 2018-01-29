using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Media;
using Xamarin.EX05FIAP.API;
using Android.Content;

[assembly: Dependency(typeof(Xamarin.EX05FIAP.Droid.MainActivity))]
namespace Xamarin.EX05FIAP.Droid
{
    [Activity(Label = "Xamarin.EX05FIAP", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void CapturarFoto()
        {
            var context = Forms.Forms.Context as Activity;

            var captura = new MediaPicker(context);

            var intent = captura.GetTakePhotoUI(new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear,
                Name = string.Format("foto_{0}.jpg", DateTime.Now.ToString()),
                Directory = "Fiap"
            });
            context.StartActivityForResult(intent, 1);
        }

        public void SelecionarFoto()
        {
            throw new NotImplementedException();
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled) return;

            var mediaPath = await data.GetMediaFileExtraAsync(Forms.Forms.Context);
            MessagingCenter.Send<ICamera, string>(this, "cameraFoto", mediaPath.Path);
        }
    }
}


