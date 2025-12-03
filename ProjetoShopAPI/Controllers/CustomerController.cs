using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoShopAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ProjetoShopAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {

    private readonly IConfiguration _configuration;

    public CustomerController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpPost]
    [Obsolete]
    public ActionResult SaveCustomerData(CustomerRequestDto requestDto)
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_SaveCustomerDetails",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      command.Parameters.AddWithValue("@CustomerId", requestDto.CustomerId);
      command.Parameters.AddWithValue("@FirstName", requestDto.FirstName);
      command.Parameters.AddWithValue("@LastName", requestDto.LastName);
      command.Parameters.AddWithValue("@Email", requestDto.Email);
      command.Parameters.AddWithValue("@Phone", requestDto.Phone);
      command.Parameters.AddWithValue("@RegistrationDate", requestDto.RegistrationDate);


      connection.Open();

      command.ExecuteNonQuery();

      connection.Close();

      return Ok();
    }
  }
}
