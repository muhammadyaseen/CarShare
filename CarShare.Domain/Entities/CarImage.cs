using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.Domain.Entities
{
    public class CarImage
    {
        public int CarImageID;
        public int CarID;
        public string Image;

        public CarImage(int ciid, int cid, string img)
        {
            // TODO: Complete member initialization
            this.CarImageID = ciid;
            this.CarID = cid;
            this.Image = img;
        }
    }
}
