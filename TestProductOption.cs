using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorThis.Models;

namespace TestRefactorThis
{
    // Very basic CRUD testing on product options

    [TestClass]
    public class TestProductOption
    {
        public Product product { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            product = new Product
            {
                Name = "New Samsung",
                Description = "This is the new Samsung phone",
                Price = 1299.99m,
                DeliveryPrice = 17.50m
            };
            product.Save();
        }


        [TestMethod]
        public void ProductOption_AddNewProductOption_ReturnsTrue()
        {
            var ProductOption = new ProductOption
            {
                ProductId = product.Id,
                Name = "Blue Phone",
                Description = "This is a blue Samsung phone"
            };

            var result = ProductOption.Save();
            Assert.IsTrue(result);

            ProductOption.Delete();
        }

        [TestMethod]
        public void ProductOption_UpdateProductOption_ReturnsTrue()
        {
            var ProductOption = new ProductOption
            {
                ProductId = product.Id,
                Name = "Red Phone",
                Description = "This is a Red Samsung phone"
            };
            var result = ProductOption.Save();
            Assert.IsTrue(result);

            ProductOption.Name = "Green Phone";
            ProductOption.Description = "Testing the Samsung Green Phone";
            result = ProductOption.Save();
            Assert.IsTrue(result);

            ProductOption.Delete();
        }

        [TestMethod]
        public void ProductOption_DeleteProductOption_ReturnsTrue()
        {
            var ProductOption = new ProductOption();
            var result = ProductOption.Delete();
            Assert.IsTrue(result);
        }
    }
}