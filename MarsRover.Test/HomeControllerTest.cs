using MarsRover.Application.Common.Contracts;
using MarsRover.Application.Common.ViewModel;
using MarsRover.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRover.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var mockRepo = new Mock<IPlateauService>();
            var mockRepoRover = new Mock<IMarsRoverService>();
            mockRepo.Setup(repo => repo.CheckIfExists(new CancellationToken()))
                .Returns(Task.FromResult(true));

            var plateauId = Guid.NewGuid();
            var obj = new GetPlateauVM
            {
                Id = plateauId,
                Width = 5,
                Height = 5,
                rovers = new List<GetRoversVM>
                { new GetRoversVM { Id = Guid.NewGuid(), PlateauId = plateauId, x = 2, y = 2, Name = "test", CardinalPoint = Domain.Constants.CardinalPoint.N } }
            };
            mockRepo.Setup(repo => repo.GetPlateau(plateauId, new CancellationToken()))
                .Returns(Task.FromResult(obj));

            var controller = new HomeController(mockRepo.Object, mockRepoRover.Object);

            // Act
            var result = controller.Index(plateauId).Result;

            // Assert
            var data = result as ViewResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (GetPlateauVM)data.Model;
            Assert.AreEqual(1, model.rovers.Count());
        }
    }
}
