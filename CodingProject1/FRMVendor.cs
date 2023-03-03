using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingProject1
{
    public partial class FRMVendor : Form
    {
        public FRMVendor()
        {
            InitializeComponent();
        }

        //list that is going to be populated by the xml file
        List<Vendor> lstVendors = new List<Vendor>();

        //data is initally saved until it is changed later on
        bool blnIsDataSaved = true;
        //intializing valiables
        public Vendor CurrentVendor = null;
        //this variable corresponds to what vendor is populating the textboxes, the form loads with the first vendor
        //being displayed
        int intVendorPage = 1;
        
        /// <summary>
        /// this method is called from frm order and displays this form in a dialog box
        /// </summary>
        /// <returns></returns>
        public Vendor ShowVendor()
        {
            this.ShowDialog();
            return CurrentVendor;
        }

        //NOTE: Is there a way to make a validation method that checks if the text of a text box was changed
        //that way instead of doing a _TextChanged for each textbox we could do a validation at the start
        //of clicking the Next/Previous button saying if the text was changed

        private void FRMVendor_Load(object sender, EventArgs e)
        {
            //populating the list with the XML Entries
            lstVendors = VendorDB.GetVendors();
            //Clears anything that might be in the Discount combo box and changes the style of the
            //combo box to where the user can only select from the options and can't change the text
            CBODiscount.Items.Clear();
            this.CBODiscount.DropDownStyle = ComboBoxStyle.DropDownList;

            CBODiscount.Items.Add("10 days");
            CBODiscount.Items.Add("15 days");
            CBODiscount.Items.Add("20 days");
            
            //calling this method to populate the form with the current vendor selected corresponding to intVendorPage
            //value
            FillInformation();

        }

        private void FillInformation()
        {
            //clearing any information in the form already
            ClearInformation();

            //counter to synchronize the vendor information with intVendorPage value
            int i = 1;
            //when the last vendor is displayed and the "next" button is pressed, it will loop and display the first vendor
            if (intVendorPage > lstVendors.Count)
            {
                intVendorPage = 1;
            }
            //when the first vendor is displayed and the "previous" button is pressed, it will loop and display the last vendor
            if (intVendorPage < 1)
            {
                intVendorPage = lstVendors.Count;
            }
            //populated the form with each vendor until the desired vendor is shown
            //the desired vendor depends on what value intVendorPage is
            foreach (Vendor v in lstVendors)
            {
                TXTVendorName.Text = v.Name;
                TXTVendorStreet.Text = v.Address;
                TXTVendorCity.Text = v.City;
                TXTVendorState.Text = v.State;
                TXTVendorZip.Text = v.Zip;
                TXTVendorPhone.Text = v.Phone;
                TXTVendorSales.Text = v.YTD.ToString();
                TXTVendorContact.Text = v.Contact;
                TXTVendorComment.Text = v.Comment;
                //i think this could be considered "inefficent" so anyone can try to make this more effeicnt
                if (v.DefaultDiscount == 10)
                    CBODiscount.SelectedIndex = 0;
                if (v.DefaultDiscount == 15)
                    CBODiscount.SelectedIndex = 1;
                if (v.DefaultDiscount == 20)
                    CBODiscount.SelectedIndex = 2;

                //when the counter is equal to the desired Vendor, the loop will stop
                if (i == intVendorPage)
                {
                    break;
                }
                //if the counter is not equal to the desired vendor, the coutner will increase until is it equal to the deisred Vendor
                else
                {
                    i++;
                }
            }
        }
 
        private void BTNNext_Click(object sender, EventArgs e)
        {
            //bool checking if any text boxes of the cbo box has been changed, if anything has changed, then blnIsDataSaved == false
            DetectAnyChange();
            //if data has been changed and has not already been saved
            if (blnIsDataSaved == false)
            {
                string strMessage = "This form contains unsaved data. \n\nDo you wish to save?";
                //displays a dialog button asking to save data that has not already been saved
                DialogResult button = MessageBox.Show(strMessage, "Unsaved Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if the user presses yes, the data is saved and the next vendor is shown
                if (button == DialogResult.Yes)
                {
                    if (IsValidData())
                    {
                        this.SaveData();
                        blnIsDataSaved = true;
                    }
                }
                //if the vendor presses no, the data is not saved and the next vendor is shown
                if (button == DialogResult.No)
                {
                    blnIsDataSaved = true;
                }
                //not sure if a cancel button is necessary here
            }
            //showing the next vendor once the dialoge box is answered
            intVendorPage++;
            FillInformation();
        }

        private void BTNPrevious_Click(object sender, EventArgs e)
        {
            //bool checking if any text boxes of the cbo box has been changed, if anything has changed, then blnIsDataSaved == false
            DetectAnyChange();
            //if data has been changed and has not already been saved
            if (blnIsDataSaved == false)
            {
                string strMessage = "This form contains unsaved data. \n\nDo you wish to save?";

                DialogResult button = MessageBox.Show(strMessage, "Unsaved Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if the vendor presses yes, the data is saved and the previous vendor is shown
                if (button == DialogResult.Yes)
                {
                    if (IsValidData())
                    {
                        this.SaveData();
                        blnIsDataSaved=true;
                    }
                }
                //if the vendor presses no, the data is not saved and the previous vendor is shown
                if (button == DialogResult.No)
                {
                    blnIsDataSaved = true;
                }
                //not sure if a cancel button is necessary here
            }
            //showing the previous vendor once the dialoge box is answered
            intVendorPage--;
            FillInformation();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void ClearInformation()
        {
            TXTVendorName.Clear();
            TXTVendorStreet.Clear();
            TXTVendorCity.Clear();
            TXTVendorState.Clear();
            TXTVendorZip.Clear();
            TXTVendorPhone.Clear();
            TXTVendorSales.Clear();
            TXTVendorContact.Clear();
            TXTVendorComment.Clear();
        }

        private bool DetectAnyChange()
        {
            //this could be inefficent so feel free to try and improve
            int i = 1;
            foreach (Vendor v in lstVendors)
            {
                if (i == intVendorPage)
                {
                    if (TXTVendorName.Text != v.Name) blnIsDataSaved = false;
                    if (TXTVendorStreet.Text != v.Address) blnIsDataSaved = false;
                    if (TXTVendorCity.Text != v.City) blnIsDataSaved = false;
                    if (TXTVendorState.Text != v.State) blnIsDataSaved = false;
                    if (TXTVendorZip.Text != v.Zip) blnIsDataSaved = false;
                    if (TXTVendorPhone.Text != v.Phone) blnIsDataSaved = false;
                    if (TXTVendorSales.Text != v.YTD.ToString()) blnIsDataSaved = false;
                    if (TXTVendorContact.Text != v.Contact) blnIsDataSaved = false;
                    if (TXTVendorComment.Text != v.Comment) blnIsDataSaved = false;
                    if (CBODiscount.SelectedIndex == 0 && v.DefaultDiscount != 10) blnIsDataSaved = false;
                    if (CBODiscount.SelectedIndex == 1 && v.DefaultDiscount != 15) blnIsDataSaved = false;
                    if (CBODiscount.SelectedIndex == 2 && v.DefaultDiscount != 20) blnIsDataSaved = false;
                }
                i++;
            }
            return blnIsDataSaved;
        }

        private bool IsValidData()
        {
            return Validator.IsPresent(TXTVendorName) && Validator.IsPresent(TXTVendorStreet) && Validator.IsPresent(TXTVendorCity)
                && Validator.IsPresent(TXTVendorState) && Validator.IsPresent(TXTVendorZip) && Validator.IsPresent(TXTVendorPhone)
                && Validator.IsPresent(TXTVendorSales) && Validator.IsPresent(TXTVendorContact) && Validator.IsComboPresent(CBODiscount) && Validator.IsInteger(TXTVendorSales);
        }

        private void SaveData()
        {
            if (IsValidData())
            { 
                //saves data to the form
                int i = 1;
                //setting the values in the list to equal to the values in the text boxes
                foreach (Vendor v in lstVendors)
                {
                    //will loop until the desired vendor page is displayed
                    if (i == intVendorPage)
                    {
                        v.Name = TXTVendorName.Text;
                        v.Address = TXTVendorStreet.Text;
                        v.City = TXTVendorCity.Text;
                        v.State = TXTVendorState.Text;
                        v.Zip = TXTVendorZip.Text;
                        v.Phone = TXTVendorPhone.Text;
                        v.YTD = Convert.ToDecimal(TXTVendorSales.Text);
                        v.Contact = TXTVendorContact.Text;
                        v.Comment = TXTVendorComment.Text;
                        //again, this could be inefficent so feel free to try and make it more efficient
                        if (CBODiscount.SelectedIndex == 0)
                            v.DefaultDiscount = 10;
                        if (CBODiscount.SelectedIndex == 1)
                            v.DefaultDiscount = 15;
                        if (CBODiscount.SelectedIndex == 2)
                            v.DefaultDiscount = 20;
                    }
                    i++;
                }
                //saves data to the xml file
                VendorDB.SaveVendors(lstVendors);
            }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
