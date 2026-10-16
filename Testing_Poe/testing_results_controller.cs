
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

            Assert.Equal("Result ID:2004 Score:85 Position:4th", results);
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
                Score = 999,
                participantId = 3003
            };

            string results = controller.addResult(newResult);

            Assert.Equal("Result added successfully.", results);
        }

        // Test 5: Update an existing result
        [Fact]
        public void test_ResultUpdate()
        {
            ResultController controller = new ResultController();

           ResultsModel updatedResult = new ResultsModel
            {
                Position = "New:3rd",
                Score = 95,
                participantId = 3003
           };

            string updateResult = controller.updateResult(2008, updatedResult);


            Assert.Equal("Result updated successfully.", updateResult);
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

