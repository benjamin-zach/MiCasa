using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCasaUI
{
    public interface BackendFacilities
    {
        Ingredient GetIngredient(int id);
        Recepy GetRecepy(int id);
    }
}
