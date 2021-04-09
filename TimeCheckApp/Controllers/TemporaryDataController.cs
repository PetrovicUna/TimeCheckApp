using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeCheckApp.Models;

namespace TimeCheckApp.Controllers
{
    public class TemporaryDataController : Controller
    {

        private readonly TimeCheckAppDbContext _context;

        public TemporaryDataController(TimeCheckAppDbContext context)
        {
            _context = context;
        }
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
            string fileName = $"{hostingEnviroment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            GetTemporaryData(file.FileName);
            return View();
        }

        private void GetTemporaryData(string fName)
        {
            List<TemporaryData> data = new List<TemporaryData>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            FileInfo file = new FileInfo(fileName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                string[] rawText = new string[25];

                List<string> list = null;

                for (int row = 2; row <= rowCount; row++)
                {
                    for (int col = 1; col <= ColCount; col++)
                    {
                        if (worksheet.Cells[row, col].Value == null)
                            worksheet.Cells[row, col].Value = string.Empty;


                        rawText[col] = worksheet.Cells[row, col].Value.ToString();

                    }
                    float hours;
                    float days;
                    var som = float.TryParse(rawText[9].ToString(), out hours);
                    var som1 = float.TryParse(rawText[20].ToString(), out days);

                    data.Add(new TemporaryData()
                    {
                        Date = rawText[1].ToString(),
                        PersonNumber = rawText[2].ToString(),
                        EmployeeName = rawText[3].ToString(),
                        ProjectCode = rawText[4].ToString(),
                        ProjectName = rawText[5].ToString(),
                        TaskName = rawText[6].ToString(),
                        TimeType = rawText[7].ToString(),
                        Location = rawText[8].ToString(),
                        Hours = hours,
                        Commnent = rawText[10].ToString(),
                        TaskNumber = rawText[11].ToString(),
                        Username = rawText[12].ToString(),
                        LineManagerName = rawText[13].ToString(),
                        Country = rawText[14].ToString(),
                        TimeEntryStatus = rawText[15].ToString(),
                        TimeCardStatus = rawText[16].ToString(),
                        Client = rawText[17].ToString(),
                        RelocatedCountry = rawText[18].ToString(),
                        FullName = rawText[19].ToString(),
                        Days = days,
                        WorkLocation = rawText[21].ToString(),
                        GradeCode = rawText[22].ToString(),
                        GradeName = rawText[23].ToString(),
                        LMPersonNumber = rawText[24].ToString()

                    }) ;
                }

            }

            SaveData(data);
        }

        private void SaveData(List<TemporaryData> list)
        {
            List<Person> people = new List<Person>();

            foreach (var item in list)
            {
                people.Add(new Person
                {
                    Name = item.EmployeeName,
                    Username = item.Username,
                    PersonNumber = Convert.ToInt32(item.PersonNumber),

                });
            
                _context.TemporaryData.Add(item);
                _context.SaveChanges();
            }
            
        }

        private void SavePerson(List<Person> list)
        {
            
        }
    }
}

