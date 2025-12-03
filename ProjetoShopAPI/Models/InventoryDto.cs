namespace ProjetoShopAPI.Models
{
  public class InventoryDto
  {
    #region Properties
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int AvailableQuantity { get; set; }
    public int ReorderPoint { get; set; }
    #endregion
  }
}
