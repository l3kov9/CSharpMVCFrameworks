namespace LearningSystem.Services
{
    using LearningSystem.Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITrainerService
    {
        Task<IEnumerable<CourseListingServiceModel>> Courses(string trainerId);

        Task<bool> IsTrainer(string userId, int courseId);

        Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourseAsync(int courseId);

        Task<bool> GradeStudentAsync(int courseId, string studentId, Grade grade);
    }
}
