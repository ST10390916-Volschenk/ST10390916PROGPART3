// Weylin Volschenk
// ST10390916
// Group 1

// References: 
//              https://www.w3schools.com/cs/cs_syntax.php
//              https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10390916PROGPOE;
using System.Collections.Generic;

namespace RecipeTests
{
    [TestClass]
    public class RecipeTests
    {

        //--------------------------------------------Test scenario where calories is more than 300---------------------------------------

        [TestMethod]
        public void calculateTotalCalories_ShouldReturn320()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient("Milk", "ml", 500, 50, "Dairy"),
                new Ingredient("Flour", "mg", 800, 120, "Carbohydrates"),
                new Ingredient("Sugar", "mg", 500, 150, "Carbohydrates")
            };

            List<string> steps = new List<string>()
            {
                "Mix",
                "Bake"
            };

            Recipe recipe = new Recipe("Cake", 3, ingredients, 1, 2, steps);

            double actual = recipe.calculateTotalCalories(ingredients);
            double expected = 320;

            Assert.AreEqual(expected, actual);            
        }

        //--------------------------------------------Test scenario where calories is less than 300---------------------------------------

        [TestMethod]
        public void calculateTotalCalories_ShouldReturn165()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient("Milk", "ml", 250, 25, "Dairy"),
                new Ingredient("Flour", "mg", 400, 60, "Carbohydrates"),
                new Ingredient("Sugar", "mg", 250, 80, "Carbohydrates")
            };

            List<string> steps = new List<string>()
            {
                "Mix",
                "Bake"
            };

            Recipe recipe = new Recipe("Cookies", 3, ingredients, 1, 2, steps);

            recipe.calculateTotalCalories(ingredients);

            double actual = recipe.calculateTotalCalories(ingredients);
            double expected = 165;

            Assert.AreEqual(expected, actual);

        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//