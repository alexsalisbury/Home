namespace Home.Core.Tests
{
    using Xunit;
    using Home.Core.Tests.Generators;

    public class AzureSettings_Tests
    {
        [Fact]
        public void TestAzureSettings()
        {
            var az = ModelGenerator.GenerateAzureSettings();
            Assert.NotNull(az);
        }

        [Fact]
        public void TestAuthority()
        {
            var az = ModelGenerator.GenerateAzureSettings();

            var testvalue = az.TenantId;
            var expected = $"https://login.microsoftonline.com/{testvalue}/";

            Assert.Equal(testvalue, az.TenantId);
            Assert.Equal(expected, az.Authority);
        }
    }
}
