using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Get Method for All Orders
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await ordersRepository.getAllOrdersAsync();
            if (result == null)
            {
                Log.Information("Orders not Available.");
                return NotFound("Orders not available.");
            }
            Log.Information("Orders are Available.");
            var response = new
            {
                Message = "Collection of Orders",
                result
            };
            return Ok(response);
        }

        /// <summary>
        /// Get Orders By Name
        /// </summary>
        /// <param name="Firstname"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet("orderbyemployee/{Firstname}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisplayAllOrderByName([FromRoute] string Firstname)
        {
            var result = await ordersRepository.displayAllOrderByNameAsync(Firstname);
            if (result == null)
            {
                Log.Information("First Name not Available.");
                return NotFound("First Name not available.");
            }

            Log.Information("Orders Available for the First Name.");
            var response = new
            {
                Message = "Collection of Orders",
                result
            };
            return Ok(response);
        }

        /// <summary>
        /// Ship Details By Id
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("shipdetails/{OrderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ShipDetailsById([FromRoute] int OrderId)
        {
            var result = await ordersRepository.shipDetailsByIdAsync(OrderId);
            if (result == null)
            {
                Log.Information("Order Id not Available.");
                return NotFound("Order Id not available.");
            }
            Log.Information("Orders Ship Details are Available.");
            var response = new
            {
                Message = "Collection of Ship Details",
                result
            };
            return Ok(response);
        }

        /// <summary>
        /// Ship Details By Date
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("BetweenDate /{FromDate}/{ToDate}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ShipDetailsByDate([FromRoute] DateTime FromDate, [FromRoute] DateTime ToDate)
        {
            var result = await ordersRepository.shipDetailsByDateAsync(FromDate, ToDate);
            if (result == null)
            {
                Log.Information("Ship Details not Available on the given date.");
                return NotFound("Ship Details not available on the given date.");
            }
            Log.Information("Ship Details are Available.");
            var response = new
            {
                Message = "Collection of Ship Details",
                result
            };
            return Ok(response);
        }

        /// <summary>
        /// All Ship Details
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("allshipdetails")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AllShipDetails()
        {
            var result = await ordersRepository.allShipDetailsAsync();
            if (result == null)
            {
                Log.Information("Ship Details not Available.");
                return NotFound("Records not available.");
            }
            Log.Information("Ship Details are Available.");
            var response = new
            {
                Message = "Collection of Ship Details",
                result
            };
            return Ok(response);
        }

        /// <summary>
        /// All Orders By Employee
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("numberoforderbyeachemployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AllOrdersByEmployee()
        {
            var result = await ordersRepository.allOrdersByEmployeeAsync();
            if (result == null)
            {
                Log.Information("Orders not Available.");
                return NotFound("Orders not available.");
            }
            Log.Information("Orders are Available.");
            var response = new
            {
                Message = "Collection of Employee",
                result
            };
            return Ok(response);
        }

    }
}
