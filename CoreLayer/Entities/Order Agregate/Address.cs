using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Order_Agregate
{
    public class Address
    {
        public Address()
        {

        }
        public Address(int id, string firstName, string lastName, string street, string city, string country)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Country = country;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public string Country { get; set; }
    }
}
