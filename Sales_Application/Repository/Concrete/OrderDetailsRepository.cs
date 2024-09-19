using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Repository.Concrete
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public OrderDetailsRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Display All Employees Data Access Logic
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderDetailDto>> displayAllEmployeeAsync()
        {
            var domain = await context.OrderDetails.ToListAsync();
            var dto =  mapper.Map<List<OrderDetailDto>>(domain);

            if(dto == null)
            {
                return null;
            }
            return dto;
        }

        /// <summary>
        /// Bill Amount for an Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<BillAmountDto>> displayAllBillAmountAsync(int id)
        {
            var idFind = await context.Orders.FirstOrDefaultAsync(o => o.EmployeeId == id);
            if (idFind == null)
            {
                return null;
            }
            var result = from o in context.Orders
                         join od in context.OrderDetails
                         on o.OrderId equals od.OrderId
                         where o.EmployeeId == id
                         select new BillAmountDto
                         {
                             OrderId = o.OrderId,
                             BillAmount = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
                         };

            return await result.ToListAsync();
        }

    }
}
