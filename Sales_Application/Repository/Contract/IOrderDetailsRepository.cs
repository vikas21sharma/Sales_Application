using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Repository.Contract
{
    public interface IOrderDetailsRepository
    {
        Task<List<OrderDetailDto>> displayAllEmployeeAsync();

        Task<List<BillAmountDto>> displayAllBillAmountAsync(int id);
    }
}
