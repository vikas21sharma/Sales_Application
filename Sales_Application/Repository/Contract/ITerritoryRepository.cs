using Sales_Application.DTO;
using Sales_Application.Models;

namespace Sales_Application.Repository.Contract
{
    public interface ITerritoryRepository
    {
        Task<string> addNewTerritory(TerritoryDto territorydto);

        Task<List<TerritoryDto>> getAllTerritory();

        Task<string> updateTheTerritory(string id, TerritoryDto territory);

        Task<string> deleteTheTerritory(string id);
    }
}
