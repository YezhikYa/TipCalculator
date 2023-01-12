using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialForMe
{
    [Activity(Label = "VeryUsefulActivity")]
    public class VeryUsefulActivity : Activity
    {
        private TextView tvQuestion;
        private EditText etPrecentage;
        private Button btnReturn;

        private int precentage;
        private string question;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TipCalculator);
            // Create your application here

            InitializeView();
        }
        private void InitializeView()
        {
            question = Intent.GetStringExtra("QUESTION");

            tvQuestion = FindViewById<TextView>(Resource.Id.tvQuestion);
            etPrecentage = FindViewById<EditText>(Resource.Id.etPrecentage);
            btnReturn = FindViewById<Button>(Resource.Id.btnReturn);

            tvQuestion.Append(question);

            btnReturn.Click += btnReturn_click;
        }
        private void btnReturn_click(object sender, EventArgs e)
        {
            if (etPrecentage.Text != "")
                precentage = int.Parse(etPrecentage.Text);
            if (etPrecentage.Text != "")
            {
                if (precentage > 100 || precentage < 0)
                {
                    etPrecentage.Text = "";
                    Toast.MakeText(this, "The number must be between 0 - 100", ToastLength.Long).Show();
                }
                else
                {
                    Intent intent = new Intent();
                    intent.PutExtra("PRECENTAGE", precentage);
                    SetResult(Result.Ok, intent);
                    Finish();
                }
            }
            else
                Toast.MakeText(this, "You need to type a precent", ToastLength.Short).Show();
        }
    }
}