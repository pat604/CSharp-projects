using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RuplandTest

// a IGenericRepository osztályt mockoltam, viszont az összetettsége miatt egy külön osztályba 
// helyeztem el, nem volt elég a Setup(). Itt láttam erre példát: https://msdn.microsoft.com/en-us/library/ff650441.aspx 
{
    [TestFixture]
    public class UserManagementTests
    {
        private CurrentProject.Class.UserManagement UM;
        private CurrentProject.App app;

        [Test]
        public void Test_CheckEmail_Should_ReturnTrue_When_CalledWithExistingEmail()
        {
            // Arrange
            MockUserRepository userRepo = new MockUserRepository();     // mindig újat definiálok, hogy a tesztek "adatbázisai" ne keveredjenek
            UM = new CurrentProject.Class.UserManagement(userRepo);

            // Act
            UM.Registration("Peti", "Lilli", "peti@oe.hu", "eperke", 1, "Eper");

            // Assert
            Assert.IsTrue(UM.CheckEmail("peti@oe.hu"));
        }

        [Test]
        public void Test_Registration_Should_ThrowException_CalledWithExistingEmail()
        {
            // a programban a Registration(...) hívásaikor ellenőrizzük a CheckEmail(string email) hívással, 
            // hogy szerepel-e már az adabázisban az email cím, de ha ezt mégsem tesszük, az Entity Framework dobna Exceptiont

            // Arrange
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);
            TestDelegate TD = new TestDelegate(RegistrationVoid);

            // Act
            UM.Registration("Peti", "Lilli", "peti@oe.hu", "eperke", 1, "Eper");

            // Assert
            Assert.Throws<MockEntityException>(TD);          
        }

        private void RegistrationVoid()
        {
            UM.Registration("Peti", "Kovács", "peti@oe.hu", "eperke", 1, "Eper");
        }

        [Test]
        public void Test_Login_Should_Fail_When_CalledWith_NonExtistentUsername()
        {
            // Arrange
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);

            // Assert
            Assert.IsFalse(UM.Login("a@a.hu", "aaa"));
        }

        [Test]
        public void Test_Login_Should_Pass_When_CalledWith_ExtisingDatas()
        {
            // Arrange
            app = new CurrentProject.App();
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);
            
            // Act
            UM.Registration("Peti", "Lilli", "peti@oe.hu", "eperke", 1, "Eper");

            // Assert
            Assert.IsTrue(UM.Login("peti@oe.hu", "eperke"));
        }

        [Test]
        public void Test_PasswordRecoveryCheck_Should_ReturnTrue_CalledWith_ExistingDatas()
        {
            // Arrange
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);

            // Act
            UM.Registration("Peti", "Lilli", "peti@oe.hu", "eperke", 1, "Eper");

            // Assert
            Assert.IsTrue(UM.PasswordRecoveryCheck("peti@oe.hu", 1, "Eper"));
        }

        [Test]
        public void Test_PasswordRecoveryCheck_Should_ReturnFalse_CalledWith_NonExistingDatas()
        {
            // Arrange
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);

            // Assert
            Assert.IsFalse(UM.PasswordRecoveryCheck("dani@oe.hu", 1, "Eper"));
        }

        [Test]
        public void Test_UpdateUserData_Should_UpdateData()
        {
            // Arrange
            MockUserRepository userRepo = new MockUserRepository();
            UM = new CurrentProject.Class.UserManagement(userRepo);

            // Act
            UM.Registration("Peti", "Lilli", "peti@oe.hu", "eperke", 1, "Eper");
            UM.UpdateUserData("Petike", "Lillike", 2, "Alma");
            IQueryable<CurrentProject.Database.User> searchedUser = userRepo.SearchFor(x => x.Email == "peti@oe.hu");
            CurrentProject.Database.User user = searchedUser.ElementAt(0);
            string newFirstName = "Petike";

            // Assert
            Assert.AreEqual(newFirstName, user.Firstname);
        }
    }
}
