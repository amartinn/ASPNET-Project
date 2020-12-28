namespace CasesNET.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models.Enum;
    using CasesNET.Services.Data;
    using CasesNET.Services.Messaging;
    using CasesNET.Web.ViewModels.Administration.Orders;
    using CasesNET.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class OrdersController : AdministrationController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: Administration/Orders
        [HttpGet]
        public IActionResult Index()
        {
            var model = new OrderListingViewModel
            {
                Orders = this.orderService.GetAll<OrderViewModel>(),
            };
            foreach (var order in model.Orders)
            {
                order.Items = this.orderService.GetAllItemsByOrderId<CartItemViewModel>(order.Id);
            }

            return this.View(model);
        }

        // GET: Administration/Orders/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var order = this.orderService.GetById<OrderEditInputModel>(id.Value);
            if (order == null)
            {
                return this.NotFound();
            }

            order.Statuses = this.GetOrderStatusesAsSelectList();
            order.Items = this.orderService.GetAllItemsByOrderId<CartItemViewModel>(id.Value);
            return this.View(order);
        }

        // POST: Administration/Orders/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.orderService.UpdateAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.OrderExists(model.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            model.Items = this.orderService.GetAllItemsByOrderId<CartItemViewModel>(model.Id);
            model.Statuses = this.GetOrderStatusesAsSelectList();
            return this.View(model);
        }

        private bool OrderExists(int id)
            => this.orderService.GetAll<OrderViewModel>().Any(e => e.Id == id);

        private SelectList GetOrderStatusesAsSelectList()
        {
            var values = from OrderStatus e in Enum.GetValues(typeof(OrderStatus))
                         select new
                        {
                            Id = (int)Enum.Parse(typeof(OrderStatus), e.ToString()),
                            Name = e.ToString(),
                        };
            return new SelectList(values, "Id", "Name", values);
        }
    }
}
