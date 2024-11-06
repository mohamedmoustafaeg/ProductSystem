using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace xUnit_Test_Project
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockCategoryService = new Mock<ICategoryService>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new ProductController(_mockProductService.Object, _mockCategoryService.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Product1" } };
            _mockProductService.Setup(service => service.GetAll()).ReturnsAsync(products);
            _mockCategoryService.Setup(service => service.GetAllCategories()).ReturnsAsync(new List<Category>());

            // Act
            var result = await _controller.Index(null, "All") as ViewResult;
            var model = result?.Model as List<Product>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count, model?.Count);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectWhenModelStateIsValid()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1" };
            _mockProductService.Setup(service => service.Add(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Mock the user context
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
                    }))
                }
            };

            // Act
            var result = await _controller.Create(product);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Post_ReturnsRedirectWhenModelStateIsValid()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Updated Product" };
            _mockProductService.Setup(service => service.Update(It.IsAny<int>(), It.IsAny<Product>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "test-user-id")
                    }))
                }
            };

            // Act
            var result = await _controller.Edit(product);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToIndex()
        {
            // Arrange
            _mockProductService.Setup(service => service.DeleteById(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundWhenProductIsNull()
        {
            // Arrange
            _mockProductService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewWhenProductIsFound()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1" };
            _mockProductService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(product, viewResult.Model);
        }
    }
}
