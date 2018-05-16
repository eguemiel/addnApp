using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.ApiModels
{
    public class RrResponse
    {
        public string NomeCliente { get; set; }
        public string NomeFantasia { get; set; }
        public string Cidade { get; set; }
        public string RR { get; set; }
        public string DescricaoEquipamento { get; set; }
        public int NumeroNF { get; set; }
        public string DataAbertura { get; set; }
        public bool Success { get; internal set; }
    }
}
