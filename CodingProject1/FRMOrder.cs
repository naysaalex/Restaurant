using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//AUTHOR:   Naysa Alex, Marcus Chan, Erin Jiang, Jonathan Pasala
//COURSE:   Istm 250 501
//FORM:  FRMCodingProject2
//PURPOSE:  This program is designed to help Edward Kirby with taking orders for 
//Kirby's Deli. The form allows cusomters to enter their name, address, phone number,
//whether or not they want delivery, order items, quantity of orders, and displays 
//the full order information for Mr. Kirby to see. Mr. Kirby will also be able to keep
//track of his inventory levels after each order is processed.
//INPUT:  Customer information including name in TXTName, address in TXTStreetAddress,
//city in TXTCity, state in TXTState, zipcode in TXTZip, phone number in TXTPhonenNumber,
//subdivision in TXTSubdivision. Delivery information that includes the input in TXTDStreetAddress,
//TXTDCity, TXTDState, TXTDZip, and TXTDSubdivision. To display order information in
//the list box, input will include a selection from CBOItems, a selection from CBOBreadCrust, 
//and input in the TXTQuantity textbox. Starting inventory amounts will be loaded into the inventory
//form using an array. 
//PROCESS:  When the form first loads, Delivery Information group box will not be available for user
//to input information, the program will determine whether or not CBXDelivery has been checked. 
//Once the user inputs all the information of an order, the subtotal will be calculated (without tax),
//the grand total will be calculated by adding all subtotals of an order together and adding an 8.25%
//tax rate. Once the process button is clicked, it will use the StoreOrder method to input data
//into the inventory form where the amount of ingredients used in each order will be calculated.
//OUTPUT:   Once an item is selected in CBOItems, a picture of either a sandwich or pizza will be
//displayed in the picture box respectively. Subtotal and Grand total amounts will be displayed
//in their respective textboxes once the Add button is clicked. The list box will also display all
//order information once the Add button is clicked. On the Inventory form, the ingredient amounts 
//will be updated and displayed for Mr. Kirby to see after each order. 
//HONOR CODE: “On my honor, as an Aggie, I have neither given  
//   nor received unauthorized aid on this academic  
//   work.” 
namespace CodingProject1
{
    public partial class FRMOrder : Form
    {
        public FRMOrder()
        {
            InitializeComponent();
        }

        //arrays that stores the Items, Bread types, and Crust types
        string[] strItems = { "Ham & Swiss sandwhich", "Turkey & Provolone sandwhich", "BLT sandwhich", "Med. cheese pizza", "Med. pepperoni pizza", "Med. supreme pizza" };
        string[] strBreadTypes = { "White", "Pumpernickel", "Rye", "Sourdough", "Multigrain" };
        string[] strCrustTypes = { "Original", "Pan", "Thin", "Wheat" };

        //grand total is a global variable as it has a running total
        decimal decGrandTotal = 0m;

        //global lists to store all orders for the day
        public static  List<int> lstItemsOrdered = new List<int>();
        public static  List<int> lstNumberOfItemsOrdered = new List<int>();

        /// <summary>
        /// When the form loads, the Combo boxes are cleared and made so the user cannot effcet them, they are then populated
        /// Snadwhiches and bread types are initalliy populated into the combo boxes
        /// The delivery group box is disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FRMOrder_Load(object sender, EventArgs e)
        {
            //When the form loads, clear anything that may be in the Items combo box
            CBOItems.Items.Clear();
            this.CBOItems.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CBOBreadCrust.DropDownStyle = ComboBoxStyle.DropDownList;

            //Populate the Items combo box with all the items in the strItems array
            foreach (string strItem in strItems)
            {
                CBOItems.Items.Add(strItem);
            }
            
            //when the form loads, the Items combo box will show the first selected index bby default
            CBOItems.SelectedIndex = 0;

            
            //when the form loads, clear anything that may be in the Breadcrust Items combo box
            CBOBreadCrust.Items.Clear();
        
            //populate the BreadCrust items box with bread types from the strBreadTypes array
            foreach (string strBreadType in strBreadTypes)
            {
                CBOBreadCrust.Items.Add(strBreadType);
            }

            //when the form loads, the breadcrust combo box will show the fist selected index by default which will
            //be a type of bread
            CBOBreadCrust.SelectedIndex = 0;

            GBXDeliveryInformation.Enabled = false;

            TXTName.Focus();
        }

        /// <summary>
        /// When a combo box is changed, the selected index is used to determine whether the item is a sandwhich
        /// or a pizza which then populates the appropriate items in the next combo box as well as displaying the
        /// appropirate picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBOItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When user selects and item in the Items combo box, clear anything that may be in 
            //the Bread/Crust combo box
            CBOBreadCrust.Items.Clear();

            //if user chooses a sandwich item (items 0-2 in array), populate the Bread/Crust combo box with 
            //all the items in the strBreadTypes array
            if (CBOItems.SelectedIndex == 0 || CBOItems.SelectedIndex == 1 || CBOItems.SelectedIndex == 2)
            {
                foreach (string strBreadType in strBreadTypes)
                {
                    CBOBreadCrust.Items.Add(strBreadType);
                }
                CBOBreadCrust.SelectedIndex = 0;

                //The picture box will then populate with a sandwich image 
                PBXPicture.Image = CodingProject1.Properties.Resources.deli;

            }
            //if the user chooses a pizza item (items 3-5 in array), populate the Brad/Crust combo boc
            //with all the items in the strCrustTypes array
            else if (CBOItems.SelectedIndex == 3 || CBOItems.SelectedIndex == 4 || CBOItems.SelectedIndex == 5)
            {
                foreach (string strCrustType in strCrustTypes)
                {
                    CBOBreadCrust.Items.Add(strCrustType);
                }
                CBOBreadCrust.SelectedIndex = 0;

                //the picture box will be populate with a pizza image
                PBXPicture.Image = CodingProject1.Properties.Resources.pizza;
            }
        }

        /// <summary>
        /// when the quanitity text box is changed, the selected item will be used to calculate the price for the requested
        /// quantity of items. The subtotal is then displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXTQuantity_TextChanged(object sender, EventArgs e)
        {
            //sandwhich and pizza prices
            decimal decSandwichPrice = 5.00m;
            decimal decPizzaPrice = 9.50m;
            
            //initialzing variables
            decimal decSubtotal = 0m;
            int intQuantity = 0;

            try
            {
                //if TXTQuantity is not empty, then it will grab the text and convert it into intQuantity
                if (TXTQuantity.Text != "")
                {
                    intQuantity = Convert.ToInt32(TXTQuantity.Text);
                }

                //if the user selects a sandwhich
                if (CBOItems.SelectedIndex == 0 || CBOItems.SelectedIndex == 1 || CBOItems.SelectedIndex == 2)
                {
                    decSubtotal = (decSandwichPrice * intQuantity);
                    TXTSubtotal.Text = decSubtotal.ToString("c");
                }
                //if the user selects a pizza
                else if (CBOItems.SelectedIndex == 3 || CBOItems.SelectedIndex == 4 || CBOItems.SelectedIndex == 5)
                {
                    decSubtotal = (decPizzaPrice * intQuantity);
                    TXTSubtotal.Text = decSubtotal.ToString("c");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Quantity is not in the correct format");
                TXTQuantity.Clear();
                TXTQuantity.Focus();
            }
        }

        /// <summary>
        /// when the add button is clicked, the subtotal is taken and tax is added to get a running total which is then
        /// formatted correctly and then placed into the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNAdd_Click(object sender, EventArgs e)
        {

            try
            {
                if (IsValid())
                {
                    //setting strSubtotal to the text in TXTSubtotal
                    string strSubtotal = TXTSubtotal.Text;
                    //taking the strSubtotal and removing the "$" sign so it can be turned into a decimal
                    strSubtotal = strSubtotal.Replace("$", "");
                    //turning strSubtotal to decSubtotal
                    decimal decSubtotal = Convert.ToDecimal(strSubtotal);

                    //Grand total = Subtotal + tax
                    decGrandTotal += decSubtotal + (decSubtotal * 0.0825m);
                    TXTGrandTotal.Text = decGrandTotal.ToString("c");

                    //getting the Item Price so it can be displayed in the list box
                    decimal decItemPrice = decSubtotal / Convert.ToInt32(TXTQuantity.Text);

                    //Bread or Crust type, Item, Quantity@$ItemPrice, SubTotal
                    string strItemInvoice = CBOBreadCrust.Text + ", " + CBOItems.Text + " " + TXTQuantity.Text + "@" + "$" + decItemPrice + " Total: " + TXTSubtotal.Text;
                    //Adds the ItemInvoice to be displayed in list box LBXOrder
                    LBXOrder.Items.Add(strItemInvoice);

                    //reseting the form partially so the next item can be added to the order
                    //CBOItems.SelectedIndex = 0;
                    CBOItems.Focus();
                    //CBOBreadCrust.SelectedIndex = 0;
                    TXTQuantity.Clear();
                    //PBXPicture.LoadAsync(@"https://i.postimg.cc/ZRGQN6m7/deli.jpg");

                    
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        /// <summary>
        /// when the process button is clicked, the order total is correctly formated displayed in a message box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNProcess_Click(object sender, EventArgs e)
        {

           if (IsValidProcess())
           {
                //displaying the order total
                MessageBox.Show("Total Cost: " + TXTGrandTotal.Text, "Order Total");
                //storing information about the order
               StoreOrder();
                //reseting the form for the next order
                ClearForm();

           }
            
        }

        /// <summary>
        /// method to store the item ordered and quantity which will allow us to call the type of item
        /// ordered and the quantity of that item so we can do calculations with the inventory
        /// </summary>
        public void StoreOrder()
        {
            foreach (string strOrder in LBXOrder.Items)
            {
                string strItemOrdered = "";
                int intQuantityOfItem = 0;
                int intStartItemPosition = strOrder.IndexOf(",") + 2;
                int intEndItemPosition = strOrder.IndexOf("@") - 2;
                int intStartQuantityPosition = intEndItemPosition;
                int intEndQuantityPosition = strOrder.IndexOf("$") - 1;
                int intAtCost = strOrder.Length - intEndQuantityPosition;
              
                string strQuantityOfItems = strOrder.Substring(intStartQuantityPosition, strOrder.Length - intEndItemPosition - intAtCost); //this is a ERROR if they order anything with a quantity more than 9
               // string strItemOrdered = CBOItems.SelectedIndex.ToString();
                //what item the customer ordered put into a string value
                strItemOrdered = strOrder.Substring(intStartItemPosition, intEndItemPosition - intStartItemPosition);
                strItemOrdered = strItemOrdered.Trim();
                //the quantity of that specific item the customer ordered put into a int value
                intQuantityOfItem = Convert.ToInt32(strQuantityOfItems);
                int intItemIndex = Array.IndexOf(strItems, strItemOrdered);
                

                //storing both of these values in thier own lists which will be used on FRMInventory
                //using lists makes it so every order will be stored and its easy to populate them
                lstItemsOrdered.Add(intItemIndex);
                lstNumberOfItemsOrdered.Add(intQuantityOfItem);


            }
            
        }


        /// <summary>
        /// when the delivery check box becomes checked, the infromation from the customer group box is copied into the
        /// corresponding fields, if the delivery check box is unchecked the group box is cleared and disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBXDelivery_CheckedChanged(object sender, EventArgs e)
        {
            //the information from the customer info group box will be copied into the Delivery information groupbox
            //if the delivery check box is checked
            if (CBXDelivery.Checked)
            {
                GBXDeliveryInformation.Enabled = true;
                TXTDStreetAddress.Text = TXTStreetAddress.Text;
                TXTDCity.Text = Convert.ToString(TXTCity.Text.Trim().ToUpper());
                TXTDState.Text = Convert.ToString(TXTState.Text.Trim().ToUpper());
                TXTDZip.Text = TXTZip.Text;
                TXTDSubdivisionLocation.Text = TXTSubdivisionLocation.Text;
                TXTDStreetAddress.Focus();
            }
            //if the check box is not checked, the Group box regarding Delivery information is disabled because the 
            //user will pick up the order at the store
            else
            {
                GBXDeliveryInformation.Enabled = false;
                TXTDStreetAddress.Clear();
                TXTDCity.Clear();
                TXTDState.Clear();
                TXTDZip.Clear();
                TXTDSubdivisionLocation.Clear();
            }
        }

        /// <summary>
        /// when the close button is clicked, the form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// when the clear button is clicked the from is reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// resets the form
        /// </summary>
        private void ClearForm()
        {
            //clearing all customer information
            TXTName.Clear();
            TXTPhoneNumber.Clear();
            TXTStreetAddress.Clear();
            TXTCity.Clear();
            TXTState.Clear();
            TXTZip.Clear();
            TXTSubdivisionLocation.Clear();
            CBXDelivery.Checked = false;
            
            //clearning all address information
            TXTDStreetAddress.Clear();
            TXTDCity.Clear();
            TXTDState.Clear();
            TXTDZip.Clear();
            TXTDSubdivisionLocation.Clear();
            GBXDeliveryInformation.Enabled = false;

            //clearning all order information and the list box
            TXTQuantity.Clear();
            CBOItems.SelectedIndex = 0;
            CBOBreadCrust.SelectedIndex = 0;
            decGrandTotal = 0;
            TXTSubtotal.Text = null;
            TXTGrandTotal.Text = null;
            LBXOrder.Items.Clear();
        }



        /// <summary>
        /// validation to make sure all given parameters are valid when the add button is clicked
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            return Validator.IsComboPresent(CBOBreadCrust) && Validator.IsComboPresent(CBOItems)
                && Validator.IsPresent(TXTQuantity) && Validator.IsWithinRange(TXTQuantity, 1, 99)
                && Validator.IsInteger(TXTQuantity);

            if(CBXDelivery.Checked)
            {
                return Validator.IsPresent(TXTDStreetAddress) && Validator.IsPresent(TXTDCity)
                    && Validator.IsPresent(TXTDState) && Validator.IsPresent(TXTDZip)
                    && Validator.IsPresent(TXTDSubdivisionLocation);
            }
        }

        /// <summary>
        /// validation to make sure all given paratmeters are valid when the process button is clicked
        /// </summary>
        /// <returns></returns>
        private bool IsValidProcess()
        {
         
            if(LBXOrder.Items.Count != 0)
            {
               
                if (CBXDelivery.Checked)
                {
                    return Validator.IsPresent(TXTDStreetAddress) && Validator.IsPresent(TXTDCity)
                    && Validator.IsPresent(TXTDState) && Validator.IsPresent(TXTDZip)
                    && Validator.IsPresent(TXTDSubdivisionLocation) && Validator.IsDeliveryPossible(TXTDCity, TXTDState)
                    && Validator.IsPresent(TXTName);
                }
                return Validator.IsPresent(TXTName);
            }
            MessageBox.Show("Please place an order first.");
            TXTQuantity.Focus(); 
            return false;
        }

        /// <summary>
        /// clicking this button opens the form containing the form that holds a list of the 
        /// inventories that are available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BTNInventory_Click(object sender, EventArgs e)
        {
            FRMInventory NewInventoryForm = new FRMInventory();
            NewInventoryForm.Show();
        }

        private void BTNVendors_Click(object sender, EventArgs e)
        {
            FRMVendor VendorForm = new FRMVendor();
            Vendor NewVendor = VendorForm.ShowVendor();
        }
    }
}
