using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Event_Organizer
{
    class EventManager
    {
        private string eventName;
        private double costPerPerson;
        private double feePerPerson;

        ParticipantManager participantManager = new ParticipantManager();

        #region Constructor
        public EventManager()
        {
            participantManager = new ParticipantManager();
        }

        #endregion

        #region Properties
        public ParticipantManager Participants
        {
            get { return participantManager; }
        }

        public string EventName
        { 
            get { return eventName; } 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                    eventName = value;
            }
        }

        // This property handles the fee per person for the event.
        public double FeePerPerson
        {
            get { return feePerPerson; }
            set
            {
                if (value >= 0.0)
                    feePerPerson = value;
            }
        }

        // this one handles the cost per person.
        public double CostPerPerson
        {
            get { return costPerPerson; }
            set
            {
                if (value >= 0.0)
                    costPerPerson = value;
            }
        }

        #endregion

        #region Other Methods
        // this 2 methos calculates the total cost or the total fee
        public double CalcTotalCost() 
        {
            return Participants.Count * costPerPerson; 
        }

        public double CalcTotalFee() 
        { 
            return Participants.Count * feePerPerson;
        }
        #endregion

    }
}
