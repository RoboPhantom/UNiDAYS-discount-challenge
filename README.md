# UNiDAYS-discount-challenge

My completion of a discount challenge made by UNiDAYS.
Found [here](https://github.com/MyUNiDAYS/tech-placement-challenge).

## Build instructions
This was made using C# and Visual Studio. To build this app open the Unidays Challenge.sln file in Visual Studio. Once opened click the build tab at the top and click either "Build Solution" or "Build Unidays Challenge". This will make a "Unidays Challenge.exe" application file in the "Unidays Challenge/bin/Debug" directory. This can be run on windows as a form application where you can add items (A,B,C,D,E) to a basket and then calculate the total prices.

## My Approach
My approach was to first create the class and methods without the base code. The AddToBasket method I made by having a pair of lists with the different items and amounts. I first thought about having the basket be a string of each item but would be difficult to count each item and apply discounts. Each type of item is recorded and then amounts are changed instead of multiple individual ones. Otherwise the CalculateTotalPrice method would both have to iterate through the basket and pricing rules. I decided the pricing rules should be a multi-dimensional array for groups of each rule. Each rule would give the item name, default price, the amount for a discount and the discount price. This could then easily be looked up for each basket item. Items that have the required discount amount like "buy 1 get 1 free" being 2 items would then have the discount price added to total otherwise the default price would be added. It would also decrease the amount in the basket by what was added to total and repeat until the basket is empty. A default total and delivery price is set before this code and then once total is calculated then the delivery discount could be checked and set. I also added a reset method to wipe the basket each calculation as if a purchase was made. Lastly I made a form to run the code and display the outputs.

### By Josiah Reagan