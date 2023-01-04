using EncuestaCarlosValdez.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncuestaCarlosValdez.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public int Excelente { get; set; }
        public int Bueno { get; set; }
        public int Regular { get; set; }
        public int Mal { get; set; }
        public int MuyMal { get; set; }

        public int Total
        {
            get { return Excelente + Bueno + Regular + Mal + MuyMal; }
        }

        EncuestaService servicio = new();
        public MainViewModel()
        {
           // servicio.Iniciar(); //portate bien weon 
            //servicio.Recibido += Servicio_Recibido;
        }

        private void Servicio_Recibido(Models.Respuesta obj)
        {
            switch (obj.Respuesta1)
            {
                case "Excelente":Excelente++;break;
                case "Bueno": Excelente++; break;
                case "Regular": Excelente++; break;
                case "Mal": Excelente++; break;
                case "MuyMal": Excelente++; break;
                default:
                    break;
            }

            Lanzar();


        }


        public void Lanzar(string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
