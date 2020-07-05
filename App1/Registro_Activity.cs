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
using Android.Support.V7.App;
using System.Threading;

using ClassEntidades;
using ClassNegocio;

namespace App1
{
    [Activity(Label = "Complete su registro")]
    public class Registro_Activity : AppCompatActivity
    {
        EditText txt_nombre, txt_apellidos, txt_email;
        TextView txt_usuario;

        NegUtilClass NegUtil = new NegUtilClass();
        NegUsuarioClass NegUsuario = new NegUsuarioClass();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.layout_registro);
            Inicio();
        }

        void Inicio()
        {
            Button btnIngresar = FindViewById<Button>(Resource.Id.btn_guardar);
            btnIngresar.Click += BtnIngresar_Click;
            txt_usuario = FindViewById<TextView>(Resource.Id.txt_usuario);
            txt_usuario.Text = Intent.GetStringExtra("usuario");
            txt_nombre = FindViewById<EditText>(Resource.Id.txt_nombre);
            txt_apellidos = FindViewById<EditText>(Resource.Id.txt_apellidos);
            txt_email = FindViewById<EditText>(Resource.Id.txt_email);
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

            if (Valida_datos())
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Registro de usuario")
                        .SetMessage("¿Está seguro que la información ingresada es la correcta?")
                        .SetPositiveButton("Aceptar", delegate {
                            var usu = new EntUsuarioClass
                            {
                                Usuario = txt_usuario.Text,
                                Nombre = txt_nombre.Text,
                                Apellidos = txt_apellidos.Text
                            };

                            if (NegUsuario.Actualizar_registro_usuario(usu))
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
            .SetTitle("Registro completo")
            .SetMessage("Sus datos fueron actualizados correctamente.")
            .SetPositiveButton("Aceptar", delegate {
            });
            var alert = builder.Create();
            alert.SetCancelable(false);
            alert.Show();
        }

        void Incorrecto()
        {
            Toast.MakeText(this, "Complete todos los datos solicitados.", ToastLength.Short).Show();
        }

        /// <summary>
        /// Valida que los datos estén completos
        /// </summary>
        /// <returns></returns>
        private bool Valida_datos()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_nombre.Text) || string.IsNullOrEmpty(txt_apellidos.Text) || string.IsNullOrEmpty(txt_email.Text))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                NegUtil.Control_Errores(ex);
                return false;
            }
        }
    }
}