using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Entities
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validName = "Product Name";
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.Name = validName;

            // Assert
            Assert.AreEqual(validName, product.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_NullOrEmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string nullName = null;
            string emptyName = "";
            Product product = new Product(1, "", "", "", null, 0);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => product.Name = nullName);
            Assert.ThrowsException<ArgumentException>(() => product.Name = emptyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Manufacturer_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validManufacturer = "Manufacturer";
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.Manufacturer = validManufacturer;

            // Assert
            Assert.AreEqual(validManufacturer, product.Manufacturer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Manufacturer_Set_NullOrEmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string nullManufacturer = null;
            string emptyManufacturer = "";
            Product product = new Product(1, "", "", "", null, 0);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => product.Manufacturer = nullManufacturer);
            Assert.ThrowsException<ArgumentException>(() => product.Manufacturer = emptyManufacturer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Description_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validDescription = "Product description";
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.Description = validDescription;

            // Assert
            Assert.AreEqual(validDescription, product.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Description_Set_NullOrEmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string nullDescription = null;
            string emptyDescription = "";
            Product product = new Product(1, "", "", "", null, 0);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => product.Description = nullDescription);
            Assert.ThrowsException<ArgumentException>(() => product.Description = emptyDescription);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Price_Set_ValidValue_SetsValue()
        {
            // Arrange
            decimal validPrice = 10.5m;
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.Price = validPrice;

            // Assert
            Assert.AreEqual(validPrice, product.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Price_Set_NegativeOrZeroValue_ThrowsArgumentException()
        {
            // Arrange
            decimal negativePrice = -10.5m;
            decimal zeroPrice = 0;
            Product product = new Product(1, "", "", "", null, 0);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => product.Price = negativePrice);
            Assert.ThrowsException<ArgumentException>(() => product.Price = zeroPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsOnSale_Set_True_SetsValue()
        {
            // Arrange
            bool isOnSale = true;
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.IsOnSale = isOnSale;

            // Assert
            Assert.AreEqual(isOnSale, product.IsOnSale);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsOnSale_Set_False_SetsValue()
        {
            // Arrange
            bool isOnSale = false;
            Product product = new Product(1, "", "", "", null, 0);

            // Act
            product.IsOnSale = isOnSale;

            // Assert
            Assert.AreEqual(isOnSale, product.IsOnSale);
        }
      

        [TestMethod]        [ExpectedException(typeof(ArgumentException))]
        public void FullName_Get_WithoutMedicineInfo_ReturnsCorrectFullName()
        {
            // Arrange
            var name = "Product Name";
            var expectedFullName = name;
            var product = new Product(1, name, "", "", null, 0);

            // Act
            var actualFullName = product.FullName;

            // Assert
            Assert.AreEqual(expectedFullName, actualFullName);
        }

  
    }
}