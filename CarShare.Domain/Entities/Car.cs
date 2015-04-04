using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarShare.Domain.Entities
{
    public class Car
    {
        
        // pk
        [HiddenInput(DisplayValue = false)]
        public int CarID { get; set; }

        //Fks
        [NotMapped]
        public int? CarScheduleID { get; set; }

        public int OwnerID { get; set; }

        public int MakeID { get; set; }

        [NotMapped]
        public string Make { get; set; }
        [NotMapped]
        public string Model { get; set; }

        public int ModelID { get; set; }

        //attrs

        [Required(ErrorMessage = "Please give your Car a title")]
        [StringLength(50, ErrorMessage = "Title must be under 50 characters in length")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [StringLength(200, ErrorMessage = "Maximum Length is 300 characters")]
        [DataType(DataType.MultilineText)]
        public string CarDesc { get; set; }

        public int MaxCapacity { get; set; }

        public string RegNo { get; set; }


        [NotMapped]
        public IEnumerable<CarImage> CarImageList { get; set; }

        //For Aleesha's Maps work
        public float Lat { get; set; }
        public float Lng { get; set; }

        public Car()
        {
            
        }
    }
}
