using Ecommerce.BLL;
using Ecommerce.Data;
using Ecommerce.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEcommerce;

[TestClass]
public class OrderBLLTests
{
    private Mock<IRepository<Products, Guid>> _mockProductRepo = new Mock<IRepository<Products, Guid>>();
    private Mock<IRepository<Cart, int>> _mockCartRepo = new Mock<IRepository<Cart, int>>();
    private Mock<IRepository<Order, int>> _mockOrderRepo = new Mock<IRepository<Order, int>>();
    private Mock<IRepository<Country, int>> _mockCountryRepo = new Mock<IRepository<Country, int>>();

    public OrderBLL InitializeBLL()
    {
        return new OrderBLL(_mockProductRepo.Object, _mockCartRepo.Object, _mockCountryRepo.Object, _mockOrderRepo.Object);
    }

    [TestMethod]
    public void ConvertedPrice_ShouldCalculateCorrectly()
    {
        // Arrange
        var countryBLL = InitializeBLL();
        
        var deliveryCountry = "Country A";
        var country = new Country
        {
            CountryName = deliveryCountry,
            ConversionRate = 1.5m, 
            TaxRate = 0.1, 
        };

        _mockCountryRepo.Setup(repo => repo.GetAll()).Returns(new List<Country> { country });

        // Act
        var result = countryBLL.convertedPrice(100.0m, deliveryCountry);

        // Assert
        Assert.AreEqual(1.5m * 100.0m, result.ConvertedPrice);
        Assert.AreEqual(0.1, (double)result.TaxRate);
        Assert.AreEqual(1.5m * 100.0m * 0.1m + 1.5m * 100.0m, result.TotalPriceWithTaxes);
    }

    [TestMethod]
    public void ConvertedPrice_ShouldHandleInvalidCountry()
    {
        // Arrange
        var countryBLL = InitializeBLL();

        _mockCountryRepo.Setup(repo => repo.GetAll()).Returns(new List<Country>());

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => countryBLL.convertedPrice(100.0m, "Non-Existent Country"));
    }

    [TestMethod]
    public void CreateOrder_ShouldCreateOrderSuccessfully()
    {
        // Arrange
        var orderBLL = InitializeBLL();
        var address = "123 Main St";
        var mailingCode = "12345";
        var deliveryCountry = "Country A";
        var totalPriceWithTaxes = 100.0m;

        var cartItems = new List<Cart>
        {
            new Cart { OrderID = 1, ProductName = "Product A", ItemsNumInCart = 3 },
            new Cart { OrderID = 1, ProductName = "Product B", ItemsNumInCart = 2 },
        };

        _mockCartRepo.Setup(repo => repo.GetAll()).Returns(cartItems);
        _mockOrderRepo.Setup(repo => repo.Create(It.IsAny<Order>())).Callback<Order>(o => cartItems[0].OrderID = o.Id+1);

        // Act
        orderBLL.CreateOrder(address, mailingCode, deliveryCountry, totalPriceWithTaxes);

        // Assert
        Assert.AreEqual(1, cartItems[0].OrderID);
    }

    [TestMethod]
    public void CreateOrder_ShouldHandleError()
    {
        // Arrange
        var orderBLL = InitializeBLL();

        // Simulate an error by setting cartItems to null
        _mockCartRepo.Setup(repo => repo.GetAll()).Returns((List<Cart>)null);

        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => orderBLL.CreateOrder("123 Main St", "12345", "Country A", 100.0m));
    }

    [TestMethod]
    public void GetAll_ShouldReturnOrders()
    {
        // Arrange
        var orderBLL = InitializeBLL();

        var orders = new List<Order>
        {
            new Order { Id = 1, Address = "Address 1" },
            new Order { Id = 2, Address = "Address 2" },
        };

        _mockOrderRepo.Setup(repo => repo.GetAll()).Returns(orders);

        // Act
        var result = orderBLL.GetAll();

        // Assert
        CollectionAssert.AreEqual(orders, result.ToList());
    }

    [TestMethod]
    public void GetAll_ShouldHandleError()
    {
        // Arrange
        var orderBLL = InitializeBLL();

        _mockOrderRepo.Setup(repo => repo.GetAll()).Returns((List<Order>)null);

        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => orderBLL.GetAll());
    }
}
