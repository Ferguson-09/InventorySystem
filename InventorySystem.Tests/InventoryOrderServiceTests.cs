using InventorySystem;

namespace InventorySystem.Tests;

public class InventoryOrderServiceTests
{
    [Fact]
    public void ProcessOrder_NormalOrder()
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
    public void ProcessOrder_TenPercentDiscount()
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

    [Fact]
    public void ProcessOrder_TwentyPercentDiscount()
    {
        // Arrange
        var service = new InventoryOrderService();

        service.AddProduct(new Product
        {
            Id = "P100",
            Name = "Keyboard",
            UnitPrice = 10m,
            StockQuantity = 60
        });

        // Act
        var result = service.ProcessOrder("P100", 50, 0m);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(400m, result.TotalCost);
    }

    // Failing tests
    [Fact]
    public void ProcessOrder_ZeroQuantity()
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
        var result = service.ProcessOrder("P100", 0, 0m);

        // Assert
        Assert.False(result.IsSuccess);
    }
    [Fact]
    public void ProcessOrder_ExactlyTenItems_TenPercentDiscount()
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
        var result = service.ProcessOrder("P100", 10, 0m);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(90m, result.TotalCost);
    }
    [Fact]
    public void ProcessOrder_ExactlyFiftyItems_TwentyPercentDiscount()
    {
        // Arrange
        var service = new InventoryOrderService();

        service.AddProduct(new Product
        {
            Id = "P100",
            Name = "Keyboard",
            UnitPrice = 10m,
            StockQuantity = 50
        });

        // Act
        var result = service.ProcessOrder("P100", 50, 0m);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(400m, result.TotalCost);
    }
    [Fact]
    public void AddProduct_NullProduct_ArgumentException()
    {
        // Arrange
        var service = new InventoryOrderService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            service.AddProduct(null!));
    }

    [Fact]
    public void AddProduct_EmptyProductId_ThrowsArgumentException()
    {
        // Arrange
        var service = new InventoryOrderService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            service.AddProduct(new Product
            {
                Id = "",
                Name = "Keyboard",
                UnitPrice = 100m,
                StockQuantity = 10
            }));
    }
}
