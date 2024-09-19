using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Repository.Contract
{
    public interface IOrdersRepository
    {
        Task<List<OrdersDto>> getAllOrdersAsync();
        Task<List<OrdersDto>> displayAllOrderByNameAsync(string Firstname);
        Task<List<ShipDetailsDtoById>> shipDetailsByIdAsync(int id);
        Task<List<ShipDetailsDtoByDate>> shipDetailsByDateAsync(DateTime fromdate, DateTime todate);
        Task<List<ShipDetailsDto>> allShipDetailsAsync();
        Task<List<OrderByEmployeeNameDto>> allOrdersByEmployeeAsync();
    }
}
