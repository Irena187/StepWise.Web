using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepWise.Services.Tests
{
    [TestFixture]
    public class BookmarkServiceTests
    {
        private Mock<IBookmarkRepository> bookmarkRepositoryMock;
        private IBookmarkService bookmarkService;

        [SetUp]
        public void Setup()
        {
            this.bookmarkRepositoryMock = new Mock<IBookmarkRepository>(MockBehavior.Strict);
            this.bookmarkService = new BookmarkService(this.bookmarkRepositoryMock.Object);
        }

        [Test]
        public void PassAlways()
        {
            Assert.Pass();
        }

        [Test]
        public async Task AddCareerPathToUserBookmarkAsync_WithExistingDeletedBookmark_ReactivatesBookmark()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            var existingBookmark = new UserCareerPath
            {
                UserId = userId,
                CareerPathId = careerPathId,
                IsDeleted = true,
                IsActive = false
            };

            bookmarkRepositoryMock
                .Setup(r => r.FindUserCareerPathAsync(userId, careerPathId))
                .ReturnsAsync(existingBookmark);

            bookmarkRepositoryMock
                .Setup(r => r.UpdateAsync(It.Is<UserCareerPath>(b => b.IsActive && !b.IsDeleted)))
                .ReturnsAsync(true);

            bookmarkRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddCareerPathToUserBookmarkAsync_WithNoExistingBookmark_AddsNewBookmark()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            bookmarkRepositoryMock
                .Setup(r => r.FindUserCareerPathAsync(userId, careerPathId))
                .ReturnsAsync((UserCareerPath?)null);

            bookmarkRepositoryMock
                .Setup(r => r.AddAsync(It.Is<UserCareerPath>(b =>
                    b.UserId == userId &&
                    b.CareerPathId == careerPathId &&
                    b.IsActive &&
                    !b.IsDeleted)))
                .Returns(Task.CompletedTask);

            bookmarkRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveCareerPathFromUserBookmarkAsync_WithValidBookmark_RemovesSuccessfully()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            var bookmark = new UserCareerPath
            {
                UserId = userId,
                CareerPathId = careerPathId,
                IsDeleted = false,
                IsActive = true
            };

            var bookmarks = new List<UserCareerPath> { bookmark }.AsQueryable();

            bookmarkRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(bookmarks);

            bookmarkRepositoryMock
                .Setup(r => r.UpdateAsync(It.Is<UserCareerPath>(b => b.IsDeleted && !b.IsActive)))
                .ReturnsAsync(true);

            bookmarkRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveCareerPathFromUserBookmarkAsync_WithNoMatchingBookmark_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            bookmarkRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<UserCareerPath>().AsQueryable());

            var result = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            Assert.IsFalse(result);
        }
    }
}
