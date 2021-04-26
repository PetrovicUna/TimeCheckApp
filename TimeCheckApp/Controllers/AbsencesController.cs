using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TimeCheckApp.Models;
using static TimeCheckApp.Models.Absences;

namespace TimeCheckApp.Controllers
{
    public class AbsencesController : Controller
    {

        private readonly TimeCheckAppDbContext _context;

        public AbsencesController(TimeCheckAppDbContext context)
        {
            _context = context;
        }
        // GET: HomeController1
        public ActionResult Index()
        {
            var absences = _context.Absences
                       .Include(wHour => wHour.Person)
                       .ToList();
            return View(absences);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            var at = new List<SelectListItem>();

            foreach (AbsencesType absenceType in Enum.GetValues(typeof(AbsencesType)))
            {
                at.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(AbsencesType), absenceType),
                    Value = absenceType.ToString()
                });
            }

            ViewBag.AbsenceType = at;

            List<Person> persons = new List<Person>();

            persons = _context.Persons.ToList();

            var personsFiltered = persons.OrderBy(x => x.Name);

            ViewBag.Persons = personsFiltered;

            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AbsenceDTO model, string sortType)
        {
            Absences absence = new Absences();
            List<Absences> absenceList = new List<Absences>();
            List<DateTime> selectedDates = new List<DateTime>();

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.StartDate != null && model.EndDate == null)
                    {
                        CultureInfo cul = CultureInfo.CurrentCulture;

                        int weekNum = cul.Calendar.GetWeekOfYear(
                                        model.StartDate,
                                        CalendarWeekRule.FirstDay,
                                        DayOfWeek.Monday);


                        absence.Date = model.StartDate;
                        absence.Hours = model.Hours;
                        absence.AbsenceType = sortType;
                        absence.PersonID = model.PersonID;
                        absence.week = weekNum;
                        

                        _context.Absences.Add(absence);
                        _context.SaveChanges();
                    }

                    else if(model.StartDate != null && model.EndDate != null)
                    {
                        for (var date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
                        {
                            selectedDates.Add(date);
                        }

                        foreach (var item in selectedDates)
                        {
                            CultureInfo cul = CultureInfo.CurrentCulture;

                            int weekNum = cul.Calendar.GetWeekOfYear(
                                            item,
                                            CalendarWeekRule.FirstDay,
                                            DayOfWeek.Monday);

                            absenceList.Add(new Absences
                            {
                                Date = item,
                                AbsenceType = sortType,
                                Hours = model.Hours,
                                week = weekNum,
                                PersonID = model.PersonID
                            });
                        }

                        foreach (var item in absenceList)
                        {
                            _context.Absences.Add(item);
                            _context.SaveChanges();
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            Absences abs = _context.Absences.Where(
             x => x.ID == id).SingleOrDefault();

            var at = new List<SelectListItem>();

            foreach (AbsencesType absencesType in Enum.GetValues(typeof(AbsencesType)))
            {
                at.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(AbsencesType), absencesType),
                    Value = absencesType.ToString()
                });
            }

            ViewBag.AbsenceType = at;

            var person = _context.Persons.FirstOrDefault(x => x.ID == abs.PersonID);
            ViewBag.Person = person.Name;


            return View(abs);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Absences model)
        {
            Absences abs = _context.Absences.Where(
              x => x.ID == model.ID).SingleOrDefault();

            model.PersonID = abs.PersonID;

            CultureInfo cul = CultureInfo.CurrentCulture;

            int weekNum = cul.Calendar.GetWeekOfYear(
                            model.Date,
                            CalendarWeekRule.FirstDay,
                            DayOfWeek.Monday);

            model.week = weekNum;

            if (abs != null)
            {

                _context.Entry(abs).CurrentValues.SetValues(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int? id)
        {
            var absence = _context.Absences.Find(id);

            _context.Absences.Remove(absence);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
