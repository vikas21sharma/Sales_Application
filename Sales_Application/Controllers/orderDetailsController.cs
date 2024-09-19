using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales_Application.Repository.Contract;
using Serilog;

namespace Sales_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsRepository orderDetailsRepository;
        public orderDetailsController(IOrderDetailsRepository orderDetailsRepository)
        {
            this.orderDetailsRepository = orderDetailsRepository;
        }

        /// <summary>
        /// Get Method For All Employees
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisplayAllEmployee()
        {
            var result = await orderDetailsRepository.displayAllEmployeeAsync();
            if (result == null)
            {
                Log.Information("Order Details not Available.");
                return NotFound("Order Details not available.");
            }
            Log.Information("Order Details are Available.");
            var response = new
            {
                Message = "Collection of Order details",
                result
            };
                return Ok(response);   
        }

        /// <summary>
        /// Bill Amount For Emplyee ID
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet("{EmployeeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BillAmount(int EmployeeID)
        {
                var result = await orderDetailsRepository.displayAllBillAmountAsync(EmployeeID);

                if(result == null)
                {
                Log.Information("Employee Id is not Available.");
                return NotFound("Employee Id is not Available");
                }

            Log.Information("Order Details are Available.");
            var response = new
            {
                Message = "Collection of Bill Amount",
                result
            };
            return Ok(response);
        }

    }
}
