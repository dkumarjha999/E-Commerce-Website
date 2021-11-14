using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4MVC.Models
{
    public class City_Name
    {
        public  string  Name{ get; set; }
        public  int Id { get; set; }
        public  int CountryId { get; set; }
        public string Country { get; set; }
        public  int StateId { get; set; }
        public string State { get; set; }
    }
}


