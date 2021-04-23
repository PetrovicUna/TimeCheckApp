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

            ViewBag.Persons = persons;

            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Absences model, string sortType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CultureInfo cul = CultureInfo.CurrentCulture;

                    int weekNum = cul.Calendar.GetWeekOfYear(
                                    model.Date,
                                    CalendarWeekRule.FirstDay,
                                    DayOfWeek.Monday);

                    model.week = weekNum;
                    model.AbsenceType = sortType;

                    _context.Absences.Add(model);
                    _context.SaveChanges();
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
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
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
