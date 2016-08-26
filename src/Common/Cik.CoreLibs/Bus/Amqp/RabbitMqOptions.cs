namespace Cik.CoreLibs.Bus.Amqp
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "root";
        public string Password { get; set; } = "root";
    }
}