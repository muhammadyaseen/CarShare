using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarShare.WebUI.Models
{
    public class SearchForm
    {
        public int SelectedModelID { get; set; }

        public IEnumerable<SelectListItem> Models { get; set; }

        public int SelectedMakeID { get; set; }

        public IEnumerable<SelectListItem> Makes { get; set; }

        public string SelectedLocation { get; set; }

        public List<SelectListItem> Location = new List<SelectListItem>
        {
            new SelectListItem { Text = "Islamabad" , Value = "Islamabad" },
            new SelectListItem { Text = "Lahore" , Value = "Lahore" },
            new SelectListItem { Text = "Karachi" , Value = "Karachi" },
            new SelectListItem { Text = "Quetta" , Value = "Quetta" },
            new SelectListItem { Text = "Peshawar" , Value = "Peshawar" },
        };

        public string Keyword { get; set; }


    }
}
