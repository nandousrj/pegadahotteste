using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Sexo : Entity
    {
        public int id_sexo { get; set; }
        public string desc_sexo { get; set; }
        public  bool status { get; set; }
    }
}
