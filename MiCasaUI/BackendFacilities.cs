﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCasaUI
{
    public interface BackendFacilities
    {
        Ingredient GetIngredient(int id);
        Ingredient GetIngredient(string name);
        Recepy GetRecepy(int id);
        Recepy GetRecepy(string name);
        List<IngredientInRecepy> GetIngredientsInRecepy(int recepyId);
        List<Recepy> GetRecepies();

        void AddIngredient(Ingredient ingredient);
        void AddRecepy(Recepy recepy);
        void AddIngredientInRecepy(IngredientInRecepy ingredientInRecepy);
    }

    public class BackendFacilitiesRef
    {
        private static BackendFacilities instance;

        public static BackendFacilities Get()
        {
            if(instance == null)
            {
                instance = new LocalBackendFacilities();
            }
            return instance;
        }
    }
}
