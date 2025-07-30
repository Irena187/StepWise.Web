using Moq;
using NUnit.Framework;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using MockQueryable;

namespace StepWise.Services.Tests
{
    [TestFixture]
    public class CareerPathServiceTests
    {
        private Mock<ICareerPathRepository> careerPathRepositoryMock;
        private Mock<IUserCareerPathRepository> userCareerPathRepositoryMock;
        private Mock<IUserCareerStepCompletionRepository> stepCompletionRepositoryMock;

        private CareerPathService careerPathService;

        [SetUp]
        public void Setup()
        {
            careerPathRepositoryMock = new Mock<ICareerPathRepository>(MockBehavior.Loose);
            userCareerPathRepositoryMock = new Mock<IUserCareerPathRepository>(MockBehavior.Loose);
            stepCompletionRepositoryMock = new Mock<IUserCareerStepCompletionRepository>(MockBehavior.Loose);

            careerPathService = new CareerPathService(
                careerPathRepositoryMock.Object,
                userCareerPathRepositoryMock.Object,
                stepCompletionRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllCareerPathsAsync_ReturnsNonDeletedCareerPaths()
        {
            var careerPaths = new List<CareerPath>
            {
                new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Path",
                    Description = "Desc",
                    GoalProfession = "Engineer",
                    IsDeleted = false,
                    IsPublic = true,
                    Creator = new Creator
                    {
                        User = new StepWise.Data.Models.ApplicationUser { UserName = "User1" }
                    },
                    Steps = new List<CareerStep>
                    {
                        new CareerStep { IsDeleted = false },
                        new CareerStep { IsDeleted = true } // Should not count
                    }
                },
                new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = "Deleted Path",
                    IsDeleted = true 
                }
            };

            var mockDbSet = careerPaths.BuildMock();

            careerPathRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockDbSet);

            var result = await careerPathService.GetAllCareerPathsAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
            var first = result.First();
            Assert.That(first.Title, Is.EqualTo("Test Path"));
            Assert.That(first.StepsCount, Is.EqualTo(1)); 
        }

        [Test]
        public async Task GetCareerPathByIdAsync_ReturnsCorrectCareerPath()
        {
            // Arrange
            var id = Guid.NewGuid();
            var careerPathList = new List<CareerPath>
            {
                new CareerPath
                {
                    Id = id,
                    Title = "Test",
                    Description = "Desc",
                    GoalProfession = "Dev",
                    IsDeleted = false,
                    Creator = new Creator
                    {
                        User = new StepWise.Data.Models.ApplicationUser { UserName = "User1" }
                    },
                    Steps = new List<CareerStep>
                    {
                        new CareerStep
                        {
                            Id = Guid.NewGuid(),
                            Title = "Step 1",
                            Description = "Step Desc",
                            Type = StepType.Course,
                            Url = "http://example.com",
                            Deadline = null,
                            IsCompleted = false,
                            IsDeleted = false
                        }
                    }
                }
            };

            var mockDbSet = careerPathList.BuildMock();

            careerPathRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockDbSet);

            var result = await careerPathService.GetCareerPathByIdAsync(id);

            Assert.NotNull(result);
            Assert.AreEqual("Test", result.Title);
            Assert.AreEqual(1, result.Steps.Count);
        }

    }
}
 