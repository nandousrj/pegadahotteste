using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Idioma : Entity
    {
        public int id_idioma { get; set; }
        public string desc_idioma { get; set; }
        public bool status { get; set; }
    }
}
