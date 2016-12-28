using System;
using NUnit.Framework;


namespace Library
{
    [TestFixture]
    public class ClassificationServiceTest
    {
        private ClassificationService service;

        [SetUp]
        public void Initialize()
        {
            service = new MasterClassificationService();
            service.DeleteAllBooks();
        }

        [Test]
        public void AddBook()
        {
            service.AddBook("QA123", "The Trial", "Kafka, Franz", "1927");
            AssertBook(service.Retrieve("QA123"), "QA123", "The Trial", "Kafka, Franz", "1927");
        }

        [Test]
        public void ReturnsNullWhenBookNotFound()
        {
            Assert.IsNull(service.Retrieve("QA123"));
        }

        [Test]
        public void Persists()
        {
            service.AddBook("QA123", "The Trial", "Kafka, Franz", "1927");

            service = new MasterClassificationService();
            AssertBook(service.Retrieve("QA123"), "QA123", "The Trial", "Kafka, Franz", "1927");
        }

        [Test]
        public void MultipleBooks()
        {
            service.AddBook("QA123", "The Trial", "Kafka, Franz", "1927");
            service.AddBook("PS334", "Agile Java", "Langr, Jeff", "2005");
            AssertBook(service.Retrieve("QA123"), "QA123", "The Trial", "Kafka, Franz", "1927");
            AssertBook(service.Retrieve("PS334"), "PS334", "Agile Java", "Langr, Jeff", "2005");
        }

        [Test]
        public void AddMovie()
        {
            service.AddMovie("FF223", "Fight Club", "Fincher, David", "1999");
            Material material = service.Retrieve("FF223");
            Assert.AreEqual("FF223", material.Classification);
            Assert.AreEqual("Fight Club", material.Title);
            Assert.AreEqual("Fincher, David", material.Director);
            Assert.AreEqual("1999", material.Year);
        }

        private void AssertBook(Material material, string classification, string title, string author, string year)
        {
            Assert.AreEqual(title, material.Title);
            Assert.AreEqual(author, material.Author);
            Assert.AreEqual(year, material.Year);
            Assert.AreEqual(classification, material.Classification);

            Assert.IsTrue(material.CheckoutPolicy.GetType() == typeof(BookCheckoutPolicy));
        }
    }
}
