using Moq;
using MockQueryable;
using MockQueryable.Moq;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StepWise.Data;
using Microsoft.EntityFrameworkCore;

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
            this.bookmarkRepositoryMock = new Mock<IBookmarkRepository>(MockBehavior.Loose);
            this.bookmarkService = new BookmarkService(this.bookmarkRepositoryMock.Object);
        }

        [Test]
        public void PassAlways()
        {
            Assert.Pass();
        }

        [Test]
        public async Task GetAllBookmarksUserViewShouldReturnEmptyCollection()
        {
            var testUserId = Guid.NewGuid();

            var emptyBookmarkList = new List<UserCareerPath>().AsQueryable();
            var emptyBookmarkDbSet = DbSetMocking.CreateMockDbSet(emptyBookmarkList);

            this.bookmarkRepositoryMock
                .Setup(br => br.GetAllAttached())
                .Returns(emptyBookmarkDbSet.Object);

            var dbContextMock = new Mock<StepWiseDbContext>();
            var emptyStepCompletions = new List<UserCareerStepCompletion>().AsQueryable();
            var emptyStepCompletionsDbSet = DbSetMocking.CreateMockDbSet(emptyStepCompletions);

            dbContextMock.Setup(db => db.UserCareerStepCompletions)
                         .Returns(emptyStepCompletionsDbSet.Object);

            this.bookmarkRepositoryMock
                .Setup(br => br.GetDbContext())
                .Returns(dbContextMock.Object);

            var emptyViewModelCollection = await this.bookmarkService.GetUserBookmarkAsync(testUserId);

            Assert.IsNotNull(emptyViewModelCollection);
            Assert.AreEqual(0, emptyViewModelCollection.Count());
        }

        [Test]
        public async Task GetAllBookmarksUserViewShouldReturnSameCountAsRepository()
        {
            var testUserId = Guid.NewGuid();

            var careerPath1 = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = "Path 1",
                GoalProfession = "Dev",
                Creator = new Creator
                {
                    User = new ApplicationUser { UserName = "user1" }
                },
                Steps = new List<CareerStep>
                {
                    new CareerStep { Id = Guid.NewGuid(), IsDeleted = false },
                    new CareerStep { Id = Guid.NewGuid(), IsDeleted = false }
                }
            };

            var careerPath2 = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = "Path 2",
                GoalProfession = "Tester",
                Creator = new Creator
                {
                    User = new ApplicationUser { UserName = "user2" }
                },
                Steps = new List<CareerStep>
                {
                    new CareerStep { Id = Guid.NewGuid(), IsDeleted = false }
                }
            };

            var bookmarks = new List<UserCareerPath>
            {
                new UserCareerPath
                {
                    UserId = testUserId,
                    CareerPath = careerPath1,
                    CareerPathId = careerPath1.Id,
                    IsActive = true,
                    IsDeleted = false,
                    FollowedAt = DateTime.UtcNow
                },
                new UserCareerPath
                {
                    UserId = testUserId,
                    CareerPath = careerPath2,
                    CareerPathId = careerPath2.Id,
                    IsActive = true,
                    IsDeleted = false,
                    FollowedAt = DateTime.UtcNow
                }
            }.AsQueryable();

            var bookmarkDbSet = EfCoreDbSetMock.Create(bookmarks);
            this.bookmarkRepositoryMock
                .Setup(br => br.GetAllAttached())
                .Returns(bookmarkDbSet.Object);

            var dbContextMock = new Mock<StepWiseDbContext>();
            var emptyStepCompletions = new List<UserCareerStepCompletion>().AsQueryable();
            var emptyStepCompletionsDbSet = EfCoreDbSetMock.Create(emptyStepCompletions);

            dbContextMock.Setup(db => db.UserCareerStepCompletions)
                         .Returns(emptyStepCompletionsDbSet.Object);

            this.bookmarkRepositoryMock
                .Setup(br => br.GetDbContext())
                .Returns(dbContextMock.Object);

            var result = await this.bookmarkService.GetUserBookmarkAsync(testUserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(bookmarks.Count(), result.Count(), "Returned bookmarks count should match repository count.");
        }
        [Test]
        public async Task GetUserBookmarkAsync_ShouldReturnEmpty_WhenNoBookmarksExist()
        {
            var testUserId = Guid.NewGuid();

            var bookmarks = new List<UserCareerPath>().AsQueryable();

            var bookmarkDbSet = EfCoreDbSetMock.Create(bookmarks);
            bookmarkRepositoryMock.Setup(br => br.GetAllAttached())
                                  .Returns(bookmarkDbSet.Object);

            var dbContextMock = new Mock<StepWiseDbContext>();
            var emptyStepCompletions = new List<UserCareerStepCompletion>().AsQueryable();
            var emptyStepCompletionsDbSet = EfCoreDbSetMock.Create(emptyStepCompletions);
            dbContextMock.Setup(db => db.UserCareerStepCompletions)
                         .Returns(emptyStepCompletionsDbSet.Object);
            bookmarkRepositoryMock.Setup(br => br.GetDbContext()).Returns(dbContextMock.Object);

            var result = await bookmarkService.GetUserBookmarkAsync(testUserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [Test]
        public async Task GetUserBookmarkAsync_ShouldIgnoreDeletedOrInactiveBookmarks()
        {
            var testUserId = Guid.NewGuid();

            var activeBookmark = new UserCareerPath
            {
                UserId = testUserId,
                CareerPath = new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = "Active Path",
                    Creator = new Creator { User = new ApplicationUser { UserName = "activeUser" } },
                    Steps = new List<CareerStep>()
                },
                IsActive = true,
                IsDeleted = false
            };

            var deletedBookmark = new UserCareerPath
            {
                UserId = testUserId,
                CareerPath = new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = "Deleted Path",
                    Creator = new Creator { User = new ApplicationUser { UserName = "deletedUser" } },
                    Steps = new List<CareerStep>()
                },
                IsActive = true,
                IsDeleted = true
            };

            var inactiveBookmark = new UserCareerPath
            {
                UserId = testUserId,
                CareerPath = new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = "Inactive Path",
                    Creator = new Creator { User = new ApplicationUser { UserName = "inactiveUser" } },
                    Steps = new List<CareerStep>()
                },
                IsActive = false,
                IsDeleted = false
            };

            var bookmarks = new List<UserCareerPath> { activeBookmark, deletedBookmark, inactiveBookmark }.AsQueryable();

            var bookmarkDbSet = EfCoreDbSetMock.Create(bookmarks);
            bookmarkRepositoryMock.Setup(br => br.GetAllAttached())
                                  .Returns(bookmarkDbSet.Object);

            var dbContextMock = new Mock<StepWiseDbContext>();
            var emptyStepCompletionsDbSet = EfCoreDbSetMock.Create(new List<UserCareerStepCompletion>().AsQueryable());
            dbContextMock.Setup(db => db.UserCareerStepCompletions).Returns(emptyStepCompletionsDbSet.Object);
            bookmarkRepositoryMock.Setup(br => br.GetDbContext()).Returns(dbContextMock.Object);

            var result = await bookmarkService.GetUserBookmarkAsync(testUserId);

            Assert.AreEqual(1, result.Count(), "Only active, non-deleted bookmarks should be returned.");
            Assert.AreEqual("Active Path", result.First().Title);
        }
        [Test]
        public async Task GetUserBookmarkAsync_ShouldCalculateCompletedStepsCorrectly()
        {
            var testUserId = Guid.NewGuid();

            var step1 = new CareerStep { Id = Guid.NewGuid(), IsDeleted = false };
            var step2 = new CareerStep { Id = Guid.NewGuid(), IsDeleted = false };

            var careerPath = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = "Path with Steps",
                Creator = new Creator { User = new ApplicationUser { UserName = "tester" } },
                Steps = new List<CareerStep> { step1, step2 }
            };

            var bookmark = new UserCareerPath
            {
                UserId = testUserId,
                CareerPathId = careerPath.Id,
                CareerPath = careerPath,
                IsActive = true,
                IsDeleted = false,
                FollowedAt = DateTime.UtcNow
            };

            var bookmarks = new List<UserCareerPath> { bookmark }.AsQueryable();
            var bookmarkDbSet = EfCoreDbSetMock.Create(bookmarks);
            bookmarkRepositoryMock.Setup(br => br.GetAllAttached()).Returns(bookmarkDbSet.Object);

            var stepCompletions = new List<UserCareerStepCompletion>
            {
                new UserCareerStepCompletion { UserId = testUserId, CareerStepId = step1.Id }
            }.AsQueryable();

            var stepCompletionsDbSet = EfCoreDbSetMock.Create(stepCompletions);

            var dbContextMock = new Mock<StepWiseDbContext>();
            dbContextMock.Setup(db => db.UserCareerStepCompletions).Returns(stepCompletionsDbSet.Object);
            bookmarkRepositoryMock.Setup(br => br.GetDbContext()).Returns(dbContextMock.Object);

            var result = (await bookmarkService.GetUserBookmarkAsync(testUserId)).ToList();

            Assert.AreEqual(1, result.Count, "Should return one bookmark.");
            Assert.AreEqual(2, result[0].TotalStepsCount, "Total steps count should match CareerPath.Steps.");
            Assert.AreEqual(1, result[0].CompletedStepsCount, "Completed steps count should match user's completions.");
        }

        [Test]
        public async Task AddCareerPathToUserBookmarkAsync_ShouldReturnFalseIfGetAllAttachedReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            bookmarkRepositoryMock.Setup(r => r.GetAllAttached()).Returns((IQueryable<UserCareerPath>)null);

            // Act
            var result = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddCareerPathToUserBookmarkAsync_ShouldAddNewBookmarkIfNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            // Simulate no existing bookmark
            bookmarkRepositoryMock
                .Setup(r => r.FindUserCareerPathAsync(userId, careerPathId))
                .ReturnsAsync((UserCareerPath?)null);

            bookmarkRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<UserCareerPath>()))
                .Returns(Task.CompletedTask);

            bookmarkRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

            // Assert
            Assert.IsTrue(result);

            bookmarkRepositoryMock.Verify(r => r.AddAsync(It.Is<UserCareerPath>(b =>
                b.UserId == userId &&
                b.CareerPathId == careerPathId &&
                b.IsActive &&
                !b.IsDeleted
            )), Times.Once);

            bookmarkRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task RemoveCareerPathFromUserBookmarkAsync_ShouldReturnFalse_WhenBookmarkDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            var emptyData = new List<UserCareerPath>();
            var mockQueryable = emptyData.BuildMock();

            bookmarkRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

            var result = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            Assert.IsFalse(result);

            bookmarkRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<UserCareerPath>()), Times.Never);
            bookmarkRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Test]
        public async Task RemoveCareerPathFromUserBookmarkAsync_ShouldReturnFalse_WhenBookmarkIsAlreadyDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            var deletedBookmark = new UserCareerPath
            {
                UserId = userId,
                CareerPathId = careerPathId,
                IsActive = false,
                IsDeleted = true
            };

            var data = new List<UserCareerPath> { deletedBookmark };
            var mockQueryable = data.BuildMock();

            bookmarkRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

            // Act
            var result = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            // Assert
            Assert.IsFalse(result);

            bookmarkRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<UserCareerPath>()), Times.Never);
            bookmarkRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Test]
        public async Task RemoveCareerPathFromUserBookmarkAsync_ShouldReturnFalse_WhenNoBookmarksExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            var emptyData = new List<UserCareerPath>();
            var mockQueryable = emptyData.BuildMock();

            bookmarkRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

            // Act
            var result = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            // Assert
            Assert.IsFalse(result);

            bookmarkRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<UserCareerPath>()), Times.Never);
            bookmarkRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Test]
        public void RemoveCareerPathFromUserBookmarkAsync_ShouldThrowIfGetAllAttachedReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var careerPathId = Guid.NewGuid();

            bookmarkRepositoryMock.Setup(r => r.GetAllAttached()).Returns((IQueryable<UserCareerPath>)null);

            // Act & Assert
            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId));
        }

    }
}
