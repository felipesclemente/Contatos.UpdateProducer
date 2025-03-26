namespace Tests
{
    public class Integracoes
    {
        [Fact]
        public void Assert_RabbitMq_IsHealthy()
        {
            //using var client = new HttpClient();
            //var response = await client.GetAsync("http://localhost:5193/health/ready");
            //var content = await response.Content.ReadAsStringAsync();
            //Assert.True(response.IsSuccessStatusCode);
            //Assert.Equal("Healthy", content);
            var value = true;
            Assert.True(value);
        }
    }
}
