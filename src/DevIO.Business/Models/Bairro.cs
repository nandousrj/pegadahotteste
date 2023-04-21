using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Bairro : Entity
    {
        public int id_bairro { get; set; }
        public string desc_bairro { get; set; }
        public Zona Zona { get; set; }
        public int id_zona { get; set; }
        public bool status { get; set; }
    }
}
