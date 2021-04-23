using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TimeCheckApp.Models;
using static TimeCheckApp.Models.WorkingHours;

namespace TimeCheckApp.Controllers
{
    public class WorkingHoursController : Controller
    {

        private readonly TimeCheckAppDbContext _context;


        public WorkingHoursController(TimeCheckAppDbContext context)
        {
            _context = context;
        }

        public int MonthQuota;
        public int WeekQuota;


        protected void CalculateMonthlyQuota()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            int weekNumberOfDays = 0;

            int days = DateTime.DaysInMonth(year, month);
            List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i <= days; i++)
            {
                DateTime date = new DateTime(year, month, i);
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    continue;
                }
                if (date.AddDays((double)(7 - date.DayOfWeek)).Date.Equals(DateTime.Now.AddDays((double)(7 - DateTime.Now.DayOfWeek)).Date))
                {
                    weekNumberOfDays++;
                }
                dates.Add(date);
            }

            // int weekDays = dates.Where(d => d.DayOfWeek > DayOfWeek.Sunday & d.DayOfWeek < DayOfWeek.Saturday).Count();
            int weekDays = dates.Count();
            MonthQuota = weekDays * 8;
            WeekQuota = weekNumberOfDays * 8;
        }

        // GET: WorkingHoursController
        public ActionResult Index(string searchString, string bday)
        {
            var workingHours = _context.WorkingHourses
                       .Include(wHour => wHour.Person)
                       .Include(wHour => wHour.Tasks).OrderBy(wHour => wHour.Date.Day)
                       .ToList();

            var bt = new List<SelectListItem>();

            foreach (BookingTypes bookingType in Enum.GetValues(typeof(BookingTypes)))
            {
                bt.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(BookingTypes), bookingType),
                    Value = bookingType.ToString()
                });
            }

            ViewBag.BookingType = bt;

            return View(workingHours);
        }

        public ActionResult Search(string searchString, string bday, string sortType, string personH, string sumH, string sumWH, string taskH)
        {
            var workingHours = _context.WorkingHourses
                       .Include(wHour => wHour.Person)
                       .Include(wHour => wHour.Tasks)
                       .ToList();


            var absences = _context.Absences
                          .Include(x => x.Person)
                          .ToList();

            List<WorkingHoursAbsenceDTO> dto = new List<WorkingHoursAbsenceDTO>();

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(bday) && !string.IsNullOrEmpty(sortType) && (!string.IsNullOrEmpty(personH) && personH.Equals("true")))
            {

                var date = Convert.ToDateTime(bday);

                var month = date.Month;

                var wh = workingHours.FirstOrDefault(x => x.Person.Name.Contains(searchString));

                if (wh != null)
                {
                    var PName = wh.Person.Name;

                    Char[] myChars = { ',' };
                    string[] names = PName.Split(myChars);

                    var Name = names[1];
                    var Surename = names[0];

                    string NameSurname = Name + " " + Surename;

                    ViewBag.PName = NameSurname;
                }

                var workingHoursFiltered = _context.WorkingHourses.Where(x => x.Person.Name.Contains(searchString) && x.Date.Month == month && x.BookingType == sortType).OrderBy(x => x.Date.Day).ToList();

                var absencesFiltered = _context.Absences.Where(x => x.Person.Name.Contains(searchString)).ToList();

                foreach (var item in workingHoursFiltered)
                {
                    foreach (var abs in absencesFiltered)
                    {
                        if (abs.Date != item.Date)
                        {
                            dto.Add(new WorkingHoursAbsenceDTO
                            {
                                Date = item.Date,
                                Hours = item.Hours,
                                BookingType = item.BookingType,
                                week = item.week
                            });
                        }
                        if (abs.Date == item.Date)
                        {
                            dto.Add(new WorkingHoursAbsenceDTO
                            {
                                Date = item.Date,
                                Hours = item.Hours,
                                BookingType = item.BookingType,
                                AbsenceType = abs.AbsenceType,
                                AbsenceHours = abs.Hours,
                                week = item.week
                            });
                        }
                    }
                }
            }

            if(!string.IsNullOrEmpty(bday) && (!string.IsNullOrEmpty(sumH) && sumH.Equals("true")))
            {
                SearchSum(bday);
                return View("SearchSum");
            }

            if (!string.IsNullOrEmpty(bday) && (!string.IsNullOrEmpty(sumWH) && sumWH.Equals("true")))
            {
                SearchWeekSum(bday);
                return View("SearchSum");
            }

            if(!string.IsNullOrEmpty(bday) && !string.IsNullOrEmpty(searchString) && (!string.IsNullOrEmpty(taskH) && taskH.Equals("true")))
            {
                SearchWhTask(bday, searchString);
                return View("Index");
            }

            var distinctWeek = workingHours.Select(x => x.week).Distinct();

            return View(dto);
        }

        public ActionResult SearchWhTask(string bday, string searchString)
        {
            var workingHours = _context.WorkingHourses
                       .Include(wHour => wHour.Person)
                       .Include(wHour => wHour.Tasks).OrderBy(wHour => wHour.Date.Day)
                       .ToList();

            var bt = new List<SelectListItem>();

            foreach (BookingTypes bookingType in Enum.GetValues(typeof(BookingTypes)))
            {
                bt.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(BookingTypes), bookingType),
                    Value = bookingType.ToString()
                });
            }

            ViewBag.BookingType = bt;

            var date = Convert.ToDateTime(bday);

            var month = date.Month;

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(bday))
            {
                workingHours = _context.WorkingHourses.Where(x => x.Person.Name.Contains(searchString) && x.Date.Month == month).OrderBy(x => x.Date.Day).ToList();
            }

            return View("Index", workingHours);
        }

        public ActionResult SearchSum(string bday)
        {
            CalculateMonthlyQuota();

            ViewBag.Monthly = MonthQuota;
            ViewBag.Week = WeekQuota;

            var date = Convert.ToDateTime(bday);

            var month = date.Month;

            var workingHoursFiltered = _context.WorkingHourses.Where(x => x.Date.Month == month).OrderBy(x => x.Date.Day).ToList();
            var AbsencesFiltered = _context.Absences.Where(x => x.Date.Month == month).OrderBy(x => x.Date.Day).ToList();

            var PersonDistinct = _context.Persons.Select(x => x.ID).Distinct().ToList();

            List<SumDto> sumDTOs = new List<SumDto>();

            List<float> RegularHours = new List<float>();
            List<float> Overtimehours = new List<float>();
            List<float> ExtendedOvertimeHours = new List<float>();
            List<float> AbsenceHours = new List<float>();

            foreach (var person in PersonDistinct)
            {
                var workHPerson = workingHoursFiltered.Where(x => x.PersonID == person).ToList();

                if(!workHPerson.Count.Equals(0))
                {
                    var forStatusAndID = workHPerson.FirstOrDefault(x => x.PersonID == person);

                    var personID = forStatusAndID.PersonID;
                    var status = forStatusAndID.Status;
                    var personName = forStatusAndID.Person.Name;

                    var absPerson = AbsencesFiltered.Where(x => x.PersonID == person).ToList();

                    foreach (var item in workHPerson)
                    {
                        if (item.BookingType == "Regular")
                        {
                            RegularHours.Add(item.Hours);
                        }
                    }

                    foreach (var item in workHPerson)
                    {

                        if (item.BookingType == "Overtime")
                        {
                            Overtimehours.Add(item.Hours);
                        }
                    }

                    foreach (var item in workHPerson)
                    {
                        if (item.BookingType == "ExtendedOvertime")
                        {
                            ExtendedOvertimeHours.Add(item.Hours);
                        }
                    }

                    foreach (var item in absPerson)
                    {
                        AbsenceHours.Add(item.Hours);
                    }

                   
                    var regular = RegularHours.Sum(x => x);
                    var overtime = Overtimehours.Sum(x => x);
                    var extended = ExtendedOvertimeHours.Sum(x => x);
                    var absence = AbsenceHours.Sum(x => x);

                    ViewBag.CountH = regular + overtime + extended;

                    sumDTOs.Add(new SumDto
                    {
                        PersonID = personID,
                        PersonName = personName,
                        Status = status,
                        RegularHours = regular,
                        OvertimeHours = overtime,
                        ExtendedOvertimeHours = extended,
                        AbsenceHours = absence
                    });

                    RegularHours.Clear();
                    Overtimehours.Clear();
                    ExtendedOvertimeHours.Clear();
                    AbsenceHours.Clear();
                } 
            }

            return View(sumDTOs);
        }

        public ActionResult SearchWeekSum(string bday)
        {
            CalculateMonthlyQuota();

            ViewBag.Monthly = MonthQuota;
            ViewBag.Week = WeekQuota;

            var date = Convert.ToDateTime(bday);

            CultureInfo cul = CultureInfo.CurrentCulture;

            int weekNum = cul.Calendar.GetWeekOfYear(
                            date,
                            CalendarWeekRule.FirstDay,
                            DayOfWeek.Monday);

            var workingHoursFiltered = _context.WorkingHourses.Where(x => x.week == weekNum).OrderBy(x => x.Date.Day).ToList();
            var AbsencesFiltered = _context.Absences.Where(x => x.week == weekNum).OrderBy(x => x.Date.Day).ToList();

            var PersonDistinct = _context.Persons.Select(x => x.ID).Distinct().ToList();

            List<SumDto> sumDTOs = new List<SumDto>();

            List<float> RegularHours = new List<float>();
            List<float> Overtimehours = new List<float>();
            List<float> ExtendedOvertimeHours = new List<float>();
            List<float> AbsenceHours = new List<float>();

            foreach (var person in PersonDistinct)
            {
                var workHPerson = workingHoursFiltered.Where(x => x.PersonID == person).ToList();

                if (!workHPerson.Count.Equals(0))
                {
                    var forStatusAndID = workHPerson.FirstOrDefault(x => x.PersonID == person);

                    var personID = forStatusAndID.PersonID;
                    var status = forStatusAndID.Status;
                    var personName = forStatusAndID.Person.Name;

                    var absPerson = AbsencesFiltered.Where(x => x.PersonID == person).ToList();

                    foreach (var item in workHPerson)
                    {
                        if (item.BookingType == "Regular")
                        {
                            RegularHours.Add(item.Hours);
                        }
                    }

                    foreach (var item in workHPerson)
                    {

                        if (item.BookingType == "Overtime")
                        {
                            Overtimehours.Add(item.Hours);
                        }
                    }

                    foreach (var item in workHPerson)
                    {
                        if (item.BookingType == "ExtendedOvertime")
                        {
                            ExtendedOvertimeHours.Add(item.Hours);
                        }
                    }

                    foreach (var item in absPerson)
                    {
                        AbsenceHours.Add(item.Hours);
                    }


                    var regular = RegularHours.Sum(x => x);
                    var overtime = Overtimehours.Sum(x => x);
                    var extended = ExtendedOvertimeHours.Sum(x => x);
                    var absence = AbsenceHours.Sum(x => x);

                    ViewBag.CountH = regular + overtime + extended;

                    sumDTOs.Add(new SumDto
                    {
                        PersonID = personID,
                        PersonName = personName,
                        Status = status,
                        RegularHours = regular,
                        OvertimeHours = overtime,
                        ExtendedOvertimeHours = extended,
                        AbsenceHours = absence
                    });

                    RegularHours.Clear();
                    Overtimehours.Clear();
                    ExtendedOvertimeHours.Clear();
                    AbsenceHours.Clear();

                    
                }
            }

            ViewBag.Reper = "true";
            return View(sumDTOs);
        }
        // GET: WorkingHoursController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: WorkingHoursController/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: WorkingHoursController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkingHoursController/Edit/5
        public ActionResult Edit(int id)
        {
            WorkingHours wH = _context.WorkingHourses.Where(
              x => x.ID == id).SingleOrDefault();

            var bt = new List<SelectListItem>();

            foreach (BookingTypes bookingType in Enum.GetValues(typeof(BookingTypes)))
            {
                bt.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(BookingTypes), bookingType),
                    Value = bookingType.ToString()
                });           
            }

            ViewBag.BookingType = bt;

            var status = new List<SelectListItem>();

            foreach (Statuses statuss in Enum.GetValues(typeof(Statuses)))
            {
                status.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(Statuses), statuss),
                    Value = statuss.ToString()
                });
            }

            ViewBag.Status = status;

            var person = _context.Persons.FirstOrDefault(x => x.ID == wH.PersonID);
            ViewBag.Person = person.Name;

            List<Tasks> tasks = new List<Tasks>();
            tasks = _context.Tasks.ToList();
            ViewBag.Tasks = tasks;

            return View(wH);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkingHours model)
        {
            WorkingHours wH = _context.WorkingHourses.Where(
              x => x.ID == model.ID).SingleOrDefault();

            model.PersonID = wH.PersonID;
            model.week = wH.week;

            if (wH != null)
            {

                _context.Entry(wH).CurrentValues.SetValues(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: WorkingHoursController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkingHoursController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
