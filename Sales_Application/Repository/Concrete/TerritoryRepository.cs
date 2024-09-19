using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Application.DTO;
using Sales_Application.Models;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Repository.Concrete
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly VikasSprintContext context;
        private readonly IMapper mapper;
        public TerritoryRepository(VikasSprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<string> addNewTerritory(TerritoryDto territorydto)
        {
            var domainModel = mapper.Map<Territory>(territorydto);
            await context.Territories.AddAsync(domainModel);
           await context.SaveChangesAsync();

            return "Record Created Successfully";
        }

        public async Task<List<TerritoryDto>> getAllTerritory()
        {
            var domainModel = await context.Territories.ToListAsync();
            var dto = mapper.Map<List<TerritoryDto>>(domainModel);
            if (dto == null)
            {
                return null;
            }
            return dto;

        }

        public async Task<string> updateTheTerritory(string id, TerritoryDto territory)
        {
            var result = context.Territories.FirstOrDefault(o => o.TerritoryId == id);

            if (result == null)
            {
                return null;
            }

            mapper.Map(territory, result);
            await context.SaveChangesAsync();
            return "Updated Successfully";

        }

        public async Task<string> deleteTheTerritory(string id)
        {
            var result = context.Territories.FirstOrDefault(o => o.TerritoryId == id);

            if (result == null)
            {
                return null;
            }

            context.Territories.Remove(result);
            await context.SaveChangesAsync();

            return "Deleted Successfully";
        }
    }
}
