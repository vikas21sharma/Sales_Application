using Sales_Application.DTO;
using Sales_Application.Models;

namespace SprintSalesTesting.Mock
{
    public class CustomResponse
    {
        public string Message { get; set; }
        public List<TerritoryDto> Territories { get; set; } = new List<TerritoryDto>();
    }
    public class MockData
    {
        public static TerritoryDto GetTerritoryDto()
        {

            return new TerritoryDto
            {
                TerritoryId = "0909",
                TerritoryDescription = "Test",
                RegionId = 1
            };
        
        }
        public static Territory GetTerritory()
        {

            return new Territory
            {
                TerritoryId = "0909",
                TerritoryDescription = "Test",
                RegionId = 1
            };

        }

        public static List<TerritoryDto> GetTerritoryDtoList()
        {
            return new List<TerritoryDto>
            {
                    new TerritoryDto
            {
                TerritoryId = "0909",
                TerritoryDescription = "Test",
                RegionId = 1
            },
                            new TerritoryDto
            {
                TerritoryId = "0909",
                TerritoryDescription = "Test",
                RegionId = 1
            }
            };
        }

        public static List<Territory> GetTerritoryList()
        {
            return new List<Territory>
            {
                    new Territory
            {
                TerritoryId = "0909",
                TerritoryDescription = "Test",
                RegionId = 1
            }
            };
        }

    }
    }
