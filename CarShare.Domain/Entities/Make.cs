using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.Domain.Entities
{
    public class Make
    {
        public int MakeID { get; set; }

        public string MakeName { get; set; }

        public Make(int id, string make)
        {
            MakeID = id;
            MakeName = make;
        }
    }
}
