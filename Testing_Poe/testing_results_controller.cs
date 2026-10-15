using poepart2.Controllers;
using poepart2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Poe
{
    public class testing_results_controller
    {
        
// Results controller test cases

[Fact]
public void test_ResultGetAll()
        {
            ResultController get_all_results = new ResultController();

            string results = get_all_results.getAllResults();

            string expected_results =
                "ID: 1002, Position: 1st, Score: 120\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ResultGetById()
        {
            ResultController get_result = new ResultController();

            string results = get_result.getResult(1002);

            string expected_results =
                "ID: 1002, Position: 1st, Score: 120";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ResultUpdate()
        {
            ResultController update_result = new ResultController();

            ResultsModel updatedResult = new ResultsModel
            {
                Position = "Updated First",
                Score = 95
            };

            string results = update_result.updateResult(1002, updatedResult);

            string expected_results = "Result updated successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_ResultDelete()
        {
            ResultController delete_result = new ResultController();

            string results = delete_result.deleteResult(1002);

            string expected_results = "Result deleted successfully.";

            Assert.Equal(expected_results, results);
        }


    }
}
