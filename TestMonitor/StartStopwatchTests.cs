using Monitor.MonitorLocalMachineService;
using System.Diagnostics;

namespace TestMonitor.StartStartTimers
{
    public class StartStartTimers
    {
        [Test]
        public void StartTimers_With0Commands_ReturnInvalid()
        {
            //Arrange
            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>();

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            //Assert
            Assert.That(processToMonitorTuple, Is.Empty);
        }

        [Test]
        public void StartTimers_With1Command_ReturnTrue()
        {
            //Arrange
            const string COMMAND_ONE = "notepad 2 1";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            //Assert
            Assert.That(processToMonitorTuple[0].Item2.Elapsed.TotalSeconds > 0, Is.EqualTo(true));
        }

        [Test]
        public void StartTimers_With2Commands_ReturnTrue()
        {
            //Arrange
            const string COMMAND_ONE = "notepad 2 1";
            const string COMMAND_TWO = "wscript 2 1";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_TWO.Split(' '), new Stopwatch())
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(processToMonitorTuple[0].Item2.Elapsed.TotalSeconds > 0, Is.EqualTo(true));
                Assert.That(processToMonitorTuple[1].Item2.Elapsed.TotalSeconds > 0, Is.EqualTo(true));
            });
        }
    }
}