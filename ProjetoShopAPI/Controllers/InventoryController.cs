using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoShopAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ProjetoShopAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    private readonly IConfiguration _configuration;

    public InventoryController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpPost]
    [Obsolete]
    public ActionResult SaveInventoryData([FromBody] InventoryRequestDto requestDto)
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_SaveInventoryData",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      command.Parameters.AddWithValue("@ProductId", requestDto.ProductId);
      command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
      command.Parameters.AddWithValue("@AvailableQuantity", requestDto.AvailableQuantity);
      command.Parameters.AddWithValue("@ReorderPoint", requestDto.ReorderPoint);

      connection.Open();

      command.ExecuteNonQuery();

      connection.Close();

      return Ok();
    }

    [HttpGet]
    [Obsolete]
    public ActionResult GetInventoryData()
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_GetInventoryData",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      connection.Open();

      List<Models.InventoryDto> response = new List<Models.InventoryDto>();

      using (SqlDataReader sqlDataReader = command.ExecuteReader())
      {
        while(sqlDataReader.Read())
        {
          InventoryDto inventoryDto = new InventoryDto();
          inventoryDto.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
          inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
          inventoryDto.AvailableQuantity = Convert.ToInt32(sqlDataReader["AvailableQuantity"]);
          inventoryDto.ReorderPoint = Convert.ToInt32(sqlDataReader["ReorderPoint"]);

          response.Add(inventoryDto);
        }
      }

      connection.Close();

      return Ok(response);
    }

    [HttpDelete("{productId}")]
    [Obsolete]
    public ActionResult DeleteInventoryData(int productId)
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_DeleteInventoryDetails",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      connection.Open();

      command.Parameters.AddWithValue("@ProductId", productId);
      command.ExecuteNonQuery();

      connection.Close();

      return Ok();
    }

    [HttpPut]
    [Obsolete]
    public ActionResult UpdateInventoryData(InventoryRequestDto inventoryRequest)
    {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = _configuration.GetConnectionString("DefaultConnection")
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_UpdateInventoryData",
        CommandType = CommandType.StoredProcedure,
        Connection = connection
      };

      connection.Open();

      command.Parameters.AddWithValue("@ProductId", inventoryRequest.ProductId);
      command.Parameters.AddWithValue("@ProductName", inventoryRequest.ProductName);
      command.Parameters.AddWithValue("@AvailableQuantity", inventoryRequest.AvailableQuantity);
      command.Parameters.AddWithValue("@ReorderPoint", inventoryRequest.ReorderPoint);

      command.ExecuteNonQuery();

      connection.Close();

      return Ok();
    }
  }
}
