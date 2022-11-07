using Monitor.MonitorLocalMachineService;
using System.Diagnostics;

namespace TestMonitor.MonitorProgramsTests
{
    public class MonitorProgramsExpectedKilllogTests
    {
        [Test]
        public void MonitorPrograms_With3processToMonitor_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";
            const string PROCESS_TWO = "wscript.exe";
            const string PROCESS_THREE = "mspaint.exe";

            const string COMMAND_ONE = "notepad 2 1";
            const string COMMAND_TWO = "wscript 2 1";
            const string COMMAND_THREE = "mspaint 2 1";

            const string EXPECTED_LOGL_ONE = "Process notepad did not exceed threshold";
            const string EXPECTED_LOGL_TWO = "Process wscript did not exceed threshold";
            const string EXPECTED_LOGL_THREE = "Process mspaint did not exceed threshold";
            const string EXPECTED_LOGL_FOUR = "Process notepad killed";
            const string EXPECTED_LOGL_FIVE = "Process wscript killed";
            const string EXPECTED_LOGL_SIX = "Process mspaint killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_TWO.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_THREE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);
            Process.Start(PROCESS_TWO);
            Process.Start(PROCESS_THREE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE,
                EXPECTED_LOGL_TWO,
                EXPECTED_LOGL_THREE,
                EXPECTED_LOGL_FOUR,
                EXPECTED_LOGL_FIVE,
                EXPECTED_LOGL_SIX
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With2processToMonitorAfterKillingProcces_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";
            const string PROCESS_TWO = "wscript.exe";
            const string PROCESS_THREE = "mspaint.exe";

            const string COMMAND_ONE = "notepad 1 1";
            const string COMMAND_TWO = "wscript 2 1";
            const string COMMAND_THREE = "mspaint 2 1";

            const string EXPECTED_LOGL_ONE = "Process notepad killed";
            const string EXPECTED_LOGL_TWO = "Process wscript did not exceed threshold";
            const string EXPECTED_LOGL_THREE = "Process mspaint did not exceed threshold";
            const string EXPECTED_LOGL_FOUR = "Process wscript killed";
            const string EXPECTED_LOGL_FIVE = "Process mspaint killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_TWO.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_THREE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);
            Process.Start(PROCESS_TWO);
            Process.Start(PROCESS_THREE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE,
                EXPECTED_LOGL_TWO,
                EXPECTED_LOGL_THREE,
                EXPECTED_LOGL_FOUR,
                EXPECTED_LOGL_FIVE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With2equalProcessToMonitor_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 2 1";
            const string COMMAND_TWO = "notepad 2 1";

            const string EXPECTED_LOGL_ONE = "Process notepad did not exceed threshold";
            const string EXPECTED_LOGL_TWO = "Process notepad did not exceed threshold";
            const string EXPECTED_LOGL_THREE = "Process notepad killed";
            const string EXPECTED_LOGL_FOUR = "Process notepad killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_TWO.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE,
                EXPECTED_LOGL_TWO,
                EXPECTED_LOGL_THREE,
                EXPECTED_LOGL_FOUR
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With1processToMonitor_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 2 1";

            const string EXPECTED_LOGL_ONE = "Process notepad did not exceed threshold";
            const string EXPECTED_LOGL_TWO = "Process notepad killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE,
                EXPECTED_LOGL_TWO
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With2processToMonitor_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";
            const string PROCESS_TWO = "wscript.exe";

            const string COMMAND_ONE = "notepad 2 1";
            const string COMMAND_TWO = "wscript 2 1";

            const string EXPECTED_LOGL_ONE = "Process notepad did not exceed threshold";
            const string EXPECTED_LOGL_TWO = "Process wscript did not exceed threshold";
            const string EXPECTED_LOGL_THREE = "Process notepad killed";
            const string EXPECTED_LOGL_FOUR = "Process wscript killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch()),
                new Tuple<string[], Stopwatch>(COMMAND_TWO.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);
            Process.Start(PROCESS_TWO);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE,
                EXPECTED_LOGL_TWO,
                EXPECTED_LOGL_THREE,
                EXPECTED_LOGL_FOUR
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With0RunningProcesses_ReturnExpectedKillLog()
        {
            //Arrange
            const string COMMAND_ONE = "notepad 0 0";

            const string EXPECTED_LOGL_ONE = "Process notepad is not running";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With0ValuedFrequency_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 1 0";

            const string EXPECTED_LOGL_ONE = "Process notepad killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With0ValuedLifetime_ReturnExpectedKillLog()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 0 1";

            const string EXPECTED_LOGL_ONE = "Process notepad killed";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }


    }

    public class MonitorProgramsInvalidFormatTests
    {
        [Test]
        public void MonitorPrograms_With0NumericValues_ReturnInvalidFormat()
        {
            //Arrange
            const string COMMAND_ONE = "a a a";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }



        [Test]
        public void MonitorPrograms_WithNonAlphanumericArgumets_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "!@#$ -1 -1";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_WithNegativeValuedArgumets_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad -2 -1";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With0Argumets_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With2Argumets_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 2";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_With4Argumets_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 2 1 1";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }

        [Test]
        public void MonitorPrograms_WithIncompleteValues_ReturnInvalidFormat()
        {
            //Arrange
            const string PROCESS_ONE = "notepad.exe";

            const string COMMAND_ONE = "notepad 1 2.1";

            const string EXPECTED_LOGL_ONE = "Invalid Command Format";

            var processToMonitorTuple = new List<Tuple<string[], Stopwatch>>()
            {
                new Tuple<string[], Stopwatch>(COMMAND_ONE.Split(' '), new Stopwatch())
            };

            Process.Start(PROCESS_ONE);

            //Expected
            List<string> expectedKillLog = new()
            {
                EXPECTED_LOGL_ONE
            };

            //Act
            MonitorLocalMachineService.StartTimers(processToMonitorTuple);

            List<string> killLogAsync = new();
            while (processToMonitorTuple.Count > 0)
            {
                Thread.Sleep(1000);
                killLogAsync.AddRange(MonitorLocalMachineService.MonitorWindowsProcessesAsync(processToMonitorTuple));
            }

            //Assert
            Assert.That(killLogAsync, Is.EqualTo(expectedKillLog));
        }
    }
}
    