using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCheckApp.Models;

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
        public ActionResult Index()
        {
            var workingHours = _context.WorkingHourses
                        .Include(wHour => wHour.Person)
                        .Include(wHour => wHour.Tasks)
                        .ToList();

            CalculateMonthlyQuota();

            ViewBag.Monthly = MonthQuota;
            ViewBag.Week = WeekQuota;
            
            return View(workingHours);
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
            return View();
        }

        // POST: WorkingHoursController/Edit/5
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
