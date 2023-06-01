using DataLayer.Dtos;
using DataLayer.Exceptions;
using DataLayer.Repositories;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTest.Repositories
{
    [TestClass]
    public class LocalMedicineReposTest
    {
        private const string TestJsonPath = "./TestJson";

        private static string CreateTestJsonDirectory()
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), TestJsonPath);
            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string basePath = "..\\..\\..\\..\\";

            // Delete all files in the test JSON folder
            DirectoryInfo directory = new DirectoryInfo(TestJsonPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            // Copy all files from PrescriptionMedicineProvider\.repos
            string reposPath = Path.Combine(basePath,  "PrescriptionMedicineProvider\\.repos");
            DirectoryInfo reposDirectory = new DirectoryInfo(reposPath);
            foreach (FileInfo file in reposDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(TestJsonPath, file.Name));
            }
        }
        [TestMethod]
        public void Add_ValidItem_AddsItemToList()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto(100, "Medicine 101", false, "Type 1", "Category 1");

            // Act
            repos.Add(item);
            Medicine addedItem = repos.Find(it => it.Name.Equals(item.Name))!;

            // Assert
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(item.Name, addedItem.Name);
            Assert.AreEqual(item.IsPrescriptionRequired, addedItem.IsPrescriptionRequired);
            Assert.AreEqual(item.Type, addedItem.Type);
            Assert.AreEqual(item.TypeCategory, addedItem.TypeCategory);
        }

        [TestMethod]
        public void Add_DuplicateItem_ThrowsDuplicateException()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item1 = new MedicineDto(100, "Medicine 102", false, "Type 1", "Category 1");
            MedicineDto item2 = new MedicineDto(200, "Medicine 102", true, "Type 2", "Category 2");
            repos.Add(item1);

            // Act & Assert
            Assert.ThrowsException<DuplicateException>(() => repos.Add(item2));
        }

        [TestMethod]
        public void Delete_ExistingItem_RemovesItemFromList()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto(100, "Medicine 104", false, "Type 1", "Category 1");
            repos.Add(item);
            Medicine addedItem = repos.Find(item.Id)!;

            // Act
            repos.Delete(addedItem);
            Medicine deletedItem = repos.Find(item.Id);

            // Assert
            Assert.IsNull(deletedItem);
        }

        [TestMethod]
        public void Delete_NonExistingItem_DoesNotThrowException()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto(100, "Medicine 105", false, "Type 1", "Category 1");
            repos.Add(item);
            Medicine nonExistingItem = new Medicine(999, "Non-existing", false, "Type", "Category");

            // Act & Assert
            repos.Delete(nonExistingItem);
        }

        [TestMethod]
        public void Find_ExistingId_ReturnsItem()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto(100, "Medicine 106", false, "Type 1", "Category 1");
            repos.Add(item);
            Medicine addedItem = repos.Find(it => it.Name.Equals(item.Name))!;

            // Act
            Medicine foundItem = repos.Find(it => it.Name.Equals(addedItem.Name))!;

            // Assert
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(addedItem.Id, foundItem.Id);
            Assert.AreEqual(addedItem.Name, foundItem.Name);
            Assert.AreEqual(addedItem.IsPrescriptionRequired, foundItem.IsPrescriptionRequired);
            Assert.AreEqual(addedItem.Type, foundItem.Type);
            Assert.AreEqual(addedItem.TypeCategory, foundItem.TypeCategory);
        }

        [TestMethod]
        public void Find_NonExistingId_ReturnsNull()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto(100, "Medicine 107", false, "Type 1", "Category 1");
            repos.Add(item);

            // Act
            Medicine? foundItem = repos.Find(999);

            // Assert
            Assert.IsNull(foundItem);
        }

        [TestMethod]
        public void Find_Predicate_ReturnsMatchingItem()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item1 = new MedicineDto(100, "Medicine 108", false, "Type 1", "Category 1");
            MedicineDto item2 = new MedicineDto(200, "Medicine 201", true, "Type 2", "Category 2");
            repos.Add(item1);
            repos.Add(item2);

            // Act
            Medicine? foundItem = repos.Find(med => med.Name.Equals(item2.Name));

            // Assert
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(item2.Name, foundItem.Name);
            Assert.AreEqual(item2.IsPrescriptionRequired, foundItem.IsPrescriptionRequired);
            Assert.AreEqual(item2.Type, foundItem.Type);
            Assert.AreEqual(item2.TypeCategory, foundItem.TypeCategory);
        }

        [TestMethod]
        public void FindAll_Predicate_ReturnsMatchingItems()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            


            MedicineDto item1 = new MedicineDto(100, "Medicine 109", false, "Type 12", "Category 1");
            MedicineDto item2 = new MedicineDto(200, "Medicine 202", true, "Type 1", "Category 2");
            MedicineDto item3 = new MedicineDto(300, "Medicine 301", true, "Type 1", "Category 3");
            repos.Add(item1);
            repos.Add(item2);
            repos.Add(item3);

            // Act
            IEnumerable<Medicine> foundItems = repos.FindAll(med => med.Type.Equals(item2.Type));

            // Assert
            Assert.AreEqual(2, foundItems.Count());
        }
        [TestMethod]
        public void DeleteFirst_Predicate_ExistingItem_RemovesFirstMatchingItem()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item1 = new MedicineDto(100, "Medicine 110", false, "Type 1", "Category 1");
            MedicineDto item2 = new MedicineDto(200, "Medicine 203", true, "Type 2", "Category 2");
            MedicineDto item3 = new MedicineDto(300, "Medicine 302", true, "Type 3", "Category 3"); 
            repos.Add(item1);
            repos.Add(item2);
            repos.Add(item3);

            // Act
            repos.DeleteFirst(med => med.IsPrescriptionRequired);
            Medicine deletedItem = repos.Find(item2.Id);

            // Assert
            Assert.IsNull(deletedItem);
        }

        [TestMethod]
        public void DeleteFirst_Predicate_NonExistingItem_DoesNotThrowException()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item = new MedicineDto ( 100, "Medicine 111",  false, "Type 1", "Category 1" );
            repos.Add(item);

            // Act & Assert
            repos.DeleteFirst(med => med.Name == "Non-existing");
        }

        [TestMethod]
        public void GetEnumerator_EnumeratesItems()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            MedicineDto item1 = new MedicineDto(100, "Medicine 112", false, "Type 1", "Category 1");
            MedicineDto item2 = new MedicineDto(200, "Medicine 204", true, "Type 2", "Category 2"); repos.Add(item1);
            repos.Add(item2);
            List<Medicine> expectedItems = new List<Medicine> { repos.Find(it => it.Name.Equals(item1.Name))!, repos.Find(it => it.Name.Equals(item2.Name))! };

            // Act
            List<Medicine> enumeratedItems = new List<Medicine>();
            IEnumerator<Medicine> enumerator = repos.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumeratedItems.Add(enumerator.Current);
            }

            // Assert
            Assert.AreEqual(52, enumeratedItems.Count);
        }

        [TestMethod]
        public void Serialize_NotSupportedExceptionThrown()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() => repos.Serialize());
        }

        [TestMethod]
        public void Deserialize_NotSupportedExceptionThrown()
        {
            // Arrange
            string jsonPath = CreateTestJsonDirectory();
            LocalMedicineRepos repos = new LocalMedicineRepos(jsonPath);
            string json = "[{\"Id\":1,\"Name\":\"Medicine 1230\",\"IsPrescriptionRequired\":false,\"Type\":\"Type 1\",\"TypeCategory\":\"Category 1\"}]";

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() => repos.Deserialize(json));
        }
    }
}
