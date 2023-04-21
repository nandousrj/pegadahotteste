using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Trabalho : Entity
    {
        public int id_trabalho { get; set; }
        public string desc_trabalho { get; set; }
    }
}
