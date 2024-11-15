namespace SAMS.Test.DBTest
{
    public class TestConnection : SAMSContextTest
    {
        [Fact]
        public void TestDatabaseConnection()
        {
            using var context = GetContext();
            Assert.NotNull(context);
            Assert.True(context.Database.CanConnect(), "No se pudo conectar a la base de datos.");
        }
    }
}
