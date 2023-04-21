using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Zona : Entity
    {
        public int id_zona { get; set; }
        public string desc_zona { get; set; }
        public bool status { get; set; }
    }
}
