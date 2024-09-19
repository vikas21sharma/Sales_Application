using Microsoft.AspNetCore.JsonPatch;
using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Repository.Contract
{
    public interface IEmployeeRepository
    {
        Task<string> addEmployeeAsync(EmployeeDto employee);
        Task<List<EmployeeDto>> displayAllEmployeeAsync();
        Task<string> editEmployeeDetailsAsync(int id, EmployeeDto employeeDto);
        Task<string> patchShipperDetailsAsync(int id, JsonPatchDocument employee);
        Task<List<EmployeeDto>> employeeByTitleAsync(string title);
        Task<List<EmployeeDto>> employeeByCityAsync(string city);
        Task<List<EmployeeDto>> employeeByRegionAsync(string region);
        Task<List<EmployeeDto>> employeeByHireDateAsync(DateTime date);
        Task<EmployeeDto> highestSaleByEmployeeDateAsync(DateTime date);
        Task<EmployeeDto> highestSaleByEmployeesYearAsync(int year);
        Task<EmployeeDto> lowestSaleByEmployeeDateAsync(DateTime date);
        Task<EmployeeDto> lowestSaleByEmployeesYearAsync(int year);
        Task<List<SaleEmployeeDto>> saleEmployeeByDateAsync(DateTime date, int id);
        Task<List<SaleEmployeeDto>> SaleEmployeeBetweenDateAsync(int id, DateTime fromdate, DateTime todate);
        Task<IEnumerable<object>> companyNameAsync(int id);

    }
}
