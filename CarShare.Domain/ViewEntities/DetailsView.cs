using CarShare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.Domain.ViewEntities
{
    public class DetailsView
    {

        public Car Car { get; set;}

        public User User { get; set;}

        public bool DetailsRequested { get; set; }
    }
}
