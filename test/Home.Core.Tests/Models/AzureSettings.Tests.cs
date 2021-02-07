namespace Home.Core.Tests
{
    using System;
    using Xunit;
    using Home.Core.Models.Settings;

    public class AzureSettings_Tests
    {
        [Fact]
        public void TestAuthority()
        {
            var testvalue = Guid.NewGuid().ToString();
            var expected = $"https://login.microsoftonline.com/{testvalue}/";

            var az = new AzureSettings()
            {
                TenantId = testvalue
            };

            Assert.NotNull(az);
            Assert.Equal(testvalue, az.TenantId);
            Assert.Equal(expected, az.Authority);
        }
    }
}
