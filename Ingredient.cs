// Weylin Volschenk
// ST10390916
// Group 1

// References: 
//              https://www.w3schools.com/cs/cs_syntax.php
//              https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/

using System;

namespace ST10390916PROGPOE
{
    public class Ingredient
    {
        public String IngredientName {  get; set; }
        public String UnitOfMeasurement { get; set; }
        public double IngredientAmount { get; set; }
        public double IngredientCalories { get; set; }
        public string IngredientFoodGroup { get; set; }

        //--------------------------------Ingredient Constructor-----------------------------------------------------------

        public Ingredient(string ingredientName, string unitOfMeasurement, double ingredientAmount, double ingredientCalories, string ingredientFoodGroup)
        {
            this.IngredientName = ingredientName;
            this.UnitOfMeasurement = unitOfMeasurement;
            this.IngredientAmount = ingredientAmount;
            IngredientCalories = ingredientCalories;
            IngredientFoodGroup = ingredientFoodGroup;
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//

