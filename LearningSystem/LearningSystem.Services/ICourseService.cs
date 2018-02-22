namespace LearningSystem.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingServiceModel>> Active();

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> UserIsEnrolledInCourseByIdAsync(string userId, int courseId);

        Task<bool> AddStudentToCourseAsync(string userId, int courseId);

        Task<bool> RemoveStudentFromCourseAsync(string userId, int courseId);
    }
}
