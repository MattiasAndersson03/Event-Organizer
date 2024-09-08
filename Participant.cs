using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Organizer
{
    class Participant
    {
        private string firstName = string.Empty;
        private string lastName = "";

        private Address address;

        #region Constuctor

        // Default constructor that initializes the address field with a new Address object.
        public Participant()
        {
            address = new Address();
        }

        public Participant(string firstName, string lastName, Address adr)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            if (adr != null)
                address = adr;
            else
                address = new Address();
        }

        public Participant(Participant theOther)
        {
            this.firstName = theOther.firstName;
            this.lastName = theOther.lastName;
            this.address = new Address(theOther.Address);
        }
        #endregion

        #region Properties

        // Property for getting and setting the participant's address.
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FullName
        {
            get { return firstName + " " + lastName; }
        }

        #endregion

        #region Other Methods

        // Ensures that the address is valid and both the first name and last name are not empty.
        public bool Validate()
        {
            bool addrOK = address.Validate();
            bool nameOK = (!string.IsNullOrEmpty(firstName)) && !string.IsNullOrEmpty(lastName);
            return addrOK && nameOK;
        }
        // Override of the ToString method to return a formatted string representation of the participant.
        public override string ToString()
        {
            string strOut = string.Format("{0, -20} {1}", FullName,Address.ToString());
            return strOut;
        }
        #endregion


    }
}
