using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingProject1
{
    public static class Validator
    {
        /// <summary>
        /// validation to make sure the address given is either in college station, tx or bryan, tx
        /// </summary>
        /// <param name="Dcity"></param>
        /// <param name="Dstate"></param>
        /// <returns></returns>
        public static bool IsDeliveryPossible(TextBox Dcity, TextBox Dstate)
        {
            //triming the text and setting it to upper case so it can be checked
            string strDCity = Dcity.Text.Trim().ToUpper();
            strDCity = strDCity.Replace(" ", "");
            string strDState = Dstate.Text.Trim().ToUpper();
            strDState = strDState.Replace(" ", "");
            //checking if the user input bryan or college station as the delivery address
            if ((strDCity != "BRYAN") && (strDCity != "COLLEGESTATION"))
            {
                MessageBox.Show("This delivery is not possible. Deliveries are limited to Bryan, TX and College Station, TX");
                Dcity.Focus();
                return false;
            }
            //checking if the user input tx or texas as their address
            if (strDState != "TX" && strDState != "TEXAS")
            {
                MessageBox.Show("This delivery is not possible. Deliveries are limited to Bryan, TX and College Station, TX");
                Dstate.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// validation to make sure TXTQuantity is an interger value
        /// </summary>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public static bool IsInteger(TextBox textbox)
        {
            int intTestValue = 0;
            if (!Int32.TryParse(textbox.Text, out intTestValue))
            {
                MessageBox.Show(textbox.Tag.ToString() + " must be a whole number.", "Entry Error");
                textbox.Clear();
                textbox.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// validation to make sure the given text box has a value other than ""
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static bool IsPresent(TextBox textBox)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(textBox.Tag.ToString() + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// validation to make sure the combo boxes have a selected value
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        public static bool IsComboPresent(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show(comboBox.Tag.ToString() + " is a required field.", "Entry Error");
                comboBox.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// validation to make sure the quantity that the user entered is within range
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="decMin"></param>
        /// <param name="decMax"></param>
        /// <returns></returns>
        public static bool IsWithinRange(TextBox textBox, decimal decMin, decimal decMax)
        {
            decimal decTestValue = Convert.ToDecimal(textBox.Text);
            if(decTestValue < decMin)
            {
                MessageBox.Show(textBox.Tag + " must be equal to or greater than 1.", "Entry Error");
                textBox.Focus();
                return false;
            }
            if(decTestValue > decMax)
            {
                MessageBox.Show(textBox.Tag + " quantity too large", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        //public static bool VendorInfoChange(TextBox textBox)
        //{
        //    
           
        //}


    }
}
