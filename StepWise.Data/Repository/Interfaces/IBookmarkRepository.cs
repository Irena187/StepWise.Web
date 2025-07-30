namespace StepWise.Data.Repository.Interfaces
{
    using Models;

    public interface IBookmarkRepository
        : IRepository<UserCareerPath, object>, IAsyncRepository<UserCareerPath, object>
    {
        UserCareerPath? GetByCompositeKey(string userId, string careerPathId);

        Task<UserCareerPath?> GetByCompositeKeyAsync(string userId, string careerPathId);

        bool Exists(string userId, string careerPathId);

        Task<bool> ExistsAsync(string userId, string careerPathId);

        Task<UserCareerPath?> FindUserCareerPathAsync(Guid userId, Guid careerPathId);

    }
}
