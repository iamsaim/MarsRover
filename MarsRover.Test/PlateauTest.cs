using MarsRover.Application.Features.Plateau.Command;
using MarsRover.Persistance;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Threading;
using System.Threading.Tasks;
using MarsRover.Domain.Entities;
using MarsRover.Application.Common.ViewModel;


namespace MarsRover.Test
{
    [TestClass]
    public class PlateauTest
    {
        [TestMethod]
        public void InitiatePlateauHandler_Test()
        {
            // Arrange
            var _dataContext = new Mock<ApplicationDbContext>();
            _dataContext.Setup(m => m.AddAsync(It.IsAny<PlateauEntity>(), default));

            _dataContext.Setup(c => c.SaveChangesAsync(default))
                        .Returns(Task.FromResult(1))
                        .Verifiable();


            var mockLogger = new Mock<ILogger<InitiatePlateauHandler>>();

            var command = new InitiatePlateauHandler(_dataContext.Object, mockLogger.Object);

            // Act
            var result = command.Handle(new InitiatePlateauCommand
            {
                data = new InitiatePlateauVM
                {
                    Width = 5,
                    Height = 5

                }
            }, new CancellationToken());

            // Assert            
            Assert.AreNotEqual(result, Guid.Empty);
        }

    }
}