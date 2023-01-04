using EncuestaCarlosValdez.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EncuestaCarlosValdez.Services
{
    public class EncuestaService
    {
        public event Action<Respuesta>? Recibido;

        private string pregunta = "";


        HttpListener listener = new();

        public EncuestaService()
        {
            listener.Prefixes.Add("http://*:10000/encuesta/");

        }


        public void Iniciar()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                listener.BeginGetContext(ContextoRecibido, null);
            }
        }
        public void EstablecerPregunta(Pregunta p)
        {
            pregunta = JsonConvert.SerializeObject(p);
        }


        private void ContextoRecibido(IAsyncResult ar)
        {
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ContextoRecibido, null);

            if (context.Request.Url != null)
            {
                if (context.Request.Url.LocalPath == "/encuesta/preguntas")
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(pregunta);
                    context.Response.ContentType = "application/json"; 
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = 200;
                    context.Response.Close();
                }
                else if (context.Request.HttpMethod == "POST" && context.Request.Url.LocalPath == "/encuesta/responder")
                {
                    var stream = new StreamReader(context.Request.InputStream);
                    var json = stream.ReadToEnd();
                    Respuesta? res = JsonConvert.DeserializeObject<Respuesta>(json);
                    context.Response.StatusCode = 200;

                    if (res != null)
                        Recibido?.Invoke(res);

                    context.Response.Close();
                }

                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.Close();
                }

            }
        }
    }
}
