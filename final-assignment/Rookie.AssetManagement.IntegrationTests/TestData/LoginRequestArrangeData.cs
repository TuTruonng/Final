using Rookie.AssetManagement.Business.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.IntegrationTests.TestData
{
    public class LoginRequestArrangeData
    {
        public static LoginRequest ValidAccount()
        {
            return new LoginRequest
            {
                Username = "user1",
                Password = "123456"
            };
        }
        public static LoginRequest InvalidAccount()
        {
            return new LoginRequest
            {
                Username = "user0",
                Password = "0000000"
            };
        }
        public static LoginRequest DisableAccount()
        {
            return new LoginRequest
            {
                Username = "user2",
                Password = "123457"
            };
        }
    }
}
