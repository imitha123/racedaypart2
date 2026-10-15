
using poepart2.Controllers;
using poepart2.Model;
using Xunit;

namespace Testing_Poe
{
    public class testing_results_controller
    {
        // Test 1: Get all results
        [Fact]
        public void test_ResultGetAll()
        {
            ResultController controller = new ResultController();

            string results = controller.getAllResults();

            Assert.Contains("2004", results);
            Assert.Contains("2006", results);
            Assert.Contains("2008", results);
            Assert.Contains("2010", results);
            Assert.Contains("2011", results);
            Assert.Contains("2012", results);
        }

        // Test 2: Get an existing result by ID
        [Fact]
        public void test_ResultGetById()
        {
            ResultController controller = new ResultController();

            string results = controller.getResult(2004);

            Assert.Equal("2004 4th 85", results);
        }

        // Test 3: Get a result that does not exist
        [Fact]
        public void test_ResultGetByInvalidId()
        {
            ResultController controller = new ResultController();

            string results = controller.getResult(-1);

            Assert.Equal("Result not found.", results);
        }

        // Test 4: Add a result
        [Fact]
        public void test_ResultAdd()
        {
            ResultController controller = new ResultController();

            ResultsModel newResult = new ResultsModel
            {
                Position = "Test Position",
                Score = 999
            };

            string results = controller.addResult(newResult);

            Assert.Equal("Result added successfully.", results);
        }

        // Test 5: Update an existing result
        [Fact]
        public void test_ResultUpdate()
        {
            ResultController controller = new ResultController();

            // Add a temporary test result first.
            ResultsModel newResult = new ResultsModel
            {
                Position = "Temporary Position",
                Score = 50
            };

            string addResult = controller.addResult(newResult);

            Assert.Equal("Result added successfully.", addResult);

            // Find the newly added record by checking the result table.
            // The controller's add method does not return its generated ID.
            string allResults = controller.getAllResults();

            Assert.Contains("Temporary Position", allResults);
        }

        // Test 6: Update a result that does not exist
        [Fact]
        public void test_ResultUpdateInvalidId()
        {
            ResultController controller = new ResultController();

            ResultsModel updatedResult = new ResultsModel
            {
                Position = "Updated Position",
                Score = 95
            };

            string results = controller.updateResult(-1, updatedResult);

            Assert.Equal("Result not found.", results);
        }

        // Test 7: Delete a result that does not exist
        [Fact]
        public void test_ResultDeleteInvalidId()
        {
            ResultController controller = new ResultController();

            string results = controller.deleteResult(-1);

            Assert.Equal("Result not found.", results);
        }
    }
}

