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
public class CartBLLTests
{
    private Mock<IRepository<Products, Guid>> _mockProductRepo = new Mock<IRepository<Products, Guid>>();
    private Mock<IRepository<Cart, int>> _mockCartRepo = new Mock<IRepository<Cart, int>>();
    private Mock<IRepository<Order, int>> _mockOrderRepo = new Mock<IRepository<Order, int>>();
    private Mock<IRepository<Country, int>> _mockCountryRepo = new Mock<IRepository<Country, int>>();

    public CartBLL InitializeBLL()
    {
        return new CartBLL(_mockProductRepo.Object, _mockCartRepo.Object, _mockCountryRepo.Object, _mockOrderRepo.Object);
    }

    [TestMethod]
    public void GetAllCarts_ShouldReturnCartItemsWithMaxOrderID()
    {
        // Arrange
        var productBLL = InitializeBLL();

        var orderCount = 3; 
        var cartItems = new List<Cart>
        {
            new Cart { OrderID = 1, ProductName = "Product A" },
            new Cart { OrderID = 2, ProductName = "Product B" },
            new Cart { OrderID = 3, ProductName = "Product C" },
            new Cart { OrderID = 4, ProductName = "Product D" },
        };

        _mockCartRepo.Setup(repo => repo.GetAll()).Returns(cartItems);
        _mockOrderRepo.Setup(repo => repo.GetAll()).Returns(new List<Order> { new Order(), new Order(), new Order() });

        // Act
        var result = productBLL.GetAllCarts();

        // Assert
        Assert.AreEqual(1, result.Count()); 
        Assert.AreEqual("Product D", result.First().ProductName);
    }

    [TestMethod]
    public void RemoveFromCart_ShouldRemoveItemAndDecrementQuantity()
    {
        // Arrange
        var productBLL = InitializeBLL();
        var orderId = 1;
        var pGuid = Guid.NewGuid();
        var products = new List<Products>
        {
            new Products { GUID = pGuid, Name = "Product A", Description="aaaa",AvailableQuantity = 5,PriceCAD= 14.99m },
        };

        var cart = new Cart
        {
            OrderID = orderId,
            ProductName = "Product A",
            ItemsNumInCart = 3,
        };

        _mockProductRepo.Setup(repo => repo.GetAll()).Returns(products);
        _mockProductRepo.Setup(repo => repo.Get(pGuid)).Returns((Guid pGuid) => products.FirstOrDefault(p => p.GUID == pGuid));
        _mockCartRepo.Setup(repo => repo.Get(orderId)).Returns(cart);

        // Act
        productBLL.RemoveFromCart(orderId);

        // Assert
        Assert.AreEqual(2, cart.ItemsNumInCart);
        Assert.AreEqual(6, products[0].AvailableQuantity);
    }

    [TestMethod]
    public void RemoveFromCart_ShouldHandleInvalidItemId()
    {
        // Arrange
        var productBLL = InitializeBLL();
        var cartItemId = 1;

        _mockCartRepo.Setup(repo => repo.Get(cartItemId)).Returns((Cart)null);

        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => productBLL.RemoveFromCart(cartItemId));
    }

    [TestMethod]
    public void TotalPrice_ShouldCalculateTotalPrice()
    {
        // Arrange
        var productBLL = InitializeBLL();
        var pGuid1 = Guid.NewGuid();
        var pGuid2 = Guid.NewGuid();
        var orderCount = 1;
        var cartItems = new List<Cart>
        {
            new Cart { OrderID = 1, ProductName = "Product A", ItemsNumInCart = 3 },
            new Cart { OrderID = 1, ProductName = "Product B", ItemsNumInCart = 2 },
        };
        var products = new List<Products>
        {
            new Products { GUID = pGuid1, Name = "Product A", PriceCAD = 10.0m },
            new Products { GUID = pGuid2, Name = "Product B", PriceCAD = 15.0m },
        };

        _mockProductRepo.Setup(repo => repo.GetAll()).Returns(products);
        _mockOrderRepo.Setup(repo => repo.GetAll()).Returns(new List<Order> { });
        _mockCartRepo.Setup(repo => repo.GetAll()).Returns(cartItems);
        //_mockProductRepo.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns((Guid pGuid) => new Products { GUID = pGuid, Name = "Product A", PriceCAD = 10.0m });
        _mockProductRepo.Setup(repo => repo.Get(It.IsAny<Guid>()))
        .Returns((Guid pGuid) =>
        {
            string productName = cartItems.FirstOrDefault(cartItem => cartItem.OrderID == orderCount)?.ProductName;
            if (productName == "Product A")
            {
                return new Products { GUID = pGuid, Name = "Product A", PriceCAD = 10.0m };
            }
            else if (productName == "Product B")
            {
                return new Products { GUID = pGuid, Name = "Product B", PriceCAD = 15.0m };
            }
            
            return null; 
        });


        // Act
        var result = productBLL.totalPrice();

        // Assert
        Assert.AreEqual(30.0m + 20.0m, result); 
    }

    [TestMethod]
    public void GetAllCountries_ShouldReturnListOfCountries()
    {
        // Arrange
        var countryBLL = InitializeBLL();
        
        var countries = new List<Country>
        {
            new Country { Id = 1, CountryName = "Country A" },
            new Country { Id = 2, CountryName = "Country B" },
            new Country { Id = 3, CountryName = "Country C" },
        };

        _mockCountryRepo.Setup(repo => repo.GetAll()).Returns(countries);

        // Act
        var result = countryBLL.GetAllCountries();

        // Assert
        CollectionAssert.AreEqual(countries, result.ToList());
    }

    [TestMethod]
    public void GetAllCountries_ShouldHandleEmptyList()
    {
        // Arrange
        var countryBLL = InitializeBLL();

        _mockCountryRepo.Setup(repo => repo.GetAll()).Returns(new List<Country>());

        // Act
        var result = countryBLL.GetAllCountries();

        // Assert
        Assert.IsNotNull(result); 
        Assert.IsFalse(result.Any()); 
    }
}
