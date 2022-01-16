using System.Collections.Generic;

namespace ImportacionExcel.Helper
{
    public class EmisionCedula
    {
        public Dictionary<string, string> ListaEmision = new Dictionary<string, string>();
        public EmisionCedula()
        {
            ListaEmision.Add("01", "Azuay");
            ListaEmision.Add("02", "Bolivar");
            ListaEmision.Add("03", "Cañar");
            ListaEmision.Add("04", "Carchi");
            ListaEmision.Add("05", "Cotopaxi");
            ListaEmision.Add("06", "Chimborazo");
            ListaEmision.Add("07", "El Oro");
            ListaEmision.Add("08", "Esmeraldas");
            ListaEmision.Add("09", "Guayas");
            ListaEmision.Add("10", "Imbabura");
            ListaEmision.Add("11", "Loja");
            ListaEmision.Add("12", "Los Rios");
            ListaEmision.Add("13", "Manabí");
            ListaEmision.Add("14", "Morona Santiago");
            ListaEmision.Add("15", "Napo");
            ListaEmision.Add("16", "Pastaza");
            ListaEmision.Add("17", "Pichincha");
            ListaEmision.Add("18", "Tungurahua");
            ListaEmision.Add("19", "Zamora Chinchipe");
            ListaEmision.Add("20", "Galápagos");
            ListaEmision.Add("21", "Sucumbios");
            ListaEmision.Add("22", "Orellana");
            ListaEmision.Add("23", "Santo Donmingo de los Tsáchilas");
            ListaEmision.Add("24", "Santa Elena");
        }
    }
}