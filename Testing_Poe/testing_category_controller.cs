using poepart2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Poe
{
    public class testing_category_controller
    {
       
// Category controller test cases

[Fact]
public void test_CategoryGetAll()
        {
            CategoryController get_all_categories = new CategoryController();

            string results = get_all_categories.getAllCategories();

            string expected_results =
                "ID: 1003, Name: board games, Description: Two players on the board\n";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryGetById()
        {
            CategoryController get_category = new CategoryController();

            string results = get_category.getCategory(1003);

            string expected_results =
                "ID: 1003, Name: board games, Description: Two players on the board";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryAdd()
        {
            CategoryController add_category = new CategoryController();

            string results = add_category.addCategory(
                "Clothing",
                "Clothing products"
            );

            string expected_results = "Category added successfully.";

            Assert.Equal(expected_results, results);
        }


        [Fact]
        public void test_CategoryDelete()
        {
            CategoryController delete_category = new CategoryController();

            string results = delete_category.deleteCategory(1003);

            string expected_results = "Category deleted successfully.";

            Assert.Equal(expected_results, results);
        }


    }
}
