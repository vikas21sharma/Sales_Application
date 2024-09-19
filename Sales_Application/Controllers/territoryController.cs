using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Sales_Application.DTO;
using Sales_Application.Repository.Contract;


namespace Sales_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class territoryController : ControllerBase
    {
        private readonly ITerritoryRepository territoryRepository;
        public territoryController(ITerritoryRepository territoryRepository)
        {
            this.territoryRepository = territoryRepository;  
        }

        /// <summary>
        /// Add New Territory
        /// </summary>
        /// <param name="territorydto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNewTerritory([FromBody] TerritoryDto territorydto)
        {
            var result = await territoryRepository.addNewTerritory(territorydto);
            Log.Information("New Territories have been added successfully");
            return Ok(result);
        }

        /// <summary>
        /// Get All Territory
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTerritory()
        {
            
            var result = await territoryRepository.getAllTerritory();
            if (result == null)
            {
                Log.Information("No Territories were found.");
                return NotFound("Records Not Available");
            }
            var response = new { Message = "Collection of Territory", Collection = result };

            Log.Information("Territories were found.");
            return Ok(response);
        }

        /// <summary>
        /// Updating the Territory
        /// </summary>
        /// <param name="Territoryid"></param>
        /// <param name="territoryDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("edit/{Territoryid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTheTerritory([FromRoute] string Territoryid, [FromBody] TerritoryDto territoryDto)
        {
            var result = await territoryRepository.updateTheTerritory(Territoryid, territoryDto);
            if (result == null)
            {
                Log.Information("No Id were found.");
                return NotFound("Id not Available");
            }
            Log.Information("Territories were Updated.");
            return Ok(result);
        }


       /// <summary>
       /// Deleting Territory
       /// </summary>
       /// <param name="Territoryid"></param>
       /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{Territoryid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTheTerritory([FromRoute] string Territoryid)
        {
            var result = await territoryRepository.deleteTheTerritory(Territoryid);
            if (result == null)
            {
                Log.Information("No Id were found.");
                return NotFound("Id is not present in the table.");
            }
            Log.Information("Deleted successfully");
            return Ok(result);
        }
    }
}
