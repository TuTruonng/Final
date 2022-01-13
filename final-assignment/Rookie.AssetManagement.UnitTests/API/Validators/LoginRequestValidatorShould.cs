using FluentAssertions;
using FluentValidation.Results;
using Moq;
using Rookie.AssetManagement.Business.Request;
using Rookie.AssetManagement.Tests.Validations;
using Rookie.AssetManagement.UnitTests.API.TestData;
using Rookie.AssetManagement.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rookie.AssetManagement.UnitTests.API.Validators
{
    public class LoginRequestValidatorShould : BaseValidatorShould
    {
        private readonly ValidationTestRunner<LoginRequestValidator, LoginRequest> _testRunner;

        public LoginRequestValidatorShould()
        {
            _testRunner = ValidationTestRunner
                .Create<LoginRequestValidator, LoginRequest>(new LoginRequestValidator());
        }

        [Theory]
        [MemberData(nameof(LoginTestData.ValidUsername), MemberType = typeof(LoginTestData))]
        public void NotHaveErrorWhenNameIsValid(string name) =>
            _testRunner
                .For(m => m.Username = name)
                .ShouldNotHaveErrorsFor(m => m.Username);

        [Theory]
        [MemberData(nameof(LoginTestData.ValidPasswords), MemberType = typeof(LoginTestData))]
        public void NotHaveErrorWhenPasswordIsValid(string password) =>
            _testRunner
                .For(m => m.Password = password)
                .ShouldNotHaveErrorsFor(m => m.Password);

        [Theory]
        [MemberData(nameof(LoginTestData.InvalidUsername), MemberType = typeof(LoginTestData))]
        public void HaveErrorWhenUsernameIsInvalid(string name, string errorMessage) =>
            _testRunner
                .For(m => m.Username = name)
                .ShouldHaveErrorsFor(m => m.Username, errorMessage);
        [Theory]
        [MemberData(nameof(LoginTestData.InvalidPassword), MemberType = typeof(LoginTestData))]
        public void HaveErrorWhenPasswordIsInvalid(string password, string errorMessage) =>
            _testRunner
                .For(m => m.Password = password)
                .ShouldHaveErrorsFor(m => m.Password, errorMessage);
    }
}
