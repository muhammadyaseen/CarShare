using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CarShare.WebUI.Models
{
    public class RequestForm
    {
        [HiddenInput]
        public int OwnerID { get; set; }

        [HiddenInput]
        public int RequesterID { get; set; }

        [HiddenInput]
        public int CarID { get; set; }

    }
}
