using System;

public class Class1
{
    // [item, price, discount quantity, discount price]
    var[,] PricingRules = { { "A", 8, 1, 8 }, { "B", 12, 2, 20 }, { "C", 4, 3, 10 }, { "D", 7, 2, 7 }, { "E", 5, 3, 10 } };

    class UnidaysDiscountChallenge(Rules)
    {
        //var Rules = PricingRules;
        
        // array of items (string) and quantities (int)
        var[,] basket = { { "A", 0 } };

        public AddToBasket(string Item)
        {
            Boolean exists = false;

            for (int x = 0; x == basket.GetLength(0) - 1; x++)
            {
                if (basket[x, 0] == Item)
                {
                    basket[x, 1] += 1;
                    exists = true;
                }
            }
            if (!exists)
            {
                basket[basket.GetLength(0), 0] = Item;
                basket[basket.GetLength(0), 1] = 1;
            }
        }

        public CalculateTotalPrice()
        {
            double Total = 0;
            double DeliveryCharge = 7;
            var[,] List = basket;

            // Loop basket
            for (int a = 0; a == List.GetLength(0); a++)
            {
                // Loop pricing rules
                for (int b = 0; b == Rules.GetLength(0); b++)
                {
                    // Check items match - basket and rule
                    if (List[a, 0] == Rules[b, 0])
                    {
                        while (List[a, 1] > 0)
                        {
                            if (List[a, 1] >= Rules[b, 3])
                            {
                                Total += Rules[b, 3];
                                List[a, 1] - Rules[b, 2];
                            }
                            else
                            {
                                Total += Rules[b, 1];
                                List[a, 1] - 1;
                            }
                        }
                    }
                }
            }

            if (Total >= 50)
            {
                DeliveryCharge = 0;
            }
        }
    }
}
