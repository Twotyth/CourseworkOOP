using DataLayer.Dtos;
using DataLayer.Repositories;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTest.Repositories
{
    [TestClass]
    public class LocalPrescriptionReposTest
    {
        private const string TestJsonPath = "./TestJson";
        private IRepos<Medicine, MedicineDto> _medicineRepos;
        private LocalPrescriptionRepos _prescriptionRepos;


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
            string reposPath = Path.Combine(basePath, "PrescriptionMedicineProvider\\.repos");
            DirectoryInfo reposDirectory = new DirectoryInfo(reposPath);
            foreach (FileInfo file in reposDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(TestJsonPath, file.Name));
            }
            _medicineRepos = new LocalMedicineRepos(TestJsonPath); // Create an instance of the medicine repository
            _prescriptionRepos = new LocalPrescriptionRepos(_medicineRepos, TestJsonPath); // Create an instance of the prescription repository

        }

        private uint LastId()
        {
            var type = _prescriptionRepos.GetType();
            var lastIdProperty = type.GetProperty("LastId", BindingFlags.NonPublic | BindingFlags.Instance);
            var lastId = (uint)lastIdProperty.GetValue(_prescriptionRepos);
            return lastId;
        }
        [TestMethod]
        public void Add_ValidPrescription_AddsPrescriptionToList()
        {
            // Arrange
            var prescriptionDto = new PrescriptionDto
            (
              1,
                 new List<uint> { 1, 2, 3 }
            );

            // Act
            _prescriptionRepos.Add(prescriptionDto);

            // Assert
            var addedPrescription = _prescriptionRepos.Find(LastId());
            Assert.IsNotNull(addedPrescription);
            CollectionAssert.AreEqual(prescriptionDto.PrescribedMedicineId.ToList(), addedPrescription.Prescribed.Select(m => m.Id).ToList());
        }

        [TestMethod]
        public void Delete_ExistingPrescription_RemovesPrescriptionFromList()
        {
            // Arrange
            var prescriptionDto = new PrescriptionDto
              (
                  1,
                   new List<uint> { 1, 2, 3 }
              );
     
            _prescriptionRepos.Add(prescriptionDto);
            var prescription = _prescriptionRepos.Find(LastId());
            Assert.IsNotNull(prescription);

            // Act
            _prescriptionRepos.Delete(prescription);

            // Assert
            var deletedPrescription = _prescriptionRepos.Find(prescription.Id);
            Assert.IsNull(deletedPrescription);
        }

        [TestMethod]
        public void Find_ExistingPrescription_ReturnsPrescription()
        {
            // Arrange
            var prescriptionDto = new PrescriptionDto
             (
                 1,
                  new List<uint> { 1, 2, 3 }
             );
       
            _prescriptionRepos.Add(prescriptionDto);

            // Act
            var foundPrescription = _prescriptionRepos.Find(LastId());

            // Assert
            Assert.IsNotNull(foundPrescription);
            Assert.AreEqual(LastId(), foundPrescription.Id);
            CollectionAssert.AreEqual(prescriptionDto.PrescribedMedicineId.ToList(), foundPrescription.Prescribed.Select(m => m.Id).ToList());
        }

        [TestMethod]
        public void Find_NonExistingPrescription_ReturnsNull()
        {
            // Act
            var foundPrescription = _prescriptionRepos.Find(1);

            // Assert
            Assert.IsNull(foundPrescription);
        }

        [TestMethod]
        public void Find_Predicate_ExistingPrescription_ReturnsPrescription()
        {
            // Arrange
            var prescriptionDto = new PrescriptionDto
            (
                1,
                 new List<uint> { 1, 2, 3 }
            );
        
            _prescriptionRepos.Add(prescriptionDto);

            // Act
            var foundPrescription = _prescriptionRepos.Find(p => p.Id == LastId());

            // Assert
            Assert.IsNotNull(foundPrescription);
            Assert.AreEqual(LastId(), foundPrescription.Id);
        }

        [TestMethod]
        public void Find_Predicate_NonExistingPrescription_ReturnsNull()
        {
            // Act
            var foundPrescription = _prescriptionRepos.Find(p => p.Id == 1);

            // Assert
            Assert.IsNull(foundPrescription);
        }


        [TestMethod]
        public void DeleteFirst_Predicate_ExistingPrescription_RemovesFirstMatchingPrescription()
        {
            // Arrange
            var prescriptionDto1 = new PrescriptionDto
           (
               1,
                new List<uint> { 1, 2, 3 }
           );
            var prescriptionDto2 = new PrescriptionDto
            (
                2,
                 new List<uint> { 4, 5, 6 }
            );
            _prescriptionRepos.Add(prescriptionDto1);
            _prescriptionRepos.Add(prescriptionDto2);

            // Act
            _prescriptionRepos.DeleteFirst(p => p.Prescribed.Any(m => m.Id == 2));

            // Assert
            var deletedPrescription = _prescriptionRepos.Find(1);
            Assert.IsNull(deletedPrescription);
        }

        [TestMethod]
        public void GetEnumerator_EnumeratesPrescriptions()
        {
            // Arrange
            var prescriptionDto1 = new PrescriptionDto
            (
                1,
                 new List<uint> { 1, 2, 3 }
            );
            var prescriptionDto2 = new PrescriptionDto
            (
                2,
                 new List<uint> { 4, 5, 6 }
            );
            _prescriptionRepos.Add(prescriptionDto1);
            _prescriptionRepos.Add(prescriptionDto2);
            var expectedPrescriptions = new List<Prescription> { _prescriptionRepos.Find(1)!, _prescriptionRepos.Find(2)! };

            // Act
            var enumeratedPrescriptions = new List<Prescription>();
            foreach (var prescription in _prescriptionRepos)
            {
                enumeratedPrescriptions.Add(prescription);
            }

            // Assert
            Assert.AreEqual(expectedPrescriptions.Count, enumeratedPrescriptions.Count);
        }
    }
}