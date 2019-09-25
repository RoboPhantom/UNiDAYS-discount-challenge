using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unidays_Challenge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UnidaysDiscountChallenge example;
        Discounts pricing;

        public void Form1_Load(object sender, EventArgs e)
        {
            pricing = new Discounts();
            Object[,] PricingRules = pricing.GetPrices();
            example = new UnidaysDiscountChallenge(PricingRules);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNotice.Text = example.AddToBasket(txtAdd.Text);
            txtBasket.Text += txtAdd.Text;
            txtAdd.Text = "";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var result = example.CalculateTotalPrice();

            double totalPrice = result[0];
            double deliveryCharge = result[1];

            txtTotal.Text = totalPrice.ToString();
            txtDelivery.Text = deliveryCharge.ToString();
            txtOverall.Text = (totalPrice + deliveryCharge).ToString();

            example.Reset();
            txtBasket.Text = "Basket: ";
            txtNotice.Text = "Item prices calculated. Basket reset.";
        }
    }

    class Discounts
    {
        // [item, default price, quantity needed for discount, discount price]
        Object[,] Prices = { { "A", 8, 1, 8 }, { "B", 12, 2, 20 }, { "C", 4, 3, 10 }, { "D", 7, 2, 7 }, { "E", 5, 3, 10 } };
        public Object[,] GetPrices()
        {
            return Prices;
        }
    }

    class UnidaysDiscountChallenge
    {
        //Array for pricing rules
        Object[,] Rules;

        //Lists for items and their quantities
        List<string> basket;
        List<int> amount;

        //total price and delivery price
        public double total;
        public double delivery;

        public UnidaysDiscountChallenge(Object[,] prices)
        {
            Rules = prices;

            basket = new List<string>();
            amount = new List<int>();

            //set base totals
            total = 0;
            delivery = 7;
        }

        public string AddToBasket(string Item)
        {
            Boolean exists = false;
            //Check if basket is empty
            if(basket.Count > 0)
            {
                //Loops basket items
                for (int x = 0; x <= basket.Count - 1; x++)
                {
                    //Checks if basket item is same as new item
                    if (basket[x] == Item)
                    {
                        //Increases item amount
                        amount[x] += 1;
                        exists = true;
                        return basket[x] + " added. Amount " + amount[x] + " Basket has " + basket.Count + " different items.";
                    }
                }
            }

            // Checks if no matching item
            if (exists == false)
            {
                //Adds item and amount
                basket.Add(Item);
                amount.Add(1);
                return basket[basket.Count-1] + " added. Amount " + amount[amount.Count-1] + " Basket has " + basket.Count + " different items.";
            }

            return "No item";
        }

        public List<double> CalculateTotalPrice()
        {
            // Totals list
            List<double> Out = new List<double>();

            // Loop basket items
            for (int a = 0; a <= basket.Count - 1; a++)
            {
                // Loop pricing rules
                for (int b = 0; b <= Rules.GetLength(0) - 1; b++)
                {
                    // Check items match - basket and rule
                    if (basket[a] == (string)Rules[b, 0])
                    {
                        // Loops item amounts til empty
                        while (amount[a] > 0)
                        {
                            // Checks if item amount qualifies for discount amount
                            if (amount[a] >= (int)Rules[b, 2])
                            {
                                //Adds discount price and removes discounted items
                                total += (int)Rules[b, 3];
                                amount[a] -= (int)Rules[b, 2];
                            }
                            else
                            {
                                //Adds default price and removes item
                                total += (int)Rules[b, 1];
                                amount[a] -= 1;
                            }
                        }
                    }
                }
            }
            //Checks if qualified for free delivery
            if (total >= 50)
            {
                delivery = 0;
            }

            Out.Add(total);
            Out.Add(delivery);
            return Out;

        }

        //Clears basket and resets totals
        public void Reset()
        {
            basket.Clear();
            amount.Clear();
            total = 0;
            delivery = 7;
        }
    }

}
