using ConsoleCafe.Domain.Events;
using ConsoleCafe.Infrastructure.Logging;

namespace ConsoleCafe.UnitTests.Infrastructure
{
    public class ConsoleOrderLoggerTests
    {
        [Fact]
        public void On_ShouldWriteLogToConsole()
        {
            var logger = new ConsoleOrderLogger("$");
            var evt = new OrderPlaced(Guid.NewGuid(), DateTimeOffset.Parse("2025-01-01T10:00:00Z"), "Latte", 4.00m, 4.80m);

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            logger.On(evt);

            var output = consoleOutput.ToString();
            Assert.Contains(evt.OrderId.ToString(), output);
            Assert.Contains("Latte", output);
            Assert.Contains("$4.80", output);
        }
    }
}
