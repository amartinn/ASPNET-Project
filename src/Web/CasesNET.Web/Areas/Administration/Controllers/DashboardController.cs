namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CasesNET.Data.Common.Repositories;
    using CasesNET.Data.Models;
    using CasesNET.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IRepository<ApplicationUser> usersRepository;
        private readonly IRepository<Order> ordersRepository;

        public DashboardController(
            IRepository<ApplicationUser> usersRepository,
            IRepository<Order> ordersRepository)
        {
            this.usersRepository = usersRepository;
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var allOrders = this.ordersRepository.AllAsNoTracking().ToList();
            var model = new IndexViewModel();
            foreach (DateTime day in this.EachDay(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow))
            {
                var orders = allOrders.Where(x => x.CreatedOn.Day == day.Day &&
                x.CreatedOn.Month == day.Month).ToList();

                model.OrderDataDate += $"'{day.ToShortDateString()}',";
                model.OrderDataCount += $"'{orders.Count()}',";
            }

            model.OrderDataDate = "[" + model.OrderDataDate + "]";
            model.OrderDataCount = "[" + model.OrderDataCount + "]";
            return this.View(model);
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
