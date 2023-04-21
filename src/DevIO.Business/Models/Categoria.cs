using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Categoria : Entity
    {
        public int id_categoria { get; set; }
        public string desc_categoria { get; set; }
        public bool status { get; set; }
    }
}
