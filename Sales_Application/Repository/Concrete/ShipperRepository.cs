using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Repository.Concrete
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public ShipperRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<string> CreateAsync(ShipperDto shipperdto)
        {
            var domain = mapper.Map<Shipper>(shipperdto);
            await context.Shippers.AddAsync(domain);
            await context.SaveChangesAsync();
            return "Record Created Successfully";
        }

        public async Task<List<ShipperDto>> displayShipperAsync()
        {
            var domain =  await context.Shippers.ToListAsync();
            var dto = mapper.Map<List<ShipperDto>>(domain);
            return dto;
        }

        public async Task<string> editShipperDetailsAsync(int id, ShipperDto shipperdto)
        {
            var updateData = await context.Shippers.FirstOrDefaultAsync(o => o.ShipperId == id);
            if (updateData == null)
            {
                return null;
            }

            mapper.Map(shipperdto, updateData);
            await context.SaveChangesAsync();

            return "Updated Successfull!!";
        }

        public async Task<string> patchShipperDetailsAsync(int id, JsonPatchDocument Shipper)
        {
            var result = await context.Shippers.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            Shipper.ApplyTo(result);
            await context.SaveChangesAsync();
            return "Patched Successfully";
        }

        public async Task<ShipperDto> searchCompanyNameAsync(string companyName)
        {
            var domain = await context.Shippers.FirstOrDefaultAsync(o => o.CompanyName == companyName);

            if (domain == null)
            {
                return null;
            }
            var dto = mapper.Map<ShipperDto>(domain);
            return dto;
        }

        public async Task<List<ShipperTotalShipmentDto>> totalShipmentAsync()
        {
            var dto = await context.Orders
                 .GroupBy(s => s.ShipVia)
                 .Select(g => new ShipperTotalShipmentDto
                 {
                     Shipper = g.Key,
                     TotalShipment = g.Count()
                 })
                 .ToListAsync();

            return dto;
        }

        public async Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperAsync()
        {

            var dto = await context.Shippers
                        .Select(shipper => new TotalAmountEarnedByShipperDto
                        {
                            CompanyName = shipper.CompanyName,
                            TotalAmount = context.Orders
                            .Where(order => order.ShipVia == shipper.ShipperId)
                            .Join(
                                context.OrderDetails,
                                order => order.OrderId,
                                orderDetail => orderDetail.OrderId,
                            (order, orderDetail) => new
                            {
                                Quantity = (decimal)orderDetail.Quantity,
                                orderDetail.UnitPrice,
                                Discount = (decimal)orderDetail.Discount
                            })
                            .Sum(od => od.Quantity * od.UnitPrice * (1 - od.Discount))
                        })
                            .ToListAsync();

            return dto;
        }

        public async Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperDateAsync(DateTime date)
        {
            var ordersForShipDateCount = await context.Orders.CountAsync(o => o.OrderDate == date);

            if (ordersForShipDateCount == 0)
            {
                return null;
            }

            var dto = await context.Orders
                .Where(o => o.OrderDate == date)
                .GroupBy(o => o.ShipViaNavigation.CompanyName)
                .Select(g => new TotalAmountEarnedByShipperDto
                {
                    CompanyName = g.Key,
                    TotalAmount = g.SelectMany(o => o.OrderDetails).
                    Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                }).ToListAsync();

            return dto;

        }

        public async Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperYearAsync(int year)
        {
            var ordersForShipDateCount = await context.Orders.CountAsync(o => o.OrderDate.Value.Year == year);

            if (ordersForShipDateCount == 0)
            {
                return null;
            }

            var dto = await context.Orders
                .Where(o => o.OrderDate.Value.Year == year)
                .GroupBy(o => o.ShipViaNavigation.CompanyName)
                .Select(g => new TotalAmountEarnedByShipperDto
                {
                    CompanyName = g.Key,
                    TotalAmount = g.SelectMany(o => o.OrderDetails).
                    Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                }).ToListAsync();

            return dto;

        }

    }
}
