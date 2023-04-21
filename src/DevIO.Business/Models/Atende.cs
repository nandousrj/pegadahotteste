using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Atende : Entity
    {
        public int id_atende { get; set; }
        public string desc_atende { get; set; }
        public bool status { get; set; }
    }
}
