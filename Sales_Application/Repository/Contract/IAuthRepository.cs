using Sales_Application.DTO;

namespace Sales_Application.Repository.Contract
{
    public interface IAuthRepository
    {
        Task<bool> adminUsernameAsync(AdminDto adminDto);
        Task<string> adminPasswordAsync(AdminDto adminDto);

        Task<bool> employeeUsernameAsync(EmployeeLoginDto employeeLoginDto);
        Task<string> employeePasswordAsync(EmployeeLoginDto employeeLoginDto);
        Task<bool> shipperUsernameAsync(ShipperLoginDto shipperLoginDto);
        Task<string> shipperPasswordAsync(ShipperLoginDto shipperLoginDto);
    }
}
