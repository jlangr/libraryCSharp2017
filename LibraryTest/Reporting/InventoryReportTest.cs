using NUnit.Framework;
using Library.Domain;

namespace LibraryTest.Reporting
{
    [TestFixture]
    public class InventoryReportTest
    {
        private BranchService branchService;
        private IClassificationService classificationService;
        private HoldingService holdingService;

        [SetUp]
        public void AddData()
        {
            branchService = new BranchService();
            var east = branchService.Add("East");
            var west = branchService.Add("West");

            classificationService = new MasterClassificationService();
            classificationService.DeleteAllBooks();
            classificationService.AddBook("1", "Trial, The", "Kafka, Franz", "1925");
            classificationService.AddBook("2", "Castle, The", "Kafka, Frank", "1926");
            classificationService.AddBook("3", "Catch-22", "Heller, Joseph", "1961");
            classificationService.AddBook("4", "Pale Fire", "Nabokov, Vladimir", "1962");
            classificationService.AddBook("5", "Lolita", "Nabokov, Vladimir", "1955");

            holdingService = new HoldingService();
            holdingService.DeleteAllHoldings();
            holdingService.ClassificationService = classificationService;
            holdingService.Add("1", east);
            holdingService.Add("2", west);
            holdingService.Add("3", east);
            holdingService.Add("4", east);
            holdingService.Add("5", west);
        }

        [Test]
        public void Generate()
        {
            AddData();
            /* TODO this throws an exception!
    InventoryReport report = new InventoryReport(holdingService, classificationService, branchService);
    Console.WriteLine(report.AllBooks());
             */
        }
    }
}
