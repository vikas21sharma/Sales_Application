using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;
using System.Drawing;

namespace Sales_Application.Repository.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public EmployeeRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<string> addEmployeeAsync(EmployeeDto employee)
        {
            var domain = mapper.Map<Employee>(employee);
            await context.Employees.AddAsync(domain);
            await context.SaveChangesAsync();
            return "Record Created Successfully";
        }
        public async Task<List<EmployeeDto>> displayAllEmployeeAsync()
        {
            var domainModel = await context.Employees.ToListAsync();
            var dto = mapper.Map<List<EmployeeDto>>(domainModel);

            return dto;
        }

        public async Task<string> editEmployeeDetailsAsync(int id, EmployeeDto employeeDto)
        {
            var existing = await context.Employees.FirstOrDefaultAsync(o => o.EmployeeId == id);

            if (existing == null)
            {
                return null;
            }

            mapper.Map(employeeDto, existing);
            await context.SaveChangesAsync();
            return "Updated Successfully";
        }

        public async Task<string> patchShipperDetailsAsync(int id, JsonPatchDocument employee)
        {
            var existing = await context.Employees.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            employee.ApplyTo(existing);
            await context.SaveChangesAsync();
            return "Patched Successfully";
        }

        public async Task<List<EmployeeDto>> employeeByTitleAsync(string title)
        {
            var titleFind = await context.Employees.Where(o => o.Title == title).FirstOrDefaultAsync();
            if(titleFind == null)
            {
                return null;
            }
            var domain = await context.Employees.Where(o => o.Title == title).ToListAsync();
            var dto = mapper.Map<List<EmployeeDto>>(domain);
            return dto;
        }

        public async Task<List<EmployeeDto>> employeeByCityAsync(string city)
        {
            var cityFind = await context.Employees.Where(o => o.City == city).FirstOrDefaultAsync();
            if (cityFind == null)
            {
                return null;
            }
            var domain = await context.Employees.Where(o => o.City == city).ToListAsync();
            var dto = mapper.Map<List<EmployeeDto>>(domain);
            return dto;
        }

        public async Task<List<EmployeeDto>> employeeByRegionAsync(string region)
        {
            var regionFind = await context.Employees.Where(o => o.Region == region).FirstOrDefaultAsync();
            if (regionFind == null)
            {
                return null;
            }
            var domain = await context.Employees.Where(o => o.Region == region).ToListAsync();
            var dto = mapper.Map<List<EmployeeDto>>(domain);
            return dto;
        }

        public async Task<List<EmployeeDto>> employeeByHireDateAsync(DateTime date)
        {
            var dateFind = await context.Employees.Where(o => o.HireDate == date).FirstOrDefaultAsync();
            if (dateFind == null)
            {
                return null;
            }
            var domain = await context.Employees.Where(o => o.HireDate == date).ToListAsync();
            var dto = mapper.Map<List<EmployeeDto>>(domain);
            return dto;
        }
        public async Task<EmployeeDto> highestSaleByEmployeeDateAsync(DateTime date)
        {

            var domain = await context.Orders
                .Join(context.OrderDetails,
                      o => o.OrderId,
                      od => od.OrderId,
                      (o, od) => new
                      {
                          o.OrderId,
                          o.OrderDate,
                          BillAmount = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount),
                          o.EmployeeId
                      })
                .Where(joined => joined.OrderDate == date)
                .OrderByDescending(joined => joined.BillAmount)
                .FirstOrDefaultAsync();

            if (domain == null)
            {
                return null;
            }
            var emp = await context.Employees.FindAsync(domain.EmployeeId);
            var dto = mapper.Map<EmployeeDto>(emp);
            return dto;


        }

        public async Task<EmployeeDto> highestSaleByEmployeesYearAsync(int year)
        {
            var domain = context.Orders
              .Join(context.OrderDetails,
                    o => o.OrderId,
                    od => od.OrderId,
                    (o, od) => new
                    {
                        o.OrderId,
                        o.OrderDate,
                        BillAmount = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount),
                        o.EmployeeId
                    })
              .Where(joined => joined.OrderDate.Value.Year == year)
              .OrderByDescending(joined => joined.BillAmount)
               .FirstOrDefault(); ;
            if (domain == null)
            {
                return null;
            }
            var emp = await context.Employees.FindAsync(domain.EmployeeId);
            var dto = mapper.Map<EmployeeDto>(emp);
            return dto;
        }

        public async Task<EmployeeDto> lowestSaleByEmployeeDateAsync(DateTime date)
        {
            var domain = context.Orders
               .Join(context.OrderDetails,
                     o => o.OrderId,
                     od => od.OrderId,
                     (o, od) => new
                     {
                         o.OrderId,
                         o.OrderDate,
                         BillAmount = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount),
                         o.EmployeeId
                     })
               .Where(joined => joined.OrderDate == date)
               .OrderBy(joined => joined.BillAmount)
               .FirstOrDefault();

            if (domain == null)
            {
                return null;
            }
            var emp = await context.Employees.FindAsync(domain.EmployeeId);
            var dto = mapper.Map<EmployeeDto>(emp);
            return dto;
        }

        public async Task<EmployeeDto> lowestSaleByEmployeesYearAsync(int year)
        {
            var domain = context.Orders
              .Join(context.OrderDetails,
                    o => o.OrderId,
                    od => od.OrderId,
                    (o, od) => new
                    {
                        o.OrderId,
                        o.OrderDate,
                        BillAmount = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount),
                        o.EmployeeId
                    })
              .Where(joined => joined.OrderDate.Value.Year == year)
              .OrderBy(joined => joined.BillAmount)
               .FirstOrDefault(); ;

            if (domain == null)
            {
                return null;
            }
            var emp = await context.Employees.FindAsync(domain.EmployeeId);
            var dto = mapper.Map<EmployeeDto>(emp);
            return dto;
        }

        public async Task<List<SaleEmployeeDto>> saleEmployeeByDateAsync(DateTime date, int id)
        {
            var dateFind = await context.Orders.FirstOrDefaultAsync(o => o.OrderDate ==  date & o.EmployeeId == id);
            if (dateFind == null)
            {
                return null;
            }
            var domain = from o in context.Orders
                        join c in context.Customers
                        on o.CustomerId equals c.CustomerId
                        where o.OrderDate == date
                        && o.EmployeeId == id
                        select new SaleEmployeeDto
                        {
                            OrderId = o.OrderId,
                            CompanyName = c.CompanyName

                        };

            var dto = await domain.ToListAsync();
            return dto;
        }

        public async Task<List<SaleEmployeeDto>> SaleEmployeeBetweenDateAsync(int id, DateTime fromdate, DateTime todate)
        {
            var find = await context.Orders.FirstOrDefaultAsync(o => o.OrderDate >= fromdate  & o.OrderDate <= todate & o.EmployeeId == id);
            if(find == null)
            {
                return null;
            }
            var domain = from o in context.Orders
                        join c in context.Customers
                        on o.CustomerId equals c.CustomerId
                        where o.OrderDate >= fromdate && o.OrderDate <= todate
                        && o.EmployeeId == id
                        select new SaleEmployeeDto
                        {
                            OrderId = o.OrderId,
                            CompanyName = c.CompanyName

                        };
            var dto = await domain.ToListAsync();
            return dto;
        }

        public async Task<IEnumerable<object>> companyNameAsync(int id)
        {
            var idFind = await context.Orders.FirstOrDefaultAsync(o => o.EmployeeId == id);
            if(idFind == null)
            {
                return null;
            }

            var domain = context.Orders
              .Where(o => o.EmployeeId == id).Join(context.Customers,
                  order => order.CustomerId,
                  customer => customer.CustomerId,
                  (order, customer) => new
                  {
                      customer.CompanyName,
                      order.OrderId,
                      order.OrderDate,
                      order.ShippedDate,
                      order.Freight,
                      order.ShipName,
                      order.ShipAddress
                  });

            var dto = await domain.ToListAsync();
            return dto;
        }
    }
}
