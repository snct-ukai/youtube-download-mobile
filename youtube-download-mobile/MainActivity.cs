using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using VideoLibrary;
using System;

namespace youtube_download_mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string folderpath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

        public Button download { get; private set; }
        public EditText url { get; private set; }
        public string[] link = new string[2];

        /*private void buttonTF()
        {
            if (url.Text.StartsWith("https://youtu")|| url.Text.StartsWith("https://www.youtu"))
            {
                download.Enabled = true;
            }
            else
            {
                download.Enabled = false;
            }
        }*/

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //buttonTF();
            download = FindViewById<Button>(Resource.Id.downloadbutton);
            url = FindViewById<EditText>(Resource.Id.url);

            //url.BeforeTextChanged += Url_BeforeTextChanged;

            download.Click += Download_Click;

            Toast.MakeText(this, "ダウンロードが完了しました", ToastLength.Long).Show();
        }

        /*private void Url_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            buttonTF();
        }*/

        private void Download_Click(object sender, System.EventArgs e)
        {
            if (url.Text != "")
            {
                if (url.Text.Contains("&list"))
                {
                    link = url.Text.Split("&list");
                }
                else
                {
                    link[0] = url.Text;
                }
                var video = YouTube.Default.GetVideo(link[0]);
                try
                {
                    System.IO.File.WriteAllBytes(folderpath + "/" + video.FullName, video.GetBytes());
                    Toast.MakeText(this, "ダウンロードが完了しました", ToastLength.Long).Show();
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "ダウンロードできませんでした", ToastLength.Long).Show();
                }
            }
        }
    }
}