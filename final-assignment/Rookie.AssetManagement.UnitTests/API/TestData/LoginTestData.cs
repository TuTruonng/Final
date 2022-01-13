using Rookie.AssetManagement.Contracts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.UnitTests.API.TestData
{
    class LoginTestData
    {
        public static IEnumerable<object[]> ValidUsername()
        {
            return new object[][]
            {
                new object[] { "admin123"},
                new object[] { "USER" },
            };
        }

        public static IEnumerable<object[]> ValidPasswords()
        {
            return new object[][]
            {
                new object[] { "678912" },
                new object[] {"user@123"}
            };
        }

        public static IEnumerable<object[]> InvalidUsername()
        {
            return new object[][]
            {
                new object[]
                {
                    "",
                    string.Format(ErrorTypes.Common.RequiredError, "Username")
                },
                new object[]
                {
                    "....",
                    string.Format("Username must contain only numbers and characters")
                },
            };
        }
        public static IEnumerable<object[]> InvalidPassword()
        {
            return new object[][]
            {
                new object[]
                {
                    "",
                    string.Format(ErrorTypes.Common.RequiredError, "Password")
                },
                new object[]
                {
                    "123",
                    string.Format("Password is at least 6 characters")
                },
            };
        }
    }
}
