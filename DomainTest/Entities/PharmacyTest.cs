using Domain.Common;
using Domain.Entities.Users;
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
    public class PharmacyTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Address_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validAddress = "123 Main St";
            Pharmacy pharmacy = new Pharmacy(1, "", null);

            // Act
            pharmacy.Address = validAddress;

            // Assert
            Assert.AreEqual(validAddress, pharmacy.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Address_Set_NullOrEmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string nullAddress = null;
            string emptyAddress = "";
            Pharmacy pharmacy = new Pharmacy(1, "", null);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => pharmacy.Address = nullAddress);
            Assert.ThrowsException<ArgumentException>(() => pharmacy.Address = emptyAddress);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Stock_Set_ValidValue_SetsValue()
        {
            // Arrange
            ProductStock validStock = new ProductStock();
            Pharmacy pharmacy = new Pharmacy(1, "", null);

            // Act
            pharmacy.Stock = validStock;

            // Assert
            Assert.AreEqual(validStock, pharmacy.Stock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WorkingPharmacist_Set_Null_SetsValue()
        {
            // Arrange
            Pharmacist validPharmacist = null;
            Pharmacy pharmacy = new Pharmacy(1, "", null);

            // Act
            pharmacy.WorkingPharmacist = validPharmacist;

            // Assert
            Assert.AreEqual(validPharmacist, pharmacy.WorkingPharmacist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WorkingPharmacist_Set_ValidValue_SetsValue()
        {
            // Arrange
            Pharmacist validPharmacist = new Pharmacist(1, "johndoe", "test21223", 1000);
            Pharmacy pharmacy = new Pharmacy(1, "", null);

            // Act
            pharmacy.WorkingPharmacist = validPharmacist;

            // Assert
            Assert.AreEqual(validPharmacist, pharmacy.WorkingPharmacist);
        }
    }
}