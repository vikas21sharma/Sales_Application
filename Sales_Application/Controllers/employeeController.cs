using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Sales_Application.DTO;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public employeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            var result = await employeeRepository.addEmployeeAsync(employeeDto);
            Log.Information(result);
            return Ok(result);

        }

      /// <summary>
      /// Display all Employees
      /// </summary>
      /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisplayAllEmployee()
        {
            var result = await employeeRepository.displayAllEmployeeAsync();

            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records are Available");
            return Ok(response);
        }

        /// <summary>
        /// Edit Employee Details
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        [HttpPut("edit/{EmployeeID}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditEmployeeDetails(int EmployeeID, [FromBody] EmployeeDto employeeDto)
        {
            var result = await employeeRepository.editEmployeeDetailsAsync(EmployeeID, employeeDto);

            if (result == null)
            {
                Log.Information("Employee Id is not Available.");
                return BadRequest("Id not found");
            }
            Log.Information("Records were available.");
            return Ok(result);
        }

        /// <summary>
        /// Patch Employee Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PatchShipper([FromRoute] int id, [FromBody] JsonPatchDocument employee)
        {

            var result = await employeeRepository.patchShipperDetailsAsync(id, employee);
            if (result == null)
            {
                Log.Information("Employee Id is not Available.");
                return NotFound("Id Not Found");
            }

            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records are available");
            return Ok(response);

        }

        /// <summary>
        /// Emplyee By Title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("title/{title}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmployeeByTitle([FromRoute] string title)
        {
            var result = await employeeRepository.employeeByTitleAsync(title);

            if (result == null)
            {
                Log.Information("Title is not Available.");
                return NotFound("Title Not Found");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }
        
        /// <summary>
        /// Employee By City
        /// </summary>
        /// <param name="City"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("City/{City}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmployeeByCity([FromRoute] string City)
        {
            var result = await employeeRepository.employeeByCityAsync(City);

            if (result == null)
            {
                Log.Information("City is not Available.");
                return NotFound("City Not Found");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were not available");
            return Ok(response);
        }

        /// <summary>
        /// Employee By Region
        /// </summary>
        /// <param name="Region"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Region/{Region}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmployeeByRegion([FromRoute] string Region)
        {
            var result = await employeeRepository.employeeByRegionAsync(Region);

            if (result == null)
            {
                Log.Information("Region is not Available.");
                return NotFound("Region Not Found");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Employee By Hire Date
        /// </summary>
        /// <param name="HireDate"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("HireDate/{HireDate}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmployeeByHireDate([FromRoute] DateTime HireDate)
        {
            var result = await employeeRepository.employeeByHireDateAsync(HireDate);

            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were available");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Highest Sale By Employee in a Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("highestsalebyemployeeDate/{date}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HighestSaleByEmployeeDate(DateTime date)
        {
            var result = await employeeRepository.highestSaleByEmployeeDateAsync(date);

            if(result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records Were Available");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);

        }

        /// <summary>
        /// Highest Sale Employee in a Year
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("highestsalebyemployeeYear/{Year}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HighestSaleByEmployeesYear([FromRoute] int Year)
        {
            var result = await employeeRepository.highestSaleByEmployeesYearAsync(Year);
            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were available for this year");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Lowest sale Employee By Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("lowestsalebyemployeeDate/{date}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LowestSaleByEmployeeDate(DateTime date)
        {
            var result = await employeeRepository.lowestSaleByEmployeeDateAsync(date);
            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were available");
            }
            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        ///  Lowest Sale Employee in a Year
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("lowestsalebyemployeeYear/{Year}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LowestSaleByEmployeesYear([FromRoute] int Year)
        {
            var result = await employeeRepository.lowestSaleByEmployeesYearAsync(Year);
            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were found for this year.");
            }

            var response = new
            {
                Title = "Collection of Employees",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Collection of OrderId & Company Name
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Employeeid"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("salemadebyanemployee/{Employeeid}/{date}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaleEmployeeByDate([FromRoute] DateTime date, [FromRoute] int Employeeid)
        {
            var result = await employeeRepository.saleEmployeeByDateAsync(date, Employeeid);
            if (result == null)
            {
                Log.Information("Records were available");
                return NotFound("No Records were availble for the specified Requirements");
            }

            var response = new
            {
                Title = "Collection of OrderId/Company Name",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Collection of Order ID & Company Name
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Admin")]
        [HttpGet("Salemadebyanemployeebetweendates/{EmployeeId}/{fromdate}/{todate}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaleEmployeeBetweenDate([FromRoute] int EmployeeId, [FromRoute] DateTime fromdate, [FromRoute] DateTime todate)
        {
            var result = await employeeRepository.SaleEmployeeBetweenDateAsync(EmployeeId, fromdate, todate);
            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were available");
            }

            var response = new
            {
                Title = "Collection of OrderId/Company Name",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }

        /// <summary>
        /// Collection of Company Name
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("companyname/{EmployeeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CompanyName([FromRoute] int EmployeeID)
        {
            var result = await employeeRepository.companyNameAsync(EmployeeID);
            if (result == null)
            {
                Log.Information("No Records were available");
                return NotFound("No Records were available for this ID");
            }

            var response = new
            {
                Title = "Collection of Company Name",
                result
            };
            Log.Information("Records were available");
            return Ok(response);
        }
    }
}
