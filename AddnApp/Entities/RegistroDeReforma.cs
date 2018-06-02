using Android.Graphics;
using System.Collections.Generic;

namespace AddnApp.Entities
{
    public class RegistroDeReforma
    {
        public string NomeCliente { get; set; }
        public string DataCadastro { get; set; }
        public string Equipamento { get; set; }
        public int NotaFiscal { get; set; }
        public List<Bitmap> ListaDeImagens { get; set; }
        public string NumeroRR { get; set; }
        public string DescricaoRR { get; set; }
        public string NumeroItem { get; set; }
        public string Cidade { get;  set; }
        public string NomeFantasia { get;  set; }
        public string Observacao { get; set; }
    }
}