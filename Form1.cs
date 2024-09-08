using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event_Organizer
{

    public partial class MainForm : Form
    {
        EventManager eventmanager;

        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
            eventmanager = new EventManager();
        }
        #region Intiliaze Update gui
        // Clearing up the ui compents and set to deafualt values
        private void InitializeGUI()
        {
            
            lstParticipants.Items.Clear();
            txtEventName.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtFee.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            cbCountry.DataSource = Enum.GetValues(typeof(Countries));
            lblNumGuest.Text = "0";
            lblTotalCost.Text = "0.0";
            lblTotalFees.Text = "0.0";
            lblProfit.Text = "0.0";
        }

        private void UpdateGUI()
        {
            string[] strInfo = eventmanager.Participants.GetParticipantsInfo();

            if (strInfo != null) 
            {
                lstParticipants.Items.Clear();
                lstParticipants.Items.AddRange(strInfo);
                lblNumGuest.Text = lstParticipants.Items.Count.ToString();
            }

            double totalCost = eventmanager.CalcTotalCost();
            lblTotalCost.Text = totalCost.ToString("0.00");
            lblNumGuest.Text = eventmanager.Participants.Count.ToString();
            
            double totalFees = eventmanager.CalcTotalFee();
            lblTotalFees.Text = totalFees.ToString("0.00");
            lblProfit.Text = (totalFees - totalCost).ToString("0.00");

            
        }
        // The UpdateGUI method refreshes the UI with updated participant information
        // and event costs. It updates the participant list, displays the number of participants,
        // calculates and shows the total cost, total fees, and profit.
        #endregion
        #region Text/Numeric Validation
        // Checks so you only use numbers in the cost and fee inputs
        private bool ValidateNumericInput(string input, string fieldName)
        {
            if (!double.TryParse(input, out double value) || value <= 0)
            {
                MessageBox.Show($"Please enter a valid {fieldName}.", "Error");
                return false;
            }
            return true;
        }

        // Validates the text so it is not null
        private bool ValidateTextInput(string text, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show($"Please provide a valid {fieldName}.", "Error");
                return false;
            }
            return true;
        }
        #endregion
        #region Read cost/fee
        // Reads the cost per person from the inputs
        private bool ReadCostPerPersson()
        {
            double amount = 0.0;
            bool ok = true;

            if (double.TryParse(txtAmount.Text, out amount) && amount >= 0.0)
            {
                eventmanager.CostPerPerson = amount;
            }
            else
            {
                MessageBox.Show("Invalid");
                ok = false;
            }

            return ok;
        }

        // Reads the Fee per person from the inputs
        private bool ReadFeePerPersson()
        {
            double amount = 0.0;
            bool ok = true;

            if (double.TryParse(txtFee.Text, out amount) && amount >= 0.0)
            {
                eventmanager.FeePerPerson = amount;
            }
            else
            {
                MessageBox.Show("Invalid");
                ok = false;
            }

            return ok;
        }
        #endregion
        private bool CreateEvent()
        {
            
            // Validate the event name
            if (!ValidateTextInput(txtEventName.Text, "event name"))
            {
                return false;
            }
            // Validate numeric inputs for cost amount and fee amount
            if (!ValidateNumericInput(txtAmount.Text, "cost amount") ||
                !ValidateNumericInput(txtFee.Text, "fee amount"))
            {
                return false;
            }

            // Parse Event Name
            string eventName = txtEventName.Text; // Assuming event name is a string
            bool costAmountsOk = ReadCostPerPersson();
            bool feeAmountsOk = ReadFeePerPersson();

           

            if (costAmountsOk && feeAmountsOk)
            {
                UpdateGUI();
                MessageBox.Show($"{eventName} Created for guests!");
                return true;
            }

            return false;
        }

        // This method attempts to read and process input data for a Participant object.
        private bool ReadInput(ref Participant participant)
        {
            bool ok = ReadParticipantData(ref participant);
            if (ok)
            {
                eventmanager.Participants.AddParticipant(participant);
            }
            else
            {
                // If the data is invalid error message is displayed
                string strMessage = "First name, last name and city is required inputs";
                MessageBox.Show(strMessage);
            }
            return ok;
        }

        // This method reads data from input fields and assigns it to a Participant object.
        private bool ReadParticipantData(ref Participant participant)
        {
            participant.FirstName = txtFirstName.Text;
            participant.LastName = txtLastName.Text;

            Address address = ReadAddress();
            participant.Address = address;

            bool ok = address.Validate();
            return ok;
        }

        // This method reads the adress related data from the inputs
        private Address ReadAddress()
        {
            Address address = new Address();

            address.Street = txtStreet.Text;
            address.City = txtCity.Text;
            address.ZipCode = txtZipCode.Text;

            address.Country = (Countries) cbCountry.SelectedIndex;

            return address;
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Countries selectedCountry = (Countries)cbCountry.SelectedItem;
            
        }

        // clears the list and calls the CreateEvent method
        private void btnCreateParty_Click(object sender, EventArgs e)
        {
            lstParticipants.Items.Clear();
            CreateEvent();
        }

       
        // When press the add button updates the gui with new information
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Participant participant = new Participant();
            if (ReadInput(ref participant))
            {
                UpdateGUI();
            }
            
        }

        // Replace the existing participant with the updated one at the given index when you press
        // the change button
        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = IsListBoxItemSelected();
            if (index < 0)
                return;

            Participant participant = eventmanager.Participants.GetParticipant(index);

            if (ReadParticipantData(ref participant))
            {
                eventmanager.Participants.ChangeParticipantAt(participant, index);
                UpdateGUI();
            }

        }

        // Delete the participants that is selected when pressing delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = IsListBoxItemSelected();
            if (index < 0)
                return;

            eventmanager.Participants.DeleteParticipantAt(index); 
            UpdateGUI();
        }

        // checks if the user has selected a participant from the list before pressing delete
        private int IsListBoxItemSelected()
        {
            int index = lstParticipants.SelectedIndex;
            
            if (lstParticipants.SelectedIndex < 0)
            {
                MessageBox.Show("Select an guest in the list!");
                return -1;
            }
            return index;
        }

        #region Unused
        // all the unused objects
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void lstParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
