#define FAKE_DATA

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace MiCasaUI
{
    public class LocalBackendFacilities : BackendFacilities
    {
        public string dataBaseFilePath
        {
            set
            {
                if((previousDataBaseFilePath == "" || value != previousDataBaseFilePath) && CheckDataBaseFilePath(value))
                {
                    previousDataBaseFilePath = value;
                    OnUpdateDataBaseFilePath();
                }
            }
            get
            {
                return previousDataBaseFilePath;
            }
        }

        private string previousDataBaseFilePath = "";
        private SQLiteConnection connection = null;

        private static bool CheckDataBaseFilePath(string path)
        {
            return File.Exists(@path);
        }

        private void OnUpdateDataBaseFilePath()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + previousDataBaseFilePath))
            {
                conn.Open();
                connection = conn;
                
            }
        }

        private string QUERY_GET_INGREDIENT = "SELECT * FROM Ingredients WHERE {1} = \"{0}\"";
        public Ingredient GetIngredient(int id)
        {
            if (connection == null)
                return null;
            string query = String.Format(QUERY_GET_INGREDIENT, id.ToString(), "Id");
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Ingredient ingredient = null;
            while (reader.Read())
            {
                ingredient = new Ingredient();
                ingredient.category = (IngredientCategory)reader["Category"];
                ingredient.id = (int)reader["Id"];
                ingredient.name = (string)reader["Name"];
            }
            return ingredient;
        }

        public Ingredient GetIngredient(string name)
        {
            if (connection == null)
                return null;
            string query = String.Format(QUERY_GET_INGREDIENT, name, "Name");
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Ingredient ingredient = null;
            while (reader.Read())
            {
                ingredient = new Ingredient();
                ingredient.category = (IngredientCategory)reader["Category"];
                ingredient.id = (int)reader["Id"];
                ingredient.name = (string)reader["Name"];
            }
            return ingredient;
        }

        private string QUERY_GET_RECEPY = "SELECT * FROM Recepies WHERE {1} = \"{0}\"";
        public Recepy GetRecepy(int id)
        {
            if (connection == null)
                return null;
            string query = string.Format(QUERY_GET_RECEPY, id.ToString(), "Id");
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Recepy recepy = null;
            while(reader.Read())
            {
                recepy = new Recepy();
                recepy.description = (string)reader["Description"];
                recepy.id = (int)reader["Id"];
                recepy.name = (string)reader["Name"];
                recepy.ingredients = GetIngredientsInRecepy(id);
            }
            return recepy;
        }

        public Recepy GetRecepy(string name)
        {
            if (connection == null)
                return null;
            string query = string.Format(QUERY_GET_RECEPY, name, "Name");
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Recepy recepy = null;
            while (reader.Read())
            {
                recepy = new Recepy();
                recepy.description = (string)reader["Description"];
                recepy.id = (int)reader["Id"];
                recepy.name = (string)reader["Name"];
                recepy.ingredients = GetIngredientsInRecepy(recepy.id);
            }
            return recepy;
        }

        private string QUERY_GET_RECEPIES = "SELECT * from Recepies";
        public List<Recepy> GetRecepies()
        {
#if FAKE_DATA
            return new List<Recepy>()
            {
                new Recepy(){name = "Apple Pie", description = "Desc Apple"},
                new Recepy(){name = "Pear Pie", description = "Desc Pear"}
            };
#endif
            if (connection == null)
                return null;
            SQLiteCommand command = new SQLiteCommand(QUERY_GET_RECEPIES, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Recepy> recepies = new List<Recepy>();
            while(reader.Read())
            {
                Recepy recepy = new Recepy();
                recepy.description = (string) reader["Description"];
                recepy.id = (int) reader["Id"];
                recepy.name = (string) reader["Name"];
                recepy.ingredients = GetIngredientsInRecepy(recepy.id);

                recepies.Add(recepy);
            }
            return recepies;
        }

        private string QUERY_GET_INGREDIENTS_IN_RECEPY = "SELECT * FROM IngrediensInRecepies WHERE RecpyId = \"{0}\"";
        public List<IngredientInRecepy> GetIngredientsInRecepy(int recepyId)
        {
            if (connection == null)
                return null;
            string query = String.Format(QUERY_GET_INGREDIENTS_IN_RECEPY, recepyId.ToString());
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<IngredientInRecepy> ingredientsInRecepy = new List<IngredientInRecepy>();
            while (reader.Read())
            {
                IngredientInRecepy ingredient = new IngredientInRecepy();
                ingredient.amount = (float)reader["Amount"];
                ingredient.id = (int)reader["Id"];
                ingredient.ingredientId = (int)reader["IngredientId"];
                ingredient.recepyId = (int)reader["RecepyId"];
                ingredient.unit = (Unit)reader["Unit"];

                ingredientsInRecepy.Add(ingredient);
            }
            return ingredientsInRecepy;
        }

        private string QUERY_ADD_INGRDIENT = "INSERT INTO Ingredients (Name, Category) VALUES ({0}, {1})";
        public void AddIngredient(Ingredient ingredient)
        {
            if (connection == null)
                return;
            string query = String.Format(QUERY_ADD_INGRDIENT, ingredient.name, ingredient.category.ToString());
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private string QUERY_ADD_RECEPY = "INSERT INTO Recepy (Name, Description) VALUES ({0}, {1})";
        public void AddRecepy(Recepy recepy)
        {
            if (connection == null)
                return;
            string query = string.Format(QUERY_ADD_RECEPY, recepy.name, recepy.description);
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private string QUERY_ADD_INGREDIENT_IN_RECEPY = "INSERT INTO IngredientsInRecepies (IngredientId, RecepyId, Amount, Unit) VALUES ({0}, {1}, {2}, {3})";
        public void AddIngredientInRecepy(IngredientInRecepy ingredientInRecepy)
        {
            if (connection == null)
                return;
            string query = String.Format(QUERY_ADD_INGREDIENT_IN_RECEPY, ingredientInRecepy.ingredientId, ingredientInRecepy.recepyId, ingredientInRecepy.amount, ingredientInRecepy.unit.ToString());
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}
