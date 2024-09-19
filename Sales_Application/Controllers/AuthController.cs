using Microsoft.AspNetCore.Mvc;
using Sales_Application.DTO;
using Sales_Application.Repository;
using Sales_Application.Repository.Contract;

namespace Sales_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly TokenRepository tokenRepository;
        public AuthController(IAuthRepository authRepository, TokenRepository tokenRepository)
        {
            this.authRepository = authRepository;
            this.tokenRepository = tokenRepository;
        }

        /// <summary>
        /// Auth for Admin Login
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        [HttpPost("Admin/Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AdminLogin([FromBody] AdminDto adminDto)
        {
            if (adminDto.FirstName.Length == 0 | adminDto.Password.Length == 0)
            {
                return BadRequest("Enter the Username or Password");
            }
            //Authenticating
            var username = await authRepository.adminUsernameAsync(adminDto);

            if (username == false)
            {
                return BadRequest("Entered Username is Not Admin");
            }
            //Authorization
            var password = await authRepository.adminPasswordAsync(adminDto);

            if (password == null)
            {
                return BadRequest("Entered Password is incorrect");
            }
            
            var token = tokenRepository.GenerateJwtToken(adminDto.Password, password);

            return Ok(token);
        }



        /// <summary>
        /// Employee Login
        /// </summary>
        /// <param name="employeeLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Employee/Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmployeeLogin([FromBody] EmployeeLoginDto employeeLoginDto)
        {
            if (employeeLoginDto.FirstName.Length == 0 | employeeLoginDto.Password.Length == 0)
            {
                return BadRequest("Enter the Username or Password");
            }
            //Authenticating
            var username = await authRepository.employeeUsernameAsync(employeeLoginDto);

            if (username == false)
            {
                return BadRequest("Entered username is incorrect");
            }

            //Authorization
            var rolename = await authRepository.employeePasswordAsync(employeeLoginDto);

            if (rolename == null)
            {
                return BadRequest("Password is incorrect"); 
            }
            var token = tokenRepository.GenerateJwtToken(employeeLoginDto.Password, rolename);
   
            return Ok(token);
        }



        /// <summary>
        /// Shipper Login
        /// </summary>
        /// <param name="shipperLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Shipper/Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ShipperLogin([FromBody] ShipperLoginDto shipperLoginDto)
        {
            if (shipperLoginDto.Email.Length == 0 | shipperLoginDto.Password.Length == 0)
            {
                return BadRequest("Enter the Username or Password");
            }
            
            //Authenticating
            var username = await authRepository.shipperUsernameAsync(shipperLoginDto);

            if (username == false)
            {
                return BadRequest("Entered email is incorrect");
            }

            //Authorization
            var rolename = await authRepository.shipperPasswordAsync(shipperLoginDto);

            if (rolename == null)
            {
                return BadRequest("Password is incorrect");
            }
            var token = tokenRepository.GenerateJwtToken(shipperLoginDto.Password, rolename);

            return Ok(token);
        }

    }
}
