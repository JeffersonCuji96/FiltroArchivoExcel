using ExcelDataReader;
using ImportacionExcel.Helper;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ImportacionExcel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["tempData"] = null;
            TempData["fileData"] = "0";
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    if (uploadFile.ContentLength > 500000)
                    {
                        ViewBag.Alert = "El archivo no debe pesar más de 500KB";
                        return View();
                    }
                    Stream stream = uploadFile.InputStream;
                    IExcelDataReader readerFile;
                    if (uploadFile.FileName.EndsWith(".xls"))
                    {
                        readerFile = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadFile.FileName.EndsWith(".xlsx"))
                    {
                        readerFile = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ViewBag.Alert = "El Formato del archivo es incorrecto";
                        return View();
                    }

                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataRow row;
                    try
                    {
                        dt2 = readerFile.AsDataSet().Tables[0];

                        //Define las columnas de  la tabla con la primera fila del archivo
                        for (int i = 0; i < dt2.Columns.Count; i++)
                        {
                            dt1.Columns.Add(dt2.Rows[0][i].ToString());
                        }

                        //Genera las filas de la tabla para agregarlas con las filas del archivo
                        int rowcounter = 0;
                        for (int row_ = 1; row_ < dt2.Rows.Count; row_++)
                        {
                            row = dt1.NewRow();
                            for (int col = 0; col < dt2.Columns.Count; col++)
                            {
                                dynamic dataRow;

                                /*Verifica el tipo de dato para obtener la fecha sin la hora*/
                                if (dt2.Rows[row_][col].GetType().Name.Equals("DateTime"))
                                    dataRow = Convert.ToDateTime(dt2.Rows[row_][col].ToString()).ToString("dd-MM-yyyy");
                                else
                                    dataRow = dt2.Rows[row_][col].ToString();

                                row[col] = dataRow;
                                rowcounter++;
                            }
                            dt1.Rows.Add(row);
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.Alert = "No se pudo cargar el archivo";
                        return View();
                    }

                    DataSet result = new DataSet();
                    result.Tables.Add(dt1);
                    readerFile.Close();
                    readerFile.Dispose();
                    TempData["fileData"] = "1";
                    Session["tempData"] = result.Tables[0];
                    return View(result.Tables[0]);
                }
                else
                {
                    TempData["fileData"] = "0";
                    ViewBag.Alert = "Debe examinar el archivo primero";
                }
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Filtro(string option)
        {
            var dt = new DataTable();
            dt = (DataTable)Session["tempData"];
            if (dt != null)
            {
                var apFiltro = new AplicacionFiltro();
                switch (option)
                {
                    case "A": return PartialView("FiltroA", apFiltro.AplicarFiltroA(dt));
                    case "B": return PartialView("FiltroB", apFiltro.AplicarFiltroB(dt));
                    case "C": return PartialView("FiltroC", apFiltro.AplicarFiltroC(dt));
                    case "D": return PartialView("FiltroD", apFiltro.AplicarFiltroD(dt));
                }
            }
            return new EmptyResult();
        }

        public FileResult DescargarArchivo()
        {
            string path = Server.MapPath("~/Content/") + "ExcelDemo.xlsx";
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", "ExcelDemo.xlsx");
        }
    }
}