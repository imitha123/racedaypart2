using poepart2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Poe
{
    public class testing_participant_controller
    {
 
// Test getting a participant by email
[Fact]
public void test_ParticipantGetByEmail()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.GetParticipant("Abdi@gmail.com");

            string expected_results =
                "Participant found: Osman Abdi, Email: Abdi@gmail.com";

            Assert.Equal(expected_results, results);
        }

        // Test adding a participant with valid email and password
        [Fact]
        public void test_ParticipantAdd()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@gmail.com",
                "Password123!");

            string expected_results =
                "Participant added successfully!";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid email
        [Fact]
        public void test_ParticipantAdd_InvalidEmail()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@yahoo.com",
                "Password123!");

            string expected_results =
                "Invalid email. Email must end with @gmail.com.";

            Assert.Equal(expected_results, results);
        }

        // Test rejecting an invalid password
        [Fact]
        public void test_ParticipantAdd_InvalidPassword()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.AddParticipant(
                "David",
                "Brown",
                "david@gmail.com",
                "password");

            string expected_results =
                "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";

            Assert.Equal(expected_results, results);
        }

        // Test getting a participant that does not exist
        [Fact]
        public void test_ParticipantGet_NotFound()
        {
            ParticipantController controller = new ParticipantController();

            string results =
                controller.GetParticipant("nobody123@gmail.com");

            string expected_results = "Participant not found.";

            Assert.Equal(expected_results, results);
        }

        // Test updating the existing participant
        [Fact]
        public void test_ParticipantUpdate()
        {
            ParticipantController controller = new ParticipantController();

            var updatedParticipant = new poepart2.Model.ParticipantModel
            {
                Participantname = "Updated",
                Participantsurname = "Participant",
                participantEmail = "updated@gmail.com",
                participantPassword = "UpdatedPass123!"
            };

            string results = controller.UpdateParticipant(
                2002,
                updatedParticipant);

            string expected_results =
                "Participant updated successfully!";

            Assert.Equal(expected_results, results);
        }

        // Test deleting the existing participant
        [Fact]
        public void test_ParticipantDelete()
        {
            ParticipantController controller = new ParticipantController();

            string results = controller.DeleteParticipant(2002);

            string expected_results =
                "Participant deleted successfully!";

            Assert.Equal(expected_results, results);
        }


    }
}
