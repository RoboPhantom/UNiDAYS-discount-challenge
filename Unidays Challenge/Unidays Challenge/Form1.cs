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

        public void Form1_Load(object sender, EventArgs e)
        {
            //UnidaysDiscountChallenge example = new UnidaysDiscountChallenge();
            example = new UnidaysDiscountChallenge();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //example.AddToBasket(txtAdd.Text);
            txtNotice.Text = example.AddToBasket(txtAdd.Text);
            txtAdd.Text = "";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var result = example.CalculateTotalPrice();

            double totalPrice = result[0];
            double deliveryCharge = result[1];
            txtNotice.Text = "Basket length " + result[2].ToString();

            txtTotal.Text = totalPrice.ToString();
            txtDelivery.Text = deliveryCharge.ToString();
            txtOverall.Text = (totalPrice + deliveryCharge).ToString();
        }
    }

    class Discounts
    {
        // [item, price, discount quantity, discount price]
        Object[,] Pricing = { { "A", 8, 1, 8 }, { "B", 12, 2, 20 }, { "C", 4, 3, 10 }, { "D", 7, 2, 7 }, { "E", 5, 3, 10 } };
        public Object[,] PricingRules()
        {
            return Pricing;
        }
    }

    class UnidaysDiscountChallenge
    {
        Object[,] Rules;
        //Object[,] basket;
        List<string> basket;
        List<int> amount;
        public double total;
        public double delivery;

        public UnidaysDiscountChallenge()
        {
            Discounts prices = new Discounts();
            Rules = prices.PricingRules();

            basket = new List<string>();
            amount = new List<int>();

            total = 0;
            delivery = 7;
            //basket = new Object[Rules.GetLength(0), 2];
            // array of items (string) and quantities (int)
            //basket[0, 0] = "A";
            //basket[0, 1] = 0;
        }

        public string AddToBasket(string Item)
        {
            Boolean exists = false;
            if(basket.Count > 0)
            {
                for (int x = 0; x <= basket.Count - 1; x++)
                {
                    if (basket[x] == Item)
                    {
                        amount[x] += 1;
                        exists = true;
                        return basket[x] + " added. Amount " + amount[x] + " Basket has " + basket.Count + " different items.";
                    }
                }
            }

            if (exists == false)
            {
                basket.Add(Item);
                amount.Add(1);
                return basket[basket.Count-1] + " added. Amount " + amount[amount.Count-1] + " Basket has " + basket.Count + " different items.";
            }

            return "No item";
        }

        public List<double> CalculateTotalPrice()
        {
            List<double> Out = new List<double>();
            //Object[,] List = basket;

            // Loop basket items
            for (int a = 0; a <= basket.Count - 1; a++)
            {
                // Loop pricing rules
                for (int b = 0; b <= Rules.GetLength(0) - 1; b++)
                {
                    // Check items match - basket and rule
                    if (basket[a] == (string)Rules[b, 0])
                    {
                        while (amount[a] > 0)
                        {
                            if (amount[a] >= (int)Rules[b, 3])
                            {
                                total += (int)Rules[b, 3];
                                amount[a] -= (int)Rules[b, 2];
                            }
                            else
                            {
                                total += (int)Rules[b, 1];
                                amount[a] -= 1;
                            }
                        }
                    }
                }
            }

            if (total >= 50)
            {
                delivery = 0;
            }

            Out.Add(total);
            Out.Add(delivery);
            Out.Add(basket.Count);
            return Out;

        }
    }

}
