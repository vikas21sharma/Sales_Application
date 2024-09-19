using Microsoft.AspNetCore.JsonPatch;
using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Repository.Contract
{
    public interface IShipperRepository
    {
        Task<string> CreateAsync(ShipperDto shipperdto);

        Task<List<ShipperDto>> displayShipperAsync();

        Task<string> editShipperDetailsAsync(int id, ShipperDto shipperdto);

        Task<string> patchShipperDetailsAsync(int id, JsonPatchDocument Shipper);

        Task<ShipperDto> searchCompanyNameAsync(string companyName);

        Task<List<ShipperTotalShipmentDto>> totalShipmentAsync();

        Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperAsync();

        Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperDateAsync(DateTime date);

        Task<List<TotalAmountEarnedByShipperDto>> getTotalAmountEarnedByShipperYearAsync(int year);


    }
}
