using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoShopAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

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
    public ActionResult SaveCustomerData([FromBody] CustomerRequestDto requestDto)
    {
      try
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

        connection.Open();

        using (var check = new SqlCommand("SELECT COUNT(*) FROM CustomerDetails WHERE CustomerId = @CustomerId", connection))
        {
          check.Parameters.AddWithValue("@CustomerId", requestDto.CustomerId);
          int exists = (int)check.ExecuteScalar();

          if (exists > 0)
            return Conflict(new { message = "CustomerId already exists" });
        }

        command.Parameters.AddWithValue("@CustomerId", requestDto.CustomerId);
        command.Parameters.AddWithValue("@FirstName", requestDto.FirstName);
        command.Parameters.AddWithValue("@LastName", requestDto.LastName);
        command.Parameters.AddWithValue("@Email", requestDto.Email);
        command.Parameters.AddWithValue("@Phone", requestDto.Phone);
        command.Parameters.AddWithValue("@RegistrationDate", requestDto.RegistrationDate);

        command.ExecuteNonQuery();
        connection.Close();

        return Ok();
      }
      catch (SqlException sqlEx)
      {
        if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
          return Conflict(new { message = "Duplicate key detected." });

        return StatusCode(500, new { message = sqlEx.Message });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = ex.Message });
      }
    }

    [HttpGet]
    [Obsolete]
    public ActionResult GetCustomerData()
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_GetCustomerDetails",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      connection.Open();

      List<CustomerDto> customers = new List<CustomerDto>();

      using (SqlDataReader sqlDataReader = command.ExecuteReader())
      {
        while (sqlDataReader.Read())
        {
          CustomerDto customerDto = new CustomerDto();
          customerDto.CustomerId = Convert.ToInt32(sqlDataReader["CustomerId"]);
          customerDto.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
          customerDto.LastName = Convert.ToString(sqlDataReader["LastName"]);
          customerDto.Email = Convert.ToString(sqlDataReader["Email"]);
          customerDto.Phone = Convert.ToString(sqlDataReader["Phone"]);
          customerDto.RegistrationDate = Convert.ToDateTime(sqlDataReader["RegistrationDate"]);

          customers.Add(customerDto);
        }
      }

      connection.Close();

      return Ok(customers);
    }

    [HttpDelete("{customerId}")]
    [Obsolete]
    public ActionResult DeleteCustomerData(int customerId)
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_DeleteCustomerDetails",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      connection.Open();

      command.Parameters.AddWithValue("@CustomerId", customerId);
      command.ExecuteNonQuery();

      connection.Close();

      return Ok();
    }

    [HttpPut]
    [Obsolete]
    public ActionResult UpdateCustomerData(CustomerRequestDto customerRequest)
    {
      try
      {
        SqlConnection connection = new SqlConnection
        {
          ConnectionString = _configuration.GetConnectionString("DefaultConnection")
        };

        SqlCommand command = new SqlCommand
        {
          CommandText = "sp_UpdateCustomerDetails",
          CommandType = CommandType.StoredProcedure,
          Connection = connection
        };

        connection.Open();

        command.Parameters.AddWithValue("@CustomerId", customerRequest.CustomerId);
        command.Parameters.AddWithValue("@FirstName", customerRequest.FirstName);
        command.Parameters.AddWithValue("@LastName", customerRequest.LastName);
        command.Parameters.AddWithValue("@Email", customerRequest.Email);
        command.Parameters.AddWithValue("@Phone", customerRequest.Phone);
        command.Parameters.AddWithValue("@RegistrationDate", customerRequest.RegistrationDate);

        command.ExecuteNonQuery();
        connection.Close();

        return Ok();
      }
      catch (SqlException sqlEx)
      {
        if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
          return Conflict(new { message = "Duplicate key detected." });

        return StatusCode(500, new { message = sqlEx.Message });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = ex.Message });
      }
    }

  }
}
