using ImportacionExcel.Models;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Linq;
using System;
using System.Globalization;

namespace ImportacionExcel.Helper
{
    public class AplicacionFiltro
    {
        public List<FiltroA> AplicarFiltroA(DataTable dt)
        {
            var listaFiltroA = new List<FiltroA>();
            var emision = new EmisionCedula();
            int countRow = dt.Rows.Count;
            for (int iRow = 0; iRow < countRow; iRow++)
            {
                string cedula = dt.Rows[iRow].ItemArray[1].ToString();
                string codigo = cedula.Substring(0, 2);
                string provincia;
                emision.ListaEmision.TryGetValue(codigo, out provincia);
                listaFiltroA.Add(new FiltroA()
                {
                    Usuario = dt.Rows[iRow].ItemArray[0].ToString(),
                    Cedula = cedula,
                    Provincia = provincia
                });
            }
            return listaFiltroA;
        }
        public List<FiltroB> AplicarFiltroB(DataTable dt)
        {
            var listaFiltroB = new List<FiltroB>();
            var emision = new EmisionCedula();
            int countRow = dt.Rows.Count;
            for (int iRow = 0; iRow < countRow; iRow++)
            {
                string email = dt.Rows[iRow].ItemArray[3].ToString();
                var address = new MailAddress(email);
                if(address.Host=="yahoo.com" || address.Host == "gmail.com")
                {
                    listaFiltroB.Add(new FiltroB()
                    {
                        Usuario = dt.Rows[iRow].ItemArray[0].ToString(),
                        Cedula = dt.Rows[iRow].ItemArray[1].ToString(),
                        Correo = email
                    });
                }
            }
            return listaFiltroB;
        }

        public List<FiltroC> AplicarFiltroC(DataTable dt)
        {
            var listaFiltroC = new List<FiltroC>();
            int countRow = dt.Rows.Count;
            int contadorMayorEdad = 0;
            int contadorMenorEdad = 0;
            var culture = new CultureInfo("es-ES");
            for (int iRow = 0; iRow < countRow; iRow++)
            {
                var fechaNacimiento = Convert.ToDateTime(dt.Rows[iRow].ItemArray[4].ToString(), culture);
                int edad=CalcularEdad(fechaNacimiento);
                if (edad >= 18) 
                { 
                    contadorMayorEdad++;  
                }
                else 
                { 
                    contadorMenorEdad++; 
                }
            }
            listaFiltroC.Add(new FiltroC(){ Descripcion = "Mayores de edad", Cantidad = contadorMayorEdad });
            listaFiltroC.Add(new FiltroC(){ Descripcion = "Menores de edad", Cantidad = contadorMenorEdad });
            return listaFiltroC;
        }

        public IEnumerable<FiltroD> AplicarFiltroD(DataTable dt)
        {
            var listaFiltroD = new List<FiltroD>();
            int countRow = dt.Rows.Count;
            var culture = new CultureInfo("es-ES");
            for (int iRow = 0; iRow < countRow; iRow++)
            {
                var fechaNacimiento = Convert.ToDateTime(dt.Rows[iRow].ItemArray[4].ToString(),culture);
                string sexo = dt.Rows[iRow].ItemArray[2].ToString();
                int edad = CalcularEdad(fechaNacimiento);
                if (sexo == "F")
                {
                    listaFiltroD.Add(new FiltroD()
                    {
                        Usuario = dt.Rows[iRow].ItemArray[0].ToString(),
                        Edad = edad,
                        Sexo = sexo
                    });
                }
            }
            IEnumerable<FiltroD> listaOrdenada = listaFiltroD.OrderBy(x => x.Edad);
            return listaOrdenada;
        }

        public int CalcularEdad(DateTime fechaNacimiento)
        {
            int año = DateTime.UtcNow.AddHours(-5).Year - fechaNacimiento.Year;
            int mes = DateTime.UtcNow.AddHours(-5).Month - fechaNacimiento.Month;
            int dia = DateTime.UtcNow.AddHours(-5).Day - fechaNacimiento.Day;
            if (mes < 0)
            {
                return año - 1;
            }
            else if (mes == 0) 
            {
                if (dia <= 0) 
                {
                    return año;
                }
                else 
                {
                    return año - 1;
                }
            }
            else 
            {
                return año;
            }
        }
    }
}