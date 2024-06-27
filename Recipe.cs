// Weylin Volschenk
// ST10390916
// Group 1

// References: 
//              https://www.w3schools.com/cs/cs_syntax.php
//              https://stackify.com/c-delegates-definition-types-examples/#
//              https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/


using System;
using System.Collections.Generic;

namespace ST10390916PROGPOE
{
    public class Recipe
    {
        public String RecipeName { get; set; }
        public int NumOfIngredients { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();     //list of ingredients in recipe
        public double IngredientScale { get; set; }                                     //For scaling the quantities of ingredients
        public int NumOfSteps { get; set; }
        public List<String> RecipeSteps { get; set; } = new List<string>();             //List of the recipe steps

        //-----------------------------------------------Constructor--------------------------------------------------------------------------

        public Recipe(string recipeName, int numOfIngredients, List<Ingredient> ingredients, double ingredientScale, int numOfSteps, List<string> recipeSteps)
        {
            RecipeName = recipeName;
            NumOfIngredients = numOfIngredients;
            Ingredients = ingredients;
            IngredientScale = ingredientScale;
            NumOfSteps = numOfSteps;
            RecipeSteps = recipeSteps;
        }

        //declaring delegate that will be used for passing the 'calculateTotalCalories' method to a variable

        public delegate double calcIngredientList(List<Ingredient> ingredientList);

        //this method will be used as a delegate type
        //-----------------------------------calculate the total calories in a recipe---------------------------------------

        public double calculateTotalCalories(List<Ingredient> ingredients)
        {
            double totalCalories = 0;

            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.IngredientCalories;
            }

            return totalCalories;
        }

        //declaring delegate that will be used for passing the 'calorieWarning' method to a variable

        public delegate string checkCalories(double calories);

        //this method will be used as a delegate type
        //-----------------------------------check if the total calories exceed 300 to notify user---------------------------------------

        public string calorieWarning(double calories)
        {
            string msg = "";

            if ((calories * IngredientScale) > 300)
            {
                msg = "Note: This recipe has more than 300 calories.";
            }

            return msg;
        }

        //--------------------------------------ToString method for displaying recipe------------------------------------------------------------

        public override String ToString()
        {
            //'calculateTotalCalories' is assigned to totalCalories as the delegate

            calcIngredientList totalCalories = calculateTotalCalories;

            //'calorieWarning' is assigned to warningMsg as the delegate

            checkCalories warningMsg = calorieWarning;

            //string containing the delegate that was used to pass on 'calculateTotalCalories' method and 'calorieWarning'

            String msg = $"{warningMsg.Invoke(calculateTotalCalories(Ingredients))}\n";
            msg += $"Your recipe:\n{RecipeName}\n\nCurrent ingredient scale: x{IngredientScale}\nTotal calories: {totalCalories.Invoke(Ingredients) * IngredientScale}\n\nIngredients:\n\n";

            //checks unit of measurement for tablespoon to cup conversion

            foreach (var ingredient in Ingredients)
            {
                if (ingredient.UnitOfMeasurement.ToLower().Equals("tbsp"))
                {
                    if ((ingredient.IngredientAmount * IngredientScale) >= 16)
                    {
                        ingredient.IngredientAmount /= 16;
                        if ((ingredient.IngredientAmount * IngredientScale) <= 1)
                        {
                            ingredient.UnitOfMeasurement = "Cup";
                        }
                        else
                        {
                            ingredient.UnitOfMeasurement = "Cups";
                        }
                    }
                }

                //checks unit of measurement for cup to tablespoon conversion

                if (ingredient.UnitOfMeasurement.ToLower().Equals("cups"))
                {
                    if ((ingredient.IngredientAmount * IngredientScale) < 1)
                    {
                        ingredient.IngredientAmount *= 16;
                        if ((ingredient.IngredientAmount * IngredientScale) <= 1)
                        {
                            ingredient.UnitOfMeasurement = "Tablespoon";
                        }
                        else
                        {
                            ingredient.UnitOfMeasurement = "Tablespoons";
                        }
                    }
                }

                msg += ingredient.IngredientName + " " + (ingredient.IngredientAmount * IngredientScale) + " " + ingredient.UnitOfMeasurement 
                    + " | " + (ingredient.IngredientCalories * IngredientScale) + " calories | Food group: " + ingredient.IngredientFoodGroup + "\n";
            }

            msg += "\nSteps: \n\n";

            int stepsCount = Math.Min(NumOfSteps, RecipeSteps.Count);
            for (int i = 1; i <= stepsCount; i++)
            {
                msg += i + ". " + RecipeSteps[i - 1].ToString() + "\n";
            }

            msg += "\nEnjoy\n";
            return msg;
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//

