using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCasaUI
{
    public enum IngredientCategory
    {
        Fruit,
        Vegetable,
        Dairy,
        Fish,
        Meat,
        Dry,
        Bakery,
        Beverage
    }

    public enum Unit
    {
        None,
        g,
        kg,
        ml,
        l,
        TeaSpoon,
        TableSpoon
    }

    public class Ingredient
    {
        public int id;
        public string name;
        public IngredientCategory category;
    }

    public class IngredientInRecepy
    {
        public int id;
        public int ingredientId;
        public int recepyId;
        public float amount;
        public Unit unit;
    }

    public class Recepy
    {
        public int id;
        public List<IngredientInRecepy> ingredients;
        public string name;
        public string description;
    }
}
