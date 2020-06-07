using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverApiModel;
using System.Web.Http.Results;
using System.Diagnostics;

namespace MarsRoverApi.Controllers.Tests
{
    [TestClass()]
    public class RoverControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            List<HistoryRecord> expectedHistory = new List<HistoryRecord>
            {
                new HistoryRecord
                {
                    Command = "5 5",
                    Input = true
                },
                new HistoryRecord
                {
                    Command = "0 0 -> 5 5",
                    Input = false
                },
                new HistoryRecord
                {
                    Command = "1 2 N",
                    Input = true
                },
                new HistoryRecord
                {
                    Command = "1 2 N",
                    Input = false
                },
                new HistoryRecord
                {
                    Command = "LMLMLMLMM",
                    Input = true
                },
                new HistoryRecord
                {
                    Command = "1 3 N",
                    Input = false
                }, 
                new HistoryRecord
                {
                    Command = "3 3 E",
                    Input = true
                },
                new HistoryRecord
                {
                    Command = "3 3 E",
                    Input = false
                },
                new HistoryRecord
                {
                    Command = "MMRMMRMRRM",
                    Input = true
                },
                new HistoryRecord
                {
                    Command = "5 1 E",
                    Input = false
                }
            };

            // Act
            controller.Post(new CommandBody
            {
                Command = "5 5",
                Type = CommandType.SetupPlateau
            });
            controller.Post(new CommandBody
            {
                Command = "1 2 N",
                Type = CommandType.SetupRover
            });
            controller.Post(new CommandBody
            {
                Command = "LMLMLMLMM",
                Type = CommandType.Move
            });
            controller.Post(new CommandBody
            {
                Command = "3 3 E",
                Type = CommandType.SetupRover
            });
            controller.Post(new CommandBody
            {
                Command = "MMRMMRMRRM",
                Type = CommandType.Move
            });
            var history = controller.Get();

            // Assert
            Assert.AreEqual(expectedHistory.Count, history.Count);
            for (int i = 0; i < history.Count; i++)
            {
                Assert.AreEqual(expectedHistory[i], history[i]);
            }
        }

        [TestMethod()]
        public void GetTestWithId()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            HistoryRecord expectedRecord1 = new HistoryRecord
            {
                Command = "0 0 -> 5 5",
                Input = false
            };
            HistoryRecord expectedRecord2 = new HistoryRecord
            {
                Command = "5 1 E",
                Input = false
            };
            HistoryRecord expectedRecord3 = null;

            // Act
            controller.Post(new CommandBody
            {
                Command = "5 5",
                Type = CommandType.SetupPlateau
            });
            controller.Post(new CommandBody
            {
                Command = "1 2 N",
                Type = CommandType.SetupRover
            });
            controller.Post(new CommandBody
            {
                Command = "LMLMLMLMM",
                Type = CommandType.Move
            });
            controller.Post(new CommandBody
            {
                Command = "3 3 E",
                Type = CommandType.SetupRover
            });
            controller.Post(new CommandBody
            {
                Command = "MMRMMRMRRM",
                Type = CommandType.Move
            });
            var record1 = controller.Get(1);
            var record2 = controller.Get(9);
            var record3 = controller.Get(10);

            // Assert
            Assert.AreEqual(expectedRecord1, record1);
            Assert.AreEqual(expectedRecord2, record2);
            Assert.AreEqual(expectedRecord3, record3);
        }

        [TestMethod()]
        public void PostTestMove()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            string moveResultEx1 = "1 3 N";
            string moveResultEx2 = "5 1 E";

            // Act
            controller.Post(new CommandBody
            {
                Command = "5 5",
                Type = CommandType.SetupPlateau
            });
            controller.Post(new CommandBody
            {
                Command = "1 2 N",
                Type = CommandType.SetupRover
            });
            string moveResult1 = controller.Post(new CommandBody
            {
                Command = "LMLMLMLMM",
                Type = CommandType.Move
            });
            controller.Post(new CommandBody
            {
                Command = "3 3 E",
                Type = CommandType.SetupRover
            });
            string moveResult2 = controller.Post(new CommandBody
            { 
                Command = "MMRMMRMRRM",
                Type = CommandType.Move
            });

            // Assert
            Assert.AreEqual(moveResultEx1, moveResult1);
            Assert.AreEqual(moveResultEx2, moveResult2);
        }

        [TestMethod()]
        public void PostTestSetupPlateau()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            int plateauX = 5;
            int plateauY = 5;
            int plateauX2 = 24;
            int plateauY2 = 13;
            string expected = "0 0 -> 5 5";
            string expected2 = "0 0 -> 24 13";

            // Act
            var result = controller.Post(new CommandBody
            {
                Command = $"{plateauX} {plateauY}",
                Type = CommandType.SetupPlateau
            });
            var result2 = controller.Post(new CommandBody
            {
                Command = $"{plateauX2} {plateauY2}",
                Type = CommandType.SetupPlateau
            });

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod()]
        public void PostTestSetupRover()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            Rover rover1 = new Rover(1, 2, 'N');
            Rover rover2 = new Rover(3, 3, 'E');

            // Act
            var result1 = controller.Post(new CommandBody
            {
                Command = rover1.ToString(),
                Type = CommandType.SetupRover
            });
            var result2 = controller.Post(new CommandBody
            {
                Command = rover2.ToString(),
                Type = CommandType.SetupRover
            });

            // Assert
            Assert.AreEqual(rover1.ToString(), result1);
            Assert.AreEqual(rover2.ToString(), result2);
        }

        [TestMethod()]
        public void PostTestBadCommands()
        {
            // Arrange
            var controller = new RoverController();
            controller.Delete();
            string bad = "test";
            string badExpected = "Bad Command Type: 0";
            string moveExpected = "Malformed move command: test";
            string plateauExpected = "Malformed Plateau command: test";
            string roverExpected = "Malformed Rover command: test";

            // Act
            string badActual = controller.Post(new CommandBody
            {
                Command = bad,
                Type = 0
            });
            string moveActual = controller.Post(new CommandBody
            {
                Command = bad,
                Type = CommandType.Move
            });
            string plateauActual = controller.Post(new CommandBody
            {
                Command = bad,
                Type = CommandType.SetupPlateau
            });
            string roverActual = controller.Post(new CommandBody
            {
                Command = bad,
                Type = CommandType.SetupRover
            });

            // Assert
            Assert.AreEqual(badExpected, badActual);
            Assert.AreEqual(moveExpected, moveActual);
            Assert.AreEqual(plateauExpected, plateauActual);
            Assert.AreEqual(roverExpected, roverActual);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            var controller = new RoverController();
            controller.Post(new CommandBody
            {
                Command = "5 5",
                Type = CommandType.SetupPlateau
            });
            controller.Post(new CommandBody
            {
                Command = "1 2 N",
                Type = CommandType.SetupRover
            });
            controller.Post(new CommandBody
            {
                Command = "LMLMLMLMM",
                Type = CommandType.Move
            });
            var beforeCount = controller.Get().Count;

            // Act
            controller.Delete();
            var afterCount = controller.Get().Count;

            //Assert
            Assert.IsTrue(beforeCount > 0, "Before count should be more than zero");
            Assert.AreEqual(0, afterCount);
        }
    }
}