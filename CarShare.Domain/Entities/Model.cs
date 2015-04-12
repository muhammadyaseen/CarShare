using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.Domain.Entities
{
    public class Model
    {
        public int ModelID { get; set; }

        public string ModelName { get; set; }

        public Model(int id, string model)
        {
            ModelID = id;
            ModelName = model;
        }
    }
}
