using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Organizer
{
    // the participant manger is responsible for manging the list for all the particpants
    class ParticipantManager
    {
        private List<Participant> participants;

        // Constructor that initializes the participants list.
        public ParticipantManager()
        {
            participants = new List<Participant>();
        }

        #region Methos to handel the list
        public Participant GetParticipant(int index)
        {
            if (index < 0 || index >= participants.Count)
                return null;

            return participants[index];

        }

        // Property to get the number of participants in the list.
        public int Count
        {
            get { return participants.Count; }
        }

        // Adds a new participant to the list using the provided things
        public bool AddParticipant(string firstName, string lastName, Address addressin)
        {
            Participant Participant = new Participant(firstName, lastName, addressin);
            participants.Add(Participant);
            return true;
        }

        public bool AddParticipant(Participant ParticipantIn) 
        { 
            if (ParticipantIn == null)
                return false;

            participants.Add(ParticipantIn);
            return true;
        }

        // Replaces the participant at the specified index with a new participant.
        public bool ChangeParticipantAt(Participant ParticipantIn, int index)
        {
            bool ok = true;
            if((ParticipantIn != null) && (CheckIndex(index)))
                participants[index] = ParticipantIn;
            else
                ok = false;
            return ok;
        }

        // Deletes a participant at the specified index.
        public bool DeleteParticipantAt(int index)
        {
            if(CheckIndex(index))
                participants.RemoveAt(index);
            else
                return false;

            return true;
        }

        public string[] GetParticipantsInfo() 
        {
            string[] strInfoStrings = new string[participants.Count];

            int i = 0;
            foreach (Participant ParticipantObj in participants)
            {
                strInfoStrings[i++] = ParticipantObj.ToString();
            }
            return strInfoStrings;
        }
        private bool CheckIndex(int index) 
        { 
            return (index >= 0) && (index < participants.Count);
        }

        #endregion

    }
}
