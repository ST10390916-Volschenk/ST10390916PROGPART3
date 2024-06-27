// Weylin Volschenk
// ST10390916
// Group 1

// References: 
//              https://www.w3schools.com/cs/cs_syntax.php
//              https://stackify.com/c-delegates-definition-types-examples/#
//              https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/

using ST10390916PROGPOE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ST10390916PROGPART3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<String> SearchList { get; set; }
        public static double recipeScale = 1;

        private List<Recipe> RecipeList = new List<Recipe>();
        private List<String> RecipeNames = new List<String>();
        private List<Ingredient> IngredientList = new List<Ingredient>();
        private List<String> Steps = new List<String>();

        private Recipe recipe;
        private String currentRecipeSelected = null;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.SearchList = new ObservableCollection<String>();
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            String step = null;

            if (txtStep.Text.Equals(""))
            {
                valid = false;
                MessageBox.Show("Enter a valid step description.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStep.Focus();
                return;
            }
            else
            {
                step = txtStep.Text;
                Steps.Add(step);

                txtStep.Clear();
                txtStep.Focus();
                txbSteps.Text += Steps.Count + ". " + step + "\n";
            }



        }

        //------------------------------------Add ingredient-----------------------------------------------------------------

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            String ingredientName = null;
            double ingredientAmount = 1;
            String unitOfMeasurement = null;
            double ingredientCalories = 0;
            string ingredientFoodGroup = null;

            if (txtIngredientName.Text.Equals(""))
            {
                valid = false;
                MessageBox.Show("Enter a valid ingredient name.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtIngredientName.Focus();
                return;
            }
            else
            {
                ingredientName = txtIngredientName.Text;
            }

            try
            {
                ingredientAmount = double.Parse(txtAmount.Text);
                if (ingredientAmount <= 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show("Enter a valid amount.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtAmount.Focus();
                return;
            }

            if (cmbMeasurement.Text.Equals(""))
            {
                valid = false;
                MessageBox.Show("Select a unit of measurement.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbMeasurement.Focus();
                return;
            }
            else
            {
                unitOfMeasurement = cmbMeasurement.Text;
            }

            try
            {
                ingredientCalories = double.Parse(txtCalories.Text);
                if (ingredientCalories <= 0)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show("Enter a valid amount of calories.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCalories.Focus();
                return;
            }

            if (cmbFoodGroup.Text.Equals(""))
            {
                valid = false;
                MessageBox.Show("Select a food group.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbFoodGroup.Focus();
                return;
            }
            else
            {
                ingredientFoodGroup = cmbFoodGroup.Text;
            }

            //-----------------------Add ingredient if valid------------------------------------------------------------------------

            if (valid)
            {
                Ingredient ingredient = new Ingredient(ingredientName, unitOfMeasurement, ingredientAmount, ingredientCalories, ingredientFoodGroup);
                IngredientList.Add(ingredient);

                txbIngredients.Text += ingredient.IngredientAmount + ingredient.UnitOfMeasurement + "  " + ingredient.IngredientName + "   Calories: " + ingredient.IngredientCalories
                    + "    Food Group: " + ingredient.IngredientFoodGroup + "\n";

                txtIngredientName.Clear();
                txtAmount.Clear();
                cmbMeasurement.SelectedIndex = 0;
                txtCalories.Clear();
                cmbFoodGroup.SelectedIndex = 0;
                txtIngredientName.Focus();
            }
        }

        private void btnCreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            String recipeName = null;

            if (txtRecipeName.Text.Equals(""))
            {
                valid = false;
                MessageBox.Show("Enter a valid recipe name.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtRecipeName.Focus();
                return;
            }
            else
            {
                recipeName = txtRecipeName.Text.Trim();
            }

            foreach (var item in RecipeList)
            {
                if (recipeName.ToLower().Equals(item.RecipeName.ToLower()))
                {
                    valid = false;
                    MessageBox.Show("That recipe name is already taken. Enter a different recipe name.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtRecipeName.Focus();
                    return;
                }

            }

            if (IngredientList.Count < 1)
            {
                valid = false;
                MessageBox.Show("Your recipe must have at least one ingredient.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtIngredientName.Focus();
                return;
            }

            if (Steps.Count < 1)
            {
                valid = false;
                MessageBox.Show("Your recipe must have at least one step/instruction.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStep.Focus();
                return;
            }

            if (valid)
            {
                recipe = new Recipe(recipeName, IngredientList.Count, new List<Ingredient>(IngredientList), 1, Steps.Count, new List<string>(Steps));
                RecipeList.Add(recipe);

                RecipeNames.Add(recipeName);
                RecipeNames.OrderBy(x => x).ToList();

                changeSearchResults(RecipeNames);

                IngredientList.Clear();
                Steps.Clear();
                lbxRecipes.Items.Refresh();
                txbIngredients.Text = "";
                txbSteps.Text = "";
                txtRecipeName.Clear();
            }

        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this recipe?", "Recipe book", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in RecipeList)
                {
                    if (currentRecipeSelected.Equals(item.RecipeName))
                    {
                        RecipeList.Remove(item);
                        RecipeNames.Remove(item.RecipeName);
                        changeSearchResults(RecipeNames);

                        lbxRecipes.Items.Refresh();
                        if (RecipeList.Count == 0)
                        {
                            tbcPages.SelectedIndex = 0;
                        }
                        else
                        {
                            lbxRecipes.SelectedItem = lbxRecipes.Items[0];
                        }
                        MessageBox.Show("Recipe Deleted.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                //test
                MessageBox.Show("Recipe does not exist", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCancelRecipe_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to discard this recipe?", "Recipe book", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                txtIngredientName.Clear();
                txtAmount.Clear();
                cmbMeasurement.SelectedIndex = 0;
                txtCalories.Clear();
                cmbFoodGroup.SelectedIndex = 0;
                tbcPages.TabIndex = 1;
            }
        }

        private void lbxRecipes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecipeList.Count == 0 || SearchList.Count() == 0)
            {
                tbcPages.SelectedIndex = 0;
                lblFormHeading.Content = "Add a Recipe";
                return;
            }

            currentRecipeSelected = lbxRecipes.SelectedItems[0].ToString();

            foreach (var recipe in RecipeList)
            {
                if (currentRecipeSelected.Equals(recipe.RecipeName))
                {
                    txbRecipe.Text = recipe.ToString();
                    lblFormHeading.Content = recipe.RecipeName;
                    tbcPages.SelectedIndex = 1;
                }
            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            tbcPages.SelectedIndex = 0;
            lblFormHeading.Content = "Add a Recipe";

        }

        private void btnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            ScaleWindow scaleWindow = new ScaleWindow();
            scaleWindow.ShowDialog();            

            foreach (var item in RecipeList)
            {
                if (currentRecipeSelected.Equals(item.RecipeName))
                {
                    if (recipeScale == item.IngredientScale)
                    {
                        MessageBox.Show("Recipe scale is already " + item.IngredientScale , "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    item.IngredientScale = recipeScale;
                    txbRecipe.Text = item.ToString();
                    MessageBox.Show("Recipe scale changed to " + item.IngredientScale, "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            String searchKey = null;
            List<string> results = new List<string>();

            if (btnSearch.Content.Equals("X"))
            {
                txtSearch.Clear();
                btnSearch.Content = "Search";
                changeSearchResults(RecipeNames);
                return;
            }

            if (RecipeList.Count() == 0)
            {
                MessageBox.Show("You don't currently have any recipes saved.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                searchKey = txtSearch.Text;
            }
            else
            {
                MessageBox.Show("Enter a valid search key", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var item in RecipeNames)
            {
                if (item.ToLower().Contains(searchKey.ToLower()) || searchKey.ToLower().Contains(item.ToLower()))
                {
                    results.Add(item);
                }
            }

            changeSearchResults(results);
            btnSearch.Content = "X";

        }

        private void changeSearchResults(List<string> recipeNames)
        {
            SearchList.Clear();

            foreach (var item in recipeNames)
            {
                SearchList.Add(item);
            }
        }

        private void btnResetScale_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in RecipeList)
            {
                if (currentRecipeSelected.Equals(item.RecipeName))
                {
                    item.IngredientScale = 1;
                    MessageBox.Show("Recipe scale reset.", "Recipe book", MessageBoxButton.OK, MessageBoxImage.Information);
                    txbRecipe.Text = item.ToString();
                    return;
                }
            }

        }
    }
}
