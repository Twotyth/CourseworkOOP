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
    public class MedicineTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validName = "Medicine Name";
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act
            medicine.Name = validName;

            // Assert
            Assert.AreEqual(validName.Trim(), medicine.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_InvalidValue_ThrowsArgumentException()
        {
            // Arrange
            string invalidName = "Invalid Name!";
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => medicine.Name = invalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsPrescriptionRequired_Set_True_SetsValue()
        {
            // Arrange
            bool isPrescriptionRequired = true;
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act
            medicine.IsPrescriptionRequired = isPrescriptionRequired;

            // Assert
            Assert.AreEqual(isPrescriptionRequired, medicine.IsPrescriptionRequired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Type_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validType = "Type";
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act
            medicine.Type = validType;

            // Assert
            Assert.AreEqual(validType, medicine.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TypeCategory_Set_ValidValue_SetsValue()
        {
            // Arrange
            string validTypeCategory = "Category";
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act
            medicine.TypeCategory = validTypeCategory;

            // Assert
            Assert.AreEqual(validTypeCategory, medicine.TypeCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_Get_ReturnsSetValue()
        {
            // Arrange
            uint expectedId = 1;
            Medicine medicine = new Medicine(expectedId, "", false, "", "");

            // Act
            uint actualId = medicine.Id;

            // Assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            string nullName = null;
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => medicine.Name = nullName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_Set_EmptyValue_ThrowsArgumentException()
        {
            // Arrange
            string emptyName = "";
            Medicine medicine = new Medicine(1, "", false, "", "");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => medicine.Name = emptyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsPrescriptionRequired_Set_False_SetsValue()
        {
            // Arrange
            bool isPrescriptionRequired = false;
            Medicine medicine = new Medicine(1, "", true, "", "");

            // Act
            medicine.IsPrescriptionRequired = isPrescriptionRequired;

            // Assert
            Assert.AreEqual(isPrescriptionRequired, medicine.IsPrescriptionRequired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Type_Set_NullValue_SetsValue()
        {
            // Arrange
            string nullType = null;
            Medicine medicine = new Medicine(1, "", false, "Old Type", "");

            // Act
            medicine.Type = nullType;

            // Assert
            Assert.IsNull(medicine.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TypeCategory_Set_EmptyValue_SetsValue()
        {
            // Arrange
            string emptyTypeCategory = "";
            Medicine medicine = new Medicine(1, "", false, "", "Old Category");

            // Act
            medicine.TypeCategory = emptyTypeCategory;

            // Assert
            Assert.AreEqual(emptyTypeCategory, medicine.TypeCategory);
        }
    }
}
