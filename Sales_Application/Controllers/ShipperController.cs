using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales_Application.DTO;
using Sales_Application.Helper;
using Sales_Application.Repository.Contract;
using Serilog;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;

namespace Sales_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperRepository ship;
        private IConfiguration config;

        public ShipperController(IShipperRepository ship, IConfiguration config)
        {
            this.ship = ship;
            this.config = config;
        }

        /// <summary>
        /// Add Shipper Details
        /// </summary>
        /// <param name="shipperDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Shipper,Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddShipper([FromBody] ShipperDto shipperDto)
        {
            //var result = await ship.CreateAsync(shipperDto);
            //Log.Information("Shipper Added Successfully");
            //return Ok(result);

            bool _isuploaded = await PostHelper.UploadBlob(config, shipperDto);
            if (_isuploaded)
            {
                Log.Information("Shipper Added Successfully to Blob and Azure.");
                return Ok(new
                {
                    message = "Shipper add in Progress!!!"
                }
                );
            }
            return StatusCode(200);
        }

        /// <summary>
        /// All Shipper Details
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Shipper,Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> displayAllShipper()
        {
            var result = await ship.displayShipperAsync();
            if(result == null)
            {
                Log.Information("No Shipper were found");
                return NotFound("No Shipper were found");
            }
            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Shipper were Available");
            return Ok(response);
        }

        /// <summary>
        /// Edit Shipper Details
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <param name="shipperDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Shipper,Admin")]
        [HttpPut("edit/{ShipperID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int ShipperID, [FromBody] ShipperPutDto shipperDto)
        {
            //var result =  await ship.editShipperDetails(ShipperID, shipperDto);
            //if (result == null)
            //{
            //    return NotFound("Id Not Found!!!!");
            //}
            // return Ok(result);
            //

            bool _isuploaded = await PutHelper.UploadBlob(config, shipperDto);
            if (_isuploaded)
            {
                Log.Information("Shipper is being updated in the blob and azure.");
                return Ok(new
                {
                    message = "Shipper update in Progress!!!"
                }
                );
            }
            return StatusCode(200);
        }

        /// <summary>
        /// Patch Shipper Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shipper"></param>
        /// <returns></returns>
        [Authorize(Roles = "Shipper,Admin")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PatchShipper([FromRoute] int id, [FromBody] JsonPatchDocument shipper)
        {

            var result = await ship.patchShipperDetailsAsync(id, shipper);
            if (result == null)
            {
                Log.Information("Shipper Id not Found");
                return NotFound("Shipper Id Not Found");
            }
            Log.Information("Shipper Details Patched");
            return Ok(result);

        }

        /// <summary>
        /// Search Shipper By Company Name 
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Shipper,Admin")]
        [HttpGet("{CompanyName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCompanyName([FromRoute] string CompanyName)
        {
            var result = await ship.searchCompanyNameAsync(CompanyName);
            if (result == null)
            {
                Log.Information("Comapny Name not found");
                return NotFound("Company Name not Found");
            }
            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Shipper Details were found");
            return Ok(response);
        }

        /// <summary>
        /// Total Shipement made by shippers
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("TotalShipment")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TotalShipment()
        {
            var result = await ship.totalShipmentAsync();

            if (result == null)
            {
                Log.Information("No Records were found");
                return NotFound("No Records Found!!!");
            }

            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Records were found");
            return Ok(response);
        }

        /// <summary>
        /// Amount Earned by shippers
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Totalamountearnedbyshipper")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalAmountEarnedByShipper()
        {
            var result = await ship.getTotalAmountEarnedByShipperAsync();

            if (result == null)
            {
                Log.Information("No Records were found");
                return NotFound("No Records were found!!");
            }
            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Records were found");
            return Ok(response);
        }

        /// <summary>
        /// Amount Earned by shipper(Date)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>)
        [Authorize(Roles = "Admin")]
        [HttpGet("totalamountearnedbyshipper/{date}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalAmountEarnedByShipperDate([FromRoute] DateTime date)
        {
            var result = await ship.getTotalAmountEarnedByShipperDateAsync(date);
            if (result == null)
            {
                Log.Information("No Details for the given date were found");
                return NotFound("No Details for the given date were found");
            }
            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Records were found");
            return Ok(response);
        }

        /// <summary>
        /// Amount Earned By Shipper (Year)
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("totalamountearnedbyshipperbyYear/{year}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalAmountEarnedByShipperYear([FromRoute] int year)
        {
            var result = await ship.getTotalAmountEarnedByShipperYearAsync(year);
            if (result == null)
            {
                Log.Information("No Details for the given year were found");
                return NotFound("No Details for the given year were found");
            }
            var response = new { Message = "Collection of Shipper", Collection = result };
            Log.Information("Records were found");
            return Ok(response);
        }

    }
}
