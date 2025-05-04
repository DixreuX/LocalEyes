using LocalEyesAPI.Filters;
using LocalEyesAPI.Helpers;
using LocalEyesAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using LocalEyesAPI.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using LocalEyes.Shared.Helpers;
using LocalEyes.Shared.Models;

namespace LocalEyesUT
{
    public class TestSuite1
    {
        // These three simple unit test are just to demonstrate the use of the of unit testing

        [Fact]
        public void EncryptionHelper_ShouldEncryptKey()
        {
            var encryptionHelper = new EncryptionHelper("TestApiKey");

            var encryptedKey = encryptionHelper.EncryptKey();

            Assert.False(string.IsNullOrEmpty(encryptedKey));
        }

        [Fact]
        public void TokenGenerator_ShouldInitializeTokenHandler()
        {
            var tokenGenerator = new TokenGenerator(null, null);

            var tokenHandler = tokenGenerator.TokenHandler;

            Assert.NotNull(tokenHandler);
        }

        [Fact]
        public void Report_ShouldHaveDefaultValues()
        {
            var report = new Report();

            Assert.Equal(Guid.Empty, report.Id);
            Assert.Null(report.Comment);
            Assert.Null(report.Latitude);
            Assert.Null(report.Longitude);
            Assert.Equal(default(DateTime), report.CreatedDate);
            Assert.Equal(default(DateTime), report.ModifiedDate);
        }
    }
}