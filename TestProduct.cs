using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorThis.Models;

namespace TestRefactorThis
{
    // Very basic CRUD testing on products
    [TestClass]
    public class TestProduct
    {
        [TestMethod]
        public void Product_AddNewProduct_ReturnsTrue()
        {
            var product = new Product
            {
                Name = "New Samsung",
                Description = "This is the new Samsung phone",
                Price = 1299.99m,
                DeliveryPrice = 17.50m
            };


            var result = product.Save();
            Assert.IsTrue(result);
            product.Delete();
        }

        [TestMethod]
        public void Product_UpdateProduct_ReturnsTrue()
        {
            var product = new Product
            {
                Name = "Apple",
                Description = "This is the new Apple phone",
                Price = 199.99m,
                DeliveryPrice = 10.50m
            };
            var result = product.Save();
            Assert.IsTrue(result);

            // Edit the Product
            product.Name = "Samsung";
            product.Description = "Testing the Samsung Phone";
            product.Price = 1199.99m;
            product.DeliveryPrice = 110.50m;
            result = product.Save();
            Assert.IsTrue(result);


            product.Delete();
        }

        [TestMethod]
        public void Product_DeleteProduct_ReturnsTrue()
        {
            var product = new Product();
            var result = product.Delete();
            Assert.IsTrue(result);
        }
    }
}