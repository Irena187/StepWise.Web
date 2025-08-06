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
using StepWise.Data;

namespace StepWise.Services.Tests
{

    namespace StepWise.Tests.Services
    {
        public class CareerPathServiceTests
        {
            private Mock<ICareerPathRepository> _careerPathRepoMock;
            private Mock<IUserCareerPathRepository> _userCareerPathRepoMock;
            private Mock<IUserCareerStepCompletionRepository> _stepCompletionRepoMock;
            private CareerPathService _service;

            [SetUp]
            public void Setup()
            {
                _careerPathRepoMock = new Mock<ICareerPathRepository>();
                _userCareerPathRepoMock = new Mock<IUserCareerPathRepository>();
                _stepCompletionRepoMock = new Mock<IUserCareerStepCompletionRepository>();

                _service = new CareerPathService(
                    _careerPathRepoMock.Object,
                    _userCareerPathRepoMock.Object,
                    _stepCompletionRepoMock.Object
                );
            }

            [Test]
            public async Task CreateCareerPathAsync_ShouldCreatePathWithSteps()
            {
                var inputModel = new AddCareerPathInputModel
                {
                    Title = "Test Path",
                    GoalProfession = "Tester",
                    Description = "Desc",
                    IsPublic = true,
                    Steps = new List<AddCareerStepInputModel>
                    {
                        new AddCareerStepInputModel
                        {
                            Title = "Step 1",
                            Description = "S1",
                            Type = StepType.Course,
                            Url = "http://step1.com",
                            Deadline = DateTime.UtcNow.AddDays(5)
                        }
                    }
                };

                var contextMock = new Mock<StepWiseDbContext>();
                var creators = new List<Creator>();
                var creatorSetMock = creators.BuildMockDbSet();
                contextMock.Setup(c => c.Set<Creator>()).Returns(creatorSetMock.Object);

                _careerPathRepoMock.Setup(r => r.GetDbContext()).Returns(contextMock.Object);

                var result = await _service.CreateCareerPathAsync(inputModel, Guid.NewGuid());

                Assert.IsTrue(result);
                _careerPathRepoMock.Verify(r => r.AddAsync(It.IsAny<CareerPath>()), Times.Once);
            }

            [Test]
            public async Task DeleteCareerPathAsync_ShouldMarkAsDeleted()
            {
                var userId = Guid.NewGuid();
                var careerPath = new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Creator = new Creator { UserId = userId },
                    IsDeleted = false,
                    Steps = new List<CareerStep> { new CareerStep { IsDeleted = false } }
                };

                var careerPaths = new List<CareerPath> { careerPath }.BuildMock();
                _careerPathRepoMock.Setup(r => r.GetAllAttached()).Returns(careerPaths);

                var success = await _service.DeleteCareerPathAsync(careerPath.Id, userId);

                Assert.IsTrue(success);
                Assert.IsTrue(careerPath.IsDeleted);
                Assert.IsTrue(careerPath.Steps.All(s => s.IsDeleted));
            }

            [Test]
            public async Task MarkStepCompletedAsync_ShouldAddCompletion()
            {
                var userId = Guid.NewGuid();
                var stepId = Guid.NewGuid();

                _stepCompletionRepoMock.Setup(r => r.ExistsAsync(userId, stepId))
                    .ReturnsAsync(false);

                await _service.MarkStepCompletedAsync(userId, stepId);

                _stepCompletionRepoMock.Verify(r => r.AddAsync(It.Is<UserCareerStepCompletion>(
                    c => c.UserId == userId && c.CareerStepId == stepId)), Times.Once);
            }

            [Test]
            public async Task GetCompletedStepIdsForUserAsync_ShouldReturnIds()
            {
                var userId = Guid.NewGuid();
                var careerPathId = Guid.NewGuid();
                var expectedIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

                _stepCompletionRepoMock.Setup(r =>
                    r.GetCompletedStepIdsAsync(userId, careerPathId)).ReturnsAsync(expectedIds);

                var result = await _service.GetCompletedStepIdsForUserAsync(userId, careerPathId);

                Assert.AreEqual(expectedIds, result);
            }

            [Test]
            public async Task UpdateCareerPathIsActiveStatusForUserAsync_ShouldUpdateActivity()
            {
                var userId = Guid.NewGuid();
                var pathId = Guid.NewGuid();

                var careerPath = new CareerPath
                {
                    Id = pathId,
                    Steps = new List<CareerStep>
                {
                    new CareerStep { IsDeleted = false },
                    new CareerStep { IsDeleted = false }
                }
                };

                var userPath = new UserCareerPath { CareerPath = careerPath };

                _userCareerPathRepoMock.Setup(r => r.GetActiveByUserIdWithCareerPathStepsAsync(userId))
                    .ReturnsAsync(new List<UserCareerPath> { userPath });

                _stepCompletionRepoMock.Setup(r =>
                    r.CountCompletedStepsAsync(userId, pathId)).ReturnsAsync(1);

                await _service.UpdateCareerPathIsActiveStatusForUserAsync(userId);

                Assert.IsTrue(userPath.IsActive);
                _userCareerPathRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            }
        }
    }
}