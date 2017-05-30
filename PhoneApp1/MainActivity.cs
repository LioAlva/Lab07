using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp1
{
    [Activity(Label = "PhoneApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers=
        new System.Collections.Generic.List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var ValActivityButton = FindViewById<Button>(Resource.Id.btnValActividad);

            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
            string TranslatedNumber = string.Empty;
            CallButton.Enabled = false;
            CallHistoryButton.Enabled = false;

            TranslateButton.Click += (object sender, System.EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.Text = $"Llamar al {TranslatedNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallHistoryButton.Click += (object sender, System.EventArgs e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);
            };

            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al numero  {TranslatedNumber}?");

                CallDialog.SetNeutralButton("Llamar", delegate {

                    PhoneNumbers.Add(TranslatedNumber);
                    CallHistoryButton.Enabled = true;

                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);

                    CallIntent.SetData(Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));
                    StartActivity(CallIntent);

                });

               

                CallDialog.SetNegativeButton("Cancelar", delegate { });

                CallDialog.Show();

            };

            ValActivityButton.Click += (object sender, System.EventArgs e) =>
            {
                /*var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity));
                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers);
                StartActivity(Intent);*/
                var Intent = new Android.Content.Intent(this,typeof(DisenioAndroid));
                StartActivity(Intent);

            };


            //Validate();
        }

    }
}

