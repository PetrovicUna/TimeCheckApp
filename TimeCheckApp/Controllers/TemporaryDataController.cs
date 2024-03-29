﻿using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectsComparer;
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
                var worksheet = package.Workbook.Worksheets["MgmtReportDetails"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                string[] rawText = new string[28];

                List<string> list = null;
                //u excelu podaci krecu od 10og reda

                for (int row = 10; row <= rowCount; row++)
                {
                    for (int col = 1; col <= ColCount; col++)
                    {
                        if (worksheet.Cells[row, col].Value == null)
                            worksheet.Cells[row, col].Value = string.Empty;


                        rawText[col] = worksheet.Cells[row, col].Value.ToString();

                    }
                    float hours;
                    float days;
                    var res = float.TryParse(rawText[9].ToString(), out hours);
                    var ress = float.TryParse(rawText[20].ToString(), out days);

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
                        LMPersonNumber = rawText[24].ToString(),
                        FM = rawText[25].ToString(),
                        Activity = rawText[26].ToString()

                    });
                }

            }

            SaveData(data);
        }

        private void SaveData(List<TemporaryData> list)
        {
            List<Person> people = new List<Person>();
            List<Tasks> Tasks = new List<Tasks>();
            List<Projects> Projects = new List<Projects>();
            List<WorkingHours> workingHours = new List<WorkingHours>();

            foreach (var item in list)
            {

                people.Add(new Person
                {
                    Name = item.EmployeeName,
                    Username = item.Username,
                    PersonNumber = Convert.ToInt32(item.PersonNumber),
                    Country = item.Country,
                    WorkLocation = item.WorkLocation,
                    GradeCode = item.GradeCode,
                    GradeName = item.GradeName
                });

                Tasks.Add(new Tasks
                {
                    TaskName = item.TaskName,
                    TaskNumber = Convert.ToInt32(item.TaskNumber),
                    Activity = item.Activity,
                    FM = item.FM
                });

                Projects.Add(new Projects
                {
                    ProjectCode = item.ProjectCode,
                    ProjectName = item.ProjectName,
                    Client = item.Client
                });


                _context.TemporaryData.Add(item);
                _context.SaveChanges();
            }

            var dbPeople = _context.Persons.ToList();
            var personListDistinct = people.Select(x => x.PersonNumber).Distinct();
            var dbTask = _context.Tasks.ToList();
            var taskListDistinct = Tasks.Select(x => x.TaskNumber).Distinct();
            var dbProject = _context.Projects.ToList();
            var projectListDistinct = Projects.Select(x => x.ProjectCode).Distinct();

            #region Project & Task

            if (dbProject.Count == 0 && dbTask.Count == 0)
            {
                List<Projects> projects = new List<Projects>();

                foreach (var projectCode in projectListDistinct)
                {
                    var newProject = Projects.FirstOrDefault(x => x.ProjectCode == projectCode);

                    projects.Add(newProject);
                }

                SaveProject(projects);

                List<Tasks> tasks = new List<Tasks>();

                foreach (var taskNumber in taskListDistinct)
                {
                    var newTask = Tasks.FirstOrDefault(x => x.TaskNumber == taskNumber);

                    tasks.Add(newTask);
                }

                SaveTask(tasks);
            }

            else
            {
                List<Tasks> tasks = new List<Tasks>();

                foreach (var taskNumber in taskListDistinct)
                {
                    var newTask = Tasks.FirstOrDefault(x => x.TaskNumber == taskNumber);
                    tasks.Add(newTask);
                }

                List<Projects> projects = new List<Projects>();

                foreach (var projectCode in projectListDistinct)
                {
                    var newProject = Projects.FirstOrDefault(x => x.ProjectCode == projectCode);
                    projects.Add(newProject);
                }

                #region If we have new project, which is not in db

                if (dbProject.Count < projects.Count)
                {
                    var newProjectProjectCode = Projects.Select(x => x.ProjectCode).Distinct();
                    var ProjectInDb = _context.Projects.Where(p => newProjectProjectCode
                                                     .Contains(p.ProjectCode))
                                                     .Select(p => p.ProjectCode).ToArray();

                    var ProjectNotInDb = Projects.Where(p => !ProjectInDb.Contains(p.ProjectCode));
                    foreach (Projects project in ProjectNotInDb)
                    {
                        _context.Projects.Add(project);
                        _context.SaveChanges();
                    }
                }

                #endregion

                //#region Delete project from db if we don't have it in excel

                //if (dbProject.Count > projects.Count)
                //{

                //    var newProjectProjectCode = dbProject.Select(x => x.ProjectCode).Distinct();
                //    var ProjectInList = projects.Where(p => newProjectProjectCode
                //                                     .Contains(p.ProjectCode))
                //                                     .Select(p => p.ProjectCode).ToArray();

                //    var ProjectNotInList = dbProject.Where(p => !ProjectInList.Contains(p.ProjectCode));
                //    foreach (Projects projects1 in ProjectNotInList)
                //    {
                //        _context.Projects.Remove(projects1);
                //        _context.SaveChanges();
                //    }
                //}

                //#endregion

                #region if we have new task, which is not in db
                if (dbTask.Count < tasks.Count)
                {
                    var newTaskTaskNumber = Tasks.Select(x => x.TaskNumber).Distinct();
                    var TaskInDb = _context.Tasks.Where(p => newTaskTaskNumber
                                                     .Contains(p.TaskNumber))
                                                     .Select(p => p.TaskNumber).ToArray();

                    var TaskNotInDb = Tasks.Where(p => !TaskInDb.Contains(p.TaskNumber));
                    foreach (Tasks task in TaskNotInDb)
                    {
                        _context.Tasks.Add(task);
                        _context.SaveChanges();
                    }
                }

                #endregion

                //#region Delete task from db if we don't have it in excel

                //if (dbTask.Count > tasks.Count)
                //{
                //    var newTaskTaskNumber = dbTask.Select(x => x.TaskNumber).Distinct();
                //    var TaskInList = tasks.Where(p => newTaskTaskNumber
                //                                     .Contains(p.TaskNumber))
                //                                     .Select(p => p.TaskNumber).ToArray();

                //    var TaskNotInList = dbTask.Where(p => !TaskInList.Contains(p.TaskNumber));
                //    foreach (Tasks task in TaskNotInList)
                //    {
                //        _context.Tasks.Remove(task);
                //        _context.SaveChanges();
                //    }
                //}

                //#endregion

            };

            List<ProjectTask> ProjectTasks = new List<ProjectTask>();

            foreach (var item in list)
            {
                var ProjectCode = item.ProjectCode;
                int TaskNumber = Convert.ToInt32(item.TaskNumber);

                var task = _context.Tasks.FirstOrDefault(t => t.TaskNumber == TaskNumber);
                var project = _context.Projects.FirstOrDefault(p => p.ProjectCode == ProjectCode);

                ProjectTasks.Add(new ProjectTask
                {
                    ProjectID = project.ID,
                    TaskID = task.ID
                });
            }

            List<ProjectTask> finalListProjectTasks = new List<ProjectTask>();

            var pt = ProjectTasks.Select(x => x.TaskID).Distinct();

            foreach (var item12 in pt)
            {
                var ProjectID = ProjectTasks.Find(x => x.TaskID == item12).ProjectID;

                finalListProjectTasks.Add(new ProjectTask
                {
                    ProjectID = ProjectID,
                    TaskID = item12
                });
            }

            SaveProjectTask(finalListProjectTasks);

            #endregion

            #region People

            if (dbPeople.Count == 0)
            {
                foreach (var personNumber in personListDistinct)
                {
                    var newPerson = people.FirstOrDefault(x => x.PersonNumber == personNumber);

                    List<Person> persons = new List<Person>();
                    persons.Add(newPerson);
                    SavePerson(persons);
                }
            }

            else
            {
                List<Person> persons = new List<Person>();

                foreach (var personNumber in personListDistinct)
                {
                    var newPerson = people.FirstOrDefault(x => x.PersonNumber == personNumber);
                    persons.Add(newPerson);
                }

                if (dbPeople.Count < persons.Count)
                {
                    var newPersonPersonNumber = people.Select(x => x.PersonNumber).Distinct();
                    var PersonInDb = _context.Persons.Where(p => newPersonPersonNumber
                                                     .Contains(p.PersonNumber))
                                                     .Select(p => p.PersonNumber).ToArray();

                    var PersonNotInDb = people.Where(p => !PersonInDb.Contains(p.PersonNumber));
                    foreach (Person person in PersonNotInDb)
                    {
                        _context.Persons.Add(person);
                        _context.SaveChanges();
                    }
                }

                Person pers = new Person();

                foreach (var personExcel in persons)
                {
                    foreach (var personDb in dbPeople)
                    {
                        if (personDb.PersonNumber == personExcel.PersonNumber)
                        {
                            var cmpr = new Comparer();

                            personExcel.ID = personDb.ID;

                            bool isSame = cmpr.Compare(personDb, personExcel);

                            if (!isSame)
                            {
                                pers = personExcel;
                                _context.Entry(personDb).CurrentValues.SetValues(pers);
                                _context.SaveChanges();
                            }
           
                        }
                    }
                }



                //if (dbPeople.Count > persons.Count)
                //{

                //    var newPersonPersonNumber = dbPeople.Select(x => x.PersonNumber).Distinct();
                //    var PersonInList = persons.Where(p => newPersonPersonNumber
                //                                     .Contains(p.PersonNumber))
                //                                     .Select(p => p.PersonNumber).ToArray();

                //    var PersonNotInList = dbPeople.Where(p => !PersonInList.Contains(p.PersonNumber));
                //    foreach (Person person in PersonNotInList)
                //    {
                //        _context.Persons.Remove(person);
                //        _context.SaveChanges();
                //    }
                //}
            };

            #endregion

            #region Working Hours

            foreach (var item in list)
            {
                DeleteWorkingHours();

                var taskNumber = Convert.ToInt32(item.TaskNumber);
                var personNumber = Convert.ToInt32(item.PersonNumber);

                var personID = _context.Persons.FirstOrDefault(x => x.PersonNumber == personNumber).ID;
                var taskID = _context.Tasks.FirstOrDefault(x => x.TaskNumber == taskNumber).ID;

                var year = item.Date.Substring(0, 4);
                var month = item.Date.Substring(4, 2);
                var day = item.Date.Substring(6, 2);

                string date = year + "-" + month + "-" + day;

                var datee = Convert.ToDateTime(date);

                CultureInfo cul = CultureInfo.CurrentCulture;

                int weekNum = cul.Calendar.GetWeekOfYear(
                                datee,
                                CalendarWeekRule.FirstDay,
                                DayOfWeek.Monday);


                workingHours.Add(new WorkingHours
                {
                    Date = datee,
                    week = weekNum,
                    Status = item.TimeCardStatus,
                    Comment = item.Commnent,
                    Hours = item.Hours,
                    BookingType = item.TimeType,
                    PersonID = personID,
                    TaskID = taskID
                });
            }

            SaveWorkingHours(workingHours);

            #endregion

            ViewBag.SuccessMessage = "Your data was successfuly inserted!";
        }

        #region Saving Methods
        private void SavePerson(List<Person> list)
        {
            foreach(var item in list)
            {
                _context.Persons.Add(item);
                _context.SaveChanges();
            }
        }

        private void SaveProjectTask(List<ProjectTask> list)
        {
            foreach (var item in list)
            {
                _context.ProjectTasks.Add(item);
                _context.SaveChanges();
            }
        }

        private void SaveTask(List<Tasks> list)
        {
            foreach (var item in list)
            {
                _context.Tasks.Add(item);
                _context.SaveChanges();
            }
        }

        private void SaveProject(List<Projects> list)
        {
            foreach (var item in list)
            {
                _context.Projects.Add(item);
                _context.SaveChanges();
            }
        }

        private void SaveWorkingHours(List<WorkingHours> list)
        {
            foreach (var item in list)
            {
                _context.WorkingHourses.Add(item);
                _context.SaveChanges();
            }
        }

        private void DeleteWorkingHours()
        {
            var all = from c in _context.WorkingHourses select c;
            _context.WorkingHourses.RemoveRange(all);
            _context.SaveChanges();
        }

        #endregion
    }
}

