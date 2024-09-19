using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Repository.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public AuthRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> adminUsernameAsync(AdminDto adminDto)
        {
            var user = context.Employees.FirstOrDefault(o => o.FirstName == adminDto.FirstName);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> adminPasswordAsync(AdminDto adminDto)
        {
            var user = context.Employees.FirstOrDefault(o => o.FirstName == adminDto.FirstName & o.Password == adminDto.Password);
            if (user == null)
            {
                return null;
            }
            var roleName = await context.Roles.FirstOrDefaultAsync(o => o.RoleId == user.RoleId);
            return roleName.RoleName;
        }

        public async Task<bool> employeeUsernameAsync(EmployeeLoginDto employeeLoginDto)
        {
            var user = context.Employees.FirstOrDefault(o => o.FirstName == employeeLoginDto.FirstName);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> employeePasswordAsync(EmployeeLoginDto employeeLoginDto)
        {
            var user = context.Employees.FirstOrDefault(o => o.FirstName == employeeLoginDto.FirstName & o.Password == employeeLoginDto.Password);
            if (user == null)
            {
                return null;
            }
            var roleName = await context.Roles.FirstOrDefaultAsync(o => o.RoleId == user.RoleId);
            return roleName.RoleName;
        }

        public async Task<bool> shipperUsernameAsync(ShipperLoginDto shipperLoginDto)
        {
            var user = context.Shippers.FirstOrDefault(o => o.Email == shipperLoginDto.Email);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> shipperPasswordAsync(ShipperLoginDto shipperLoginDto)
        {
            var user = context.Shippers.FirstOrDefault(o => o.Email == shipperLoginDto.Email & o.Password == shipperLoginDto.Password);
            if (user == null)
            {
                return null;
            }
            var roleName = await context.Roles.FirstOrDefaultAsync(o => o.RoleId == user.RoleId);
            return roleName.RoleName;
        }
    }
}
