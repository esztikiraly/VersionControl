using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;
using UnitTestExample;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
    Test,
    TestCase("abcd1234", false),
    TestCase("irf@uni-corvinus", false),
    TestCase("irf.uni-corvinus.hu", false),
    TestCase("irf@uni-corvinus.hu", true)
]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }
        [
            Test,
            TestCase("Password", false),
            TestCase("PASSWORD123", false),
            TestCase("password123", false),
            TestCase("a", false),
            TestCase("Password123", true)
            ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.Register(password,password);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
