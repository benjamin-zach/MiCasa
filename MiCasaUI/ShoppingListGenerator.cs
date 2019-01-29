using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCasaUI
{
    class ShoppingListGenerator
    {
        private List<Meal> meals;

        public ShoppingListGenerator(List<Meal> inMeals)
        {
            meals = inMeals;
        }

        public List<IngredientInRecepy> Generate()
        {
            List<IngredientInRecepy> shoppingList = new List<IngredientInRecepy>();
            foreach(var meal in meals)
            {
                foreach(var ingredientInRecepy in meal.recepy.ingredients)
                {
                    var shoppingListItem = shoppingList.Find(ing => ing == ingredientInRecepy);
                    if(shoppingListItem != null)
                    {
                        shoppingListItem += ingredientInRecepy;
                    }
                    else
                    {
                        shoppingList.Add(ingredientInRecepy);
                    }
                }
            }
            return shoppingList;
        }

        public static void Debug()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient(){id = 0, category = IngredientCategory.Bakery, name = "Flour"},
                new Ingredient(){id = 1, category = IngredientCategory.Dairy, name = "Egg"},
                new Ingredient(){id = 2, category = IngredientCategory.Dairy, name = "Milk"},
                new Ingredient(){id = 3, category = IngredientCategory.Dry, name = "Spaghetti"},
                new Ingredient(){id = 4, category = IngredientCategory.Fish, name = "Rombo"},
                new Ingredient(){id = 5, category = IngredientCategory.Meat, name = "Vitello"},
                new Ingredient(){id = 6, category = IngredientCategory.Vegetable, name = "Tomato"}
            };
            List<Recepy> recepies = new List<Recepy>()
            {
                new Recepy() {id = 0, name = "Apple Pie", description = "Desc 0", ingredients = new List<IngredientInRecepy>
                    {
                        new IngredientInRecepy() {id = 0, ingredientId = 0, recepyId = 0, amount = 500, unit = Unit.g},

                    }
                }
        }
    }
}
