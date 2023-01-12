using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Text.RegularExpressions;

namespace SpecialForMe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText etSum;
        private Button btnFood;
        private Button btnService;
        private Button btnAtmosphere;

        private int requestCode = 0;

        private int foodPrecentage = 0;
        private int servicePrecentage = 0;
        private int atmospherePrecentage = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitializeViews();
        }

        private void InitializeViews()
        {
            etSum = FindViewById<EditText>(Resource.Id.etSum);
            btnFood = FindViewById<Button>(Resource.Id.btnFood);
            btnService = FindViewById<Button>(Resource.Id.btnService);
            btnAtmosphere = FindViewById<Button>(Resource.Id.btnAtmosphere);

            btnFood.Click += btn_Click;
            btnService.Click += btn_Click;
            btnAtmosphere.Click += btn_Click;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string str = " food?";
            if (sender == btnService)
            {
                str = "service?";
                requestCode = 1;
            }
            else if(sender == btnAtmosphere)
            {
                str = "atmosphere?";
                requestCode = 2;
            }
            Intent intent = new Intent(this, typeof(VeryUsefulActivity));
            intent.PutExtra("QUESTION", str);
            StartActivityForResult(intent, requestCode);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 0)
            {
                foodPrecentage = data.GetIntExtra("PRECENTAGE", 0);
            }
            if (requestCode == 1)
            {
                servicePrecentage = data.GetIntExtra("PRECENTAGE", 0);
            }
            if (requestCode == 2)
            {
                atmospherePrecentage = data.GetIntExtra("PRECENTAGE", 0);
            }
            if (foodPrecentage != 0 && servicePrecentage != 0 && atmospherePrecentage != 0)
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this, 1);
                builder.SetTitle("The tip you have to pay is:");
                int precentageAverage = (foodPrecentage + servicePrecentage + atmospherePrecentage) / 3;
                int tip = (precentageAverage * int.Parse(etSum.Text)) / 100;
                builder.SetMessage(tip.ToString());
                builder.SetPositiveButton("OK", (c, ev) => { });
                builder.Show();
            }
        }
    }
}