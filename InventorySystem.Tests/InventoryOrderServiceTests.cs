using InventorySystem;

namespace InventorySystem.Tests;

public class InventoryOrderServiceTests
{
    [Fact]
    public void ProcessOrder_NormalOrder_ReturnsSuccessfulResult()
    {
        // Arrange
        var service = new InventoryOrderService();

        service.AddProduct(new Product
        {
            Id = "P100",
            Name = "Keyboard",
            UnitPrice = 100m,
            StockQuantity = 20
        });

        // Act
        var result = service.ProcessOrder("P100", 5, 0.05m);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(525m, result.TotalCost);
        Assert.Equal("Order processed successfully.", result.Message);
    }

    [Fact]
    public void ProcessOrder_ElevenItems_AppliesTenPercentDiscount()
    {
        // Arrange
        var service = new InventoryOrderService();

        service.AddProduct(new Product
        {
            Id = "P100",
            Name = "Keyboard",
            UnitPrice = 10m,
            StockQuantity = 20
        });

        // Act
        var result = service.ProcessOrder("P100", 11, 0m);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(99m, result.TotalCost);
    }
}
