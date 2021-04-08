using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeCheckApp.Models;

namespace TimeCheckApp.Controllers
{
    public class TemporaryDataController : Controller
    {
        // GET: HomeController1
        [HttpGet]
        public IActionResult Index(List<TemporaryData> temporaryData = null)
        {
            temporaryData = temporaryData == null ? new List<TemporaryData>() : temporaryData;
            return View(temporaryData);
        }

        [HttpPost]
        public ActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            //string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";
            //using (FileStream fileStream = System.IO.File.Create(fileName))
            //{
            //    file.CopyTo(fileStream);
            //    fileStream.Flush();
            //}

            //var data = GetTemporaryData(file.FileName);
            return View();
        }

        private List<TemporaryData> GetTemporaryData(string fName)
        {
            //    List<TemporaryData> data = new List<TemporaryData>();
            //    var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //    using(var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            //    {
            //        using(var reader= ExcelReaderFactory.CreateReader(stream))
            //        {
            //            for (int i = 1; i < reader.RowCount; i++)
            //           {
            //                foreach (var item in )
            //                {

            //                }
            //                data.Add(new TemporaryData()
            //                {
            //                    Date = reader.GetDateTime(0),
            //                    PersonNumber = reader.GetInt32(1),
            //                    EmployeeName = reader.GetValue(2).ToString(),
            //                    ProjectCode = reader.GetValue(3).ToString(),
            //                    ProjectName = reader.GetValue(4).ToString(),
            //                    TaskName = reader.GetValue(5).ToString(),
            //                    TimeType = reader.GetValue(6).ToString(),
            //                    Location = reader.GetValue(7).ToString(),
            //                    Hours = float.Parse(reader.GetValue(8).ToString()),
            //                    Commnent = reader.GetValue(9).ToString(),
            //                    TaskNumber = Convert.ToInt32(reader.GetValue(10)),
            //                    Username = reader.GetValue(11).ToString(),
            //                    LineManagerName = reader.GetValue(12).ToString(),
            //                    Country = reader.GetValue(13).ToString(),
            //                    TimeEntryStatus = reader.GetValue(14).ToString(),
            //                    TimeCardStatus = reader.GetValue(15).ToString(),
            //                    Client = reader.GetValue(16).ToString(),
            //                    FullName = reader.GetValue(17).ToString(),
            //                    Days = float.Parse(reader.GetValue(18).ToString()),
            //                    WorkLocation = reader.GetValue(19).ToString(),
            //                    GradeCode = reader.GetValue(20).ToString(),
            //                    GradeName = reader.GetValue(21).ToString(),
            //                    LMPersonNumber = Convert.ToInt32(reader.GetValue(22)),
            //                    FM = reader.GetValue(23).ToString(),
            //                    Activity = reader.GetValue(24).ToString()

            //                });
            //            }  
            //        }

            //        return data;
            //    }
            //}
        }
    }
}

