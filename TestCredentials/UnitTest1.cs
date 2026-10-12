
namespace poepart2
{
    public class UnitTest1
    {
        public class CategoryControllerTests
        {
            [Fact]
            public void test_CategoryGetAll()
            {
                // instance with object
                CategoryController get_all_categories = new CategoryController();

                // get request method
                string results = get_all_categories.getAllCategories();

                string expected_results = "ID: 1, Name: Electronics, Description: Electronic products\n" +
                                          "ID: 2, Name: Furniture, Description: Furniture products\n";

                Assert.Equal(expected_results, results);
            }


            [Fact]
            public void test_CategoryGetById()
            {
                // instance with object
                CategoryController get_category = new CategoryController();

                // get request method
                string results = get_category.getCategory(1);

                string expected_results =
                    "ID: 1, Name: Electronics, Description: Electronic products";

                Assert.Equal(expected_results, results);
            }


            [Fact]
            public void test_CategoryAdd()
            {
                // instance with object
                CategoryController add_category = new CategoryController();

                // add request method
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
                // instance with object
                CategoryController delete_category = new CategoryController();

                // delete request method
                string results = delete_category.deleteCategory(1);

                string expected_results = "Category deleted successfully.";

                Assert.Equal(expected_results, results);
            }
        }

    }
}