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
    public partial class FRMInventory : Form
    {
        public FRMInventory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// array that stores the starting inventory amounts
        /// </summary>
        decimal[] decCurrentInventory = { 200m,
                                          50m,
                                          30m,
                                          25m,
                                          10m,
                                          10m,
                                          20m,
                                          14m,
                                          14m,
                                          10m,
                                          20m,
                                          15m,
                                          12m,
                                          20m,
                                          60m,
                                          25m,
                                          10m,
                                          10m };

        /// <summary>
        /// array that stores the types of ingredients in inventory
        /// </summary>
        public string[] strIngredients = { "Flour",
                                           "Yeast",
                                           "Sugar",
                                           "Oil",
                                           "Ham",
                                           "Turkey",
                                           "SCheese",
                                           "Lettuce",
                                           "Tomato",
                                           "Bacon",
                                           "Pickles",
                                           "Mayo",
                                           "Mustard",
                                           "Pepperoni",
                                           "Sauce",
                                           "GCheese",
                                           "Salt",
                                           "Pepper" };


        /// <summary>
        /// array that stores how many of what type of ingredients a certain food item uses
        /// </summary>
                                          //H&S    T&P    BLT    Ch    Pep    Sup
        decimal[,] decIngredientsUsed = { { 1m   , 1m   , 1m   , 3m   , 3m   , 3m    }, //flour
                                          { 0.5m , 0.5m , 0.5m , 2m   , 2m   , 2m    }, //yeast
                                          { 0.03m, 0.03m, 0.03m, 0.5m , 0.5m , 0.5m  }, //sugar
                                          { 0.05m, 0.05m, 0.05m, 0.1m , 0.1m , 0.1m  }, //oil
                                          { 0.1m , 0m   , 0m   , 0m   , 0m   , 0.1m  }, //ham
                                          { 0m   , 0.1m , 0m   , 0m   , 0m   , 0.1m  }, //turkey
                                          { 0.1m , 0.1m , 0m   , 0m   , 0m   , 0m    }, //Scheese
                                          { 0.25m, 0.25m, 0.3m , 0m   , 0m   , 0m    }, //lettuce
                                          { 0.25m, 0.25m, 0.3m , 0m   , 0m   , 0.3m  }, //tomato
                                          { 0m   , 0m   , 0.1m , 0m   , 0m   , 0.1m  }, //bacon
                                          { 0.02m, 0.02m, 0m   , 0m   , 0m   , 0m    }, //pickles
                                          { 0.02m, 0.02m, 0.02m, 0m   , 0m   , 0m    }, //mayo
                                          { 0.02m, 0.02m, 0.02m, 0m   , 0m   , 0m    }, //mustard
                                          { 0m   , 0m   , 0m   , 0m   , 0.3m , 0.3m  }, //pepperoni
                                          { 0m   , 0m   , 0m   , 1m   , 1m   , 1m    }, //sauce
                                          { 0m   , 0m   , 0m   , 0.3m , 0.2m , 0.2m  }, //gcheese
                                          { 0.01m, 0.01m, 0.01m, 0.02m, 0.02m, 0.02m }, //salt
                                          { 0.01m, 0.01m, 0.01m, 0.02m, 0.02m, 0.02m } }; //pepper

        
        /// <summary>
        /// When the form loads, Inventory will be calculated and the inventory ingredients amount
        /// will be updated based on the order that was just placed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FRMInventory_Load(object sender, EventArgs e)
        {
            //calculating inventory
            CalculateInventory();
            //populating the list box
            //this variable is acting as a counter for the foreach statement so it will add the same index value from decCurrentInventory
            int i = 0;
            foreach (string strIngredient in strIngredients)
            {
                lbxInventory.Items.Add(strIngredient + " " + decCurrentInventory[i]);
                i++;
            }
            
        }

        /// <summary>
        /// Method that calculates the ingredients an order will use and updates the inventory form
        /// </summary>
        public void CalculateInventory()
        {
            //this is the counter for the orders so the quantity ordered updates corresponding to each order
            int j = 0;
            //each order placed will be processed
            foreach (int order in FRMOrder.lstItemsOrdered)
            {
                //each order placed also has a corresponding quantity
                int quantity = FRMOrder.lstNumberOfItemsOrdered.ElementAt(j);
                //this variable acts as a counter for the index value of decCurrentInventory and decIngredientsUsed
                int i = 0;
                //repeats so ever ingredient is decreased by the given values
                int k = Convert.ToInt32(order);
                foreach (string ingredient in strIngredients)
                {

                    decCurrentInventory[i] = decCurrentInventory[i] - (quantity * decIngredientsUsed[i, k]);

                    i++;
                }
                j++;
                
            }
        }

        /// <summary>
        /// This button closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
