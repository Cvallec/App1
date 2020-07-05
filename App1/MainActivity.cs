using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Threading;
using SQLite;
using ClassEntidades;
using ClassNegocio;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView txt_usuario;
        TextView txt_contrasena;

        NegUtilClass NegUtil = new NegUtilClass();
        NegUsuarioClass NegUsuario = new NegUsuarioClass();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            if (!NegUtil.IniciaBD())
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetMessage("No pudimos iniciar el sistema, ingrese nuevamente")
                        .SetPositiveButton("Aceptar", delegate { });
                var alert = builder.Create();
                alert.SetCancelable(false);
                alert.Show();
            }
            else
            {
                Inicio();
            }
        }

        void Inicio()
        {
            Button btnIngresar = FindViewById<Button>(Resource.Id.btn_ingresar);
            btnIngresar.Click += BtnIngresar_Click;
            Button btnRegistrame = FindViewById<Button>(Resource.Id.btn_registrame);
            btnRegistrame.Click += BtnRegistrame_Click;
            txt_usuario = FindViewById<EditText>(Resource.Id.txt_usuario);
            txt_contrasena = FindViewById<EditText>(Resource.Id.txt_contrasena);
        }

        private void BtnIngresar_Click(object sender, System.EventArgs e)
        {
            var titulo = "Inicio de sesión";
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetTitle(titulo)
                    .SetMessage("¿Está seguro de continuar con el inicio de sesión?")
                    .SetPositiveButton("Aceptar", (senderAlert, args) => {

                        var resultMessage = string.Empty;
                        var dialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                        dialog.SetTitle(titulo);
                        var progressDialog =
                            ProgressDialog.Show(this, "Espere un momento", "Sincronizando...", true, false);
                        progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                        new Thread(new ThreadStart(async delegate {
                            
                            //aquí va el código.

                            RunOnUiThread(() => progressDialog.Dismiss());
                            RunOnUiThread(delegate {
                                dialog.SetMessage(resultMessage);
                                dialog.Show();
                            });
                        })).Start();

                    })
                    .SetNegativeButton("Cancelar", delegate { });
            var alert = builder.Create();
            alert.SetCancelable(false);
            alert.Show();
        }

        private void BtnRegistrame_Click(object sender, System.EventArgs e)
        {
            if (Valida_datos())
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                            .SetMessage("¿Está seguro de seguir con el registro?")
                            .SetPositiveButton("Aceptar", delegate {
                                var usu = new EntUsuarioClass
                                {
                                    guid = Guid.NewGuid().ToString(),
                                    Usuario = txt_usuario.Text,
                                    Contrasena = txt_contrasena.Text
                                };

                                if (NegUsuario.Insertar_Usuario(usu))
                                {
                                    Correcto();
                                }
                                else
                                {
                                    Incorrecto();
                                }
                            })
                            .SetNegativeButton("Cancelar", delegate { });
                var alert = builder.Create();
                alert.SetCancelable(false);
                alert.Show();
            }
            else
            {
                Incorrecto();
            }
        }

        void Correcto()
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                           .SetMessage("El usuario " + txt_usuario.Text + " se ha creado correctamente. \n" +
                           "Ahora debe completar sus datos personales.")
                           .SetPositiveButton("Aceptar", delegate {

                               var intent = new Intent(this, typeof(Registro_Activity));
                               intent.PutExtra("usuario", txt_usuario.Text);
                               StartActivity(intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask));

                           });
            var alert = builder.Create();
            alert.SetCancelable(false);
            alert.Show();
        }

        void Incorrecto()
        {
            //var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
            //               .SetMessage("Se ha producido un error.")
            //               .SetPositiveButton("Aceptar", delegate { });
            //var alert = builder.Create();
            //alert.SetCancelable(false);
            //alert.Show();
            Toast.MakeText(this, "Se ha producido un error", ToastLength.Short).Show();
        }

        /// <summary>
        /// Valida que los datos estén completos
        /// </summary>
        /// <returns></returns>
        private bool Valida_datos()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_usuario.Text) || string.IsNullOrEmpty(txt_contrasena.Text))
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                NegUtil.Control_Errores(ex);
                return false;
            }
        }
    }
}