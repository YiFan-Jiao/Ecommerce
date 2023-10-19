using Ecommerce.BLL;
using Ecommerce.Data;
using Ecommerce.Models;
using Moq;

namespace TestEcommerce
{
    [TestClass]
    public class ProductBLLTests
    {
        private Mock<IRepository<Products, Guid>> _mockProductRepo = new Mock<IRepository<Products, Guid>>();
        private Mock<IRepository<Cart, int>> _mockCartRepo = new Mock<IRepository<Cart, int>>();
        private Mock<IRepository<Order, int>> _mockOrderRepo = new Mock<IRepository<Order, int>>();

        public ProductBLL InitializeBLL()
        {
            return new ProductBLL(_mockProductRepo.Object, _mockCartRepo.Object, _mockOrderRepo.Object);
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnOrderedProducts()
        {
            // Arrange
            var products = new List<Products>
            {
                new Products { GUID = Guid.NewGuid(), Name = "B" },
                new Products { GUID = Guid.NewGuid(), Name = "A" },
                new Products { GUID = Guid.NewGuid(), Name = "C" }
            };
            _mockProductRepo.Setup(repo => repo.GetAll()).Returns(products);

            var productBLL = InitializeBLL();

            // Act
            var result = productBLL.GetAllProducts();

            // Assert
            CollectionAssert.AreEqual(products.OrderBy(p => p.Name).ToList(), result.ToList());
        }

        [TestMethod]
        public void GetAllProducts_ShouldHandleErrorGracefully()
        {
            // Arrange
            var mockProductRepo = new Mock<IRepository<Products, Guid>>();
            mockProductRepo.Setup(repo => repo.GetAll()).Throws(new ArgumentNullException("Simulated error"));

            var productBLL = InitializeBLL();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => productBLL.GetAllProducts());
        }

        [TestMethod]
        public void AddToCart_ShouldAddToExistingCart()
        {
            // Arrange
            var productBLL = InitializeBLL();

            var productId = Guid.NewGuid();

            var products = new List<Products>
        {
            new Products { GUID = productId, Name = "Product A", AvailableQuantity = 5 },
        };

            var carts = new List<Cart>
        {
            new Cart { OrderID = 1, ProductName = "Product A", ItemsNumInCart = 3 },
        };

            _mockProductRepo.Setup(repo => repo.Get(productId)).Returns(products[0]);
            _mockCartRepo.Setup(repo => repo.Update(It.IsAny<Cart>())).Callback<Cart>(c => carts[0] = c);
            _mockCartRepo.Setup(repo => repo.GetAll()).Returns(carts);
            _mockOrderRepo.Setup(repo => repo.GetAll()).Returns(new List<Order> { });

            // Act
            productBLL.AddToCart(productId);

            // Assert
            Assert.AreEqual(4, products[0].AvailableQuantity);
            Assert.AreEqual(4, carts[0].ItemsNumInCart);
        }

        [TestMethod]
        public void AddToCart_ShouldThrowExceptionWhenCartIsNull()
        {
            // Arrange
            var productBLL = InitializeBLL();

            _mockCartRepo.Setup(repo => repo.GetAll()).Returns((List<Cart>)null);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => productBLL.AddToCart(Guid.NewGuid()));
        }

        [TestMethod]
        public void Search_ShouldFilterProductsBySearchTerm()
        {
            // Arrange
            var productBLL = InitializeBLL();

            var products = new List<Products>
            {
                new Products { GUID = Guid.NewGuid(), Name = "Product A", Description = "Description A" },
                new Products { GUID = Guid.NewGuid(), Name = "Product B", Description = "Description B" },
                new Products { GUID = Guid.NewGuid(), Name = "Product C", Description = "Description C" },
            };

            _mockProductRepo.Setup(repo => repo.GetAll()).Returns(products);

            // Act
            var result = productBLL.Search("A");

            // Assert
            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count); 
            Assert.AreEqual("Product A", resultList[0].Name); 
        }
    }
}