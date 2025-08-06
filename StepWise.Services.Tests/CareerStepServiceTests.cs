using Moq;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Tests
{
    public class CareerStepServiceTests
    {
        private Mock<IUserCareerStepCompletionRepository> _stepCompletionRepoMock;
        private CareerStepService _service;

        [SetUp]
        public void Setup()
        {
            _stepCompletionRepoMock = new Mock<IUserCareerStepCompletionRepository>();
            _service = new CareerStepService(_stepCompletionRepoMock.Object);
        }

        [Test]
        public async Task IsStepCompletedAsync_ReturnsTrue_WhenExists()
        {
            var userId = Guid.NewGuid();
            var stepId = Guid.NewGuid();
            _stepCompletionRepoMock.Setup(r => r.ExistsAsync(userId, stepId)).ReturnsAsync(true);

            var result = await _service.IsStepCompletedAsync(userId, stepId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsStepCompletedAsync_ReturnsFalse_WhenNotExists()
        {
            var userId = Guid.NewGuid();
            var stepId = Guid.NewGuid();
            _stepCompletionRepoMock.Setup(r => r.ExistsAsync(userId, stepId)).ReturnsAsync(false);

            var result = await _service.IsStepCompletedAsync(userId, stepId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task MarkStepCompletionAsync_AddsCompletion_WhenNotExists_AndIsComplete()
        {
            var userId = Guid.NewGuid();
            var stepId = Guid.NewGuid();
            _stepCompletionRepoMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<UserCareerStepCompletion, bool>>>()))
                .ReturnsAsync((UserCareerStepCompletion)null);

            await _service.MarkStepCompletionAsync(userId, stepId, true);

            _stepCompletionRepoMock.Verify(r => r.AddAsync(It.Is<UserCareerStepCompletion>(
                c => c.UserId == userId && c.CareerStepId == stepId)), Times.Once);
            _stepCompletionRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task MarkStepCompletionAsync_DeletesCompletion_WhenExists_AndIsNotComplete()
        {
            var userId = Guid.NewGuid();
            var stepId = Guid.NewGuid();
            var completion = new UserCareerStepCompletion { UserId = userId, CareerStepId = stepId };
            _stepCompletionRepoMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<UserCareerStepCompletion, bool>>>()))
                .ReturnsAsync(completion);

            await _service.MarkStepCompletionAsync(userId, stepId, false);

            _stepCompletionRepoMock.Verify(r => r.DeleteAsync(completion), Times.Once);
            _stepCompletionRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetCompletedStepIdsForUserAsync_ReturnsIds()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();
            var expectedIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            _stepCompletionRepoMock.Setup(r => r.GetCompletedStepIdsAsync(userId, careerPathId)).ReturnsAsync(expectedIds);

            var result = await _service.GetCompletedStepIdsForUserAsync(userId, careerPathId);

            Assert.AreEqual(expectedIds, result);
        }
    }
}
