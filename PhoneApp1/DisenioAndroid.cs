using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp1
{
    [Activity(Label = "Validar Actividad",Icon = "@drawable/icon")]
    public class DisenioAndroid : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DisenioAndroid);

            var btnValidate = FindViewById<Button>(Resource.Id.btnValidacion);
            var lblCorreo = FindViewById<EditText>(Resource.Id.txtCorreo);
            var lblPassword = FindViewById<EditText>(Resource.Id.txtClave);
            /*btnValidate.Enabled = false;
            if (lblCorreo.Text.Trim()!=string.Empty && lblPassword.Text.Trim()!=string.Empty) {
                btnValidate.Enabled = true;
               
            }*/

            btnValidate.Click += (object sender, System.EventArgs e) =>
            {
                Validate(lblCorreo.Text.Trim(), lblPassword.Text.Trim());
            };


        }

        
           

        private async void Validate(String Correo, string Clave)
        {
            var TextValidator = FindViewById<TextView>(Resource.Id.TextValidator);
            SALLab07.ServiceClient ServiceClient = new
                SALLab07.ServiceClient();

            string myDevice = Android.Provider
                 .Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab07.ResultInfo Result = await
            ServiceClient.ValidateAsync(Correo, Clave, myDevice);
            string mensaje = ($"{Result.Status} \n{Result.Fullname}\n {Result.Token}");

            var Builder = new Notification.Builder(this).SetContentTitle("Validacion de la Actividad").
                SetContentText(mensaje).SetSmallIcon(Resource.Drawable.Icon);

            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Builder.SetCategory(Notification.CategoryMessage);

                var ObjectNotification = Builder.Build();
                var Manager = GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
                Manager.Notify(0, ObjectNotification);
            }
            else {
                TextValidator.Text = mensaje;
            }

        }
    }
}