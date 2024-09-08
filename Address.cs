using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Organizer
{
    class Address
    {
        private string street;
        private string zipCode;
        private string city;
        private Countries country;

        #region Constructor

        // Constructor that initializes all the fields of the Address class.
        public Address(string street, string zip, string city, Countries country)
        {
            this.street = street;
            this.zipCode = zip;
            this.city = city;

            this.country = country;
        }

        public Address(string street, string zip, string city) :
            this(street, zip, city, Countries.Sweden)
        {
        }

        // Default constructor that initializes the address with empty strings and the default country.
        public Address() : this(string.Empty, string.Empty, string.Empty) 
        { 
        }

        public Address(Address theOther)
        {
            this.street = theOther.street;
            this.zipCode = theOther.zipCode;
            this.city = theOther.city;
            this.country = theOther.country;

        }

        #endregion
        #region Properties

        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string City
        {
            get { return city; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    city = value;
            }
        }

        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        public Countries Country
        {
            get { return country; }
            set { country = value; }
        }

        #endregion

        // Method to convert the country enum value to a string and replace underscores with spaces.
        public string GetCountryString()
        {
            string strCountry = country.ToString();
            strCountry = strCountry.Replace("_", " ");
            return strCountry;
        }

        public bool Validate()
        {
            bool cityOK = !string.IsNullOrEmpty(city);
            return cityOK;
        }

        public string GetAdreessLabel()
        {
            string strOut = street + Environment.NewLine;
            strOut += zipCode + " " + city;
            return strOut;
        }

        // Override of the ToString method to return a formatted string representation of the address.
        public override string ToString()
        {
            string strOut = string.Format("{0, -25} {1, -8} {2, -10} {3}",
                street, zipCode, city, GetCountryString());
            return strOut;
        }
         
        
    }
}
