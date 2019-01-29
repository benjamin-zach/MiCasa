using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

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

    public enum MealType
    {
        Flexible,
        Breakfast,
        Lunch,
        Dinner
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

        static Dictionary<Unit, Dictionary<Unit, float>> conversionFactors = new Dictionary<Unit, Dictionary<Unit, float>>()
        {
            {Unit.g, new Dictionary<Unit, float>() { {Unit.g, 1f}, {Unit.kg, 0.001f } } },
            {Unit.kg, new Dictionary<Unit, float>() {{Unit.g, 1000f }, {Unit.kg, 1f } } },
            {Unit.l, new Dictionary<Unit, float>() {{Unit.l, 1f}, { Unit.ml, 1000f} } },
            {Unit.ml, new Dictionary<Unit, float>() {{Unit.l, 0.001f}, { Unit.ml, 1f} } }
        };

        public IngredientInRecepy() { }

        public IngredientInRecepy(IngredientInRecepy other)
        {
            this.id = other.id;
            this.ingredientId = other.ingredientId;
            this.recepyId = other.recepyId;
            this.amount = other.amount;
            this.unit = other.unit;
        }

        public static IngredientInRecepy operator+(IngredientInRecepy lhs, IngredientInRecepy rhs)
        {
            IngredientInRecepy result = new IngredientInRecepy(lhs);
            result.amount += conversionFactors[rhs.unit][lhs.unit] * rhs.amount;
            return result;
        }
    }

    public class Recepy
    {
        public int id;
        public List<IngredientInRecepy> ingredients = new List<IngredientInRecepy>();
        public string name { get; set; }
        public string description { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class Meal
    {
        public int id;
        public DateTime date;
        public string date_ { get { return date.ToShortDateString(); } }
        public MealType type;
        public string type_ { get { return Enum.GetName(type.GetType(), type); } }
        public Recepy recepy;
        public string recepy_ { get { return recepy.name; } }
    }
}