using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Repository.Concrete
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public OrdersRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Display All Orders Details
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrdersDto>> getAllOrdersAsync()
        {
            var domain = await context.Orders.ToListAsync();
            if (domain == null) { return null; }
            var dto = mapper.Map<List<OrdersDto>>(domain);
        
            return dto;
        }

        /// <summary>
        /// Display Orders By Name
        /// </summary>
        /// <param name="Firstname"></param>
        /// <returns></returns>
        public async Task<List<OrdersDto>> displayAllOrderByNameAsync(string Firstname)
        {
            var nameFind =  context.Employees.Where(o => o.FirstName == Firstname).FirstOrDefault();
            if (nameFind == null) 
            { 
                return null; 
            }

            var domain =  from o in context.Orders
                         join em in context.Employees
                         on o.EmployeeId equals em.EmployeeId
                         where em.FirstName == Firstname
                         select o;

            var dto =  mapper.Map<List<OrdersDto>>(domain);
            return dto;
        }

        /// <summary>
        /// Display Orders By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ShipDetailsDtoById>> shipDetailsByIdAsync(int id)
        {
            var idFind = context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            if (idFind == null)
            {
                return null;
            }
            var dto = from order in context.Orders
                        where order.OrderId == id
                        select new ShipDetailsDtoById
                        {
                            ShipName = order.ShipName,
                            ShipAddress = order.ShipAddress,
                        };


            return await dto.ToListAsync();
        }

        /// <summary>
        /// Display Ship Details By Date
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public async Task<List<ShipDetailsDtoByDate>> shipDetailsByDateAsync(DateTime fromdate, DateTime todate)
        {
            var dateFind = await context.Orders.Where(o => o.OrderDate >= fromdate & o.OrderDate <= todate).FirstOrDefaultAsync();
            if (dateFind == null)
            {
                return null;
            }
            var dto = from order in context.Orders
                        where order.OrderDate >= fromdate
                        && order.OrderDate <= todate
                        select new ShipDetailsDtoByDate
                        {
                            ShipName = order.ShipName,
                            ShipAddress = order.ShipAddress,
                            ShipRegion = order.ShipRegion
                        };

            return await dto.ToListAsync();
        }

        /// <summary>
        /// Display Ship Details 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShipDetailsDto>> allShipDetailsAsync()
        {
            var dto = from order in context.Orders
                         select new ShipDetailsDto
                         {
                             ShipName = order.ShipName,
                             ShipAddress = order.ShipAddress,
                             ShipRegion = order.ShipRegion
                         };
            if (dto == null) { return null; }
            return await dto.ToListAsync();
        }

        /// <summary>
        /// Employee Order Details
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderByEmployeeNameDto>> allOrdersByEmployeeAsync()
        {
            var dto = from o in context.Orders
                         join em in context.Employees
                         on o.EmployeeId equals em.EmployeeId
                         group o by em.FirstName into g
                         select new OrderByEmployeeNameDto
                         {
                             EmployeeName = g.Key,
                             TotalOrder = g.Count()
                         };
            if (dto == null) { return null; }
            return await dto.ToListAsync();
        }
    }
}
