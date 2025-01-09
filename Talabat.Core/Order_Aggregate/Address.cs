using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregate
{
    public class Address
    {
        public Address(string fName, string lName, string country, string city, string street)
        {
            FName = fName;
            LName = lName;
            Country = country;
            this.city = city;
            this.street = street;
        }
        public Address()
        {
            
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }
}
