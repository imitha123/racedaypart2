using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testingPOE.Controllers;

namespace Testing_Poe
{
    public class testing_organizer_controller
    {
        
// Organizer controller test cases

// Test getting all organizers
[Fact]
public void test_OrganizerGetAll()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.getOrganizer();

            string expected_results =
                "ID: 2003, Name: Imitha, Email: imitha@gmail.com\n";

            Assert.Equal(expected_results, results);
        }


        // Test adding an organizer with valid email and password
        [Fact]
        public void test_OrganizerAdd()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David",
                "david@gmail.com",
                "Password123!",
                "Brown");

            string expected_results = "Organizer added successfully.";

            Assert.Equal(expected_results, results);
        }


        // Test getting an organizer using email and password
        [Fact]
        public void test_OrganizerGetByEmailAndPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "imitha@gmail.com",
                "Imitha123@18");

            string expected_results =
                "Organizer found: ID: 2003, Name: Imitha, Email: imitha@gmail.com";

            Assert.Equal(expected_results, results);
        }


        // Test rejecting an invalid email when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidEmail()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David Brown",
                "david@yahoo.com",
                "Password123!",
                "Brown");

            string expected_results =
                "Invalid email. Email must end with @gmail.com.";

            Assert.Equal(expected_results, results);
        }


        // Test rejecting an invalid password when adding an organizer
        [Fact]
        public void test_OrganizerAdd_InvalidPassword()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.AddOrganizer(
                "David Brown",
                "david@gmail.com",
                "password",
                "Brown");

            string expected_results =
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";

            Assert.Equal(expected_results, results);
        }


        // Test getting an organizer with incorrect credentials
        [Fact]
        public void test_OrganizerGetByIncorrectCredentials()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.GetOrganizerByEmailAndPassword(
                "imitha@gmail.com",
                "WrongPassword123!");

            string expected_results = "Invalid email or password.";

            Assert.Equal(expected_results, results);
        }


        // Test deleting an organizer
        [Fact]
        public void test_OrganizerDelete()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.DeleteOrganizer(2003);

            string expected_results = "Organizer deleted successfully.";

            Assert.Equal(expected_results, results);
        }


        // Test updating an organizer
        [Fact]
        public void test_OrganizerUpdate()
        {
            OrganizerController controller = new OrganizerController();

            string results = controller.UpdateOrganizer(
                2003,
                "Updated Organizer",
                "updated@gmail.com",
                "UpdatedPass123!");

            string expected_results = "Organizer updated successfully.";

            Assert.Equal(expected_results, results);
        }


    }
}
