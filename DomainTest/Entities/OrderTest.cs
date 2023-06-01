using Domain.Entities.Users;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Entities
{
    [TestClass]
    public class OrderTest
    {
 

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Client_Set_NonNull_ThrowsInvalidOperationException()
        {
            // Arrange
            Client existingClient = new Client(1, "JohnDoe", "test1213223");
            Order order = new Order(1, existingClient, new Dictionary<Product, uint>(), null, false, 0);
            Client newClient = new Client(2, "JaneSmith", "test1212312");

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => order.Client = newClient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ordered_Get_ReturnsImmutableDictionary()
        {
            // Arrange
            IDictionary<Product, uint> ordered = new Dictionary<Product, uint>
            {
                { new Product(1, "Product 1", "", "", null, 0), 2 },
                { new Product(2, "Product 2", "", "", null, 0), 3 }
            };
            Order order = new Order(1, null, ordered, null, false, 0);

            // Act
            IImmutableDictionary<Product, uint> orderedImmutable = order.Ordered;

            // Assert
            Assert.IsInstanceOfType(orderedImmutable, typeof(IImmutableDictionary<Product, uint>));
        }


     
    }
}
