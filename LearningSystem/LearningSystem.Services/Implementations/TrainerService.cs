namespace LearningSystem.Services.Implementations
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using LearningSystem.Data.Models;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> Courses(string trainerId)
            => await this.db
                .Courses
                .Where(c => c.TrainerId == trainerId)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<bool> GradeStudentAsync(int courseId, string studentId, Grade grade)
        {
            var studentInCourse = await this.db.FindAsync<StudentCourse>(studentId, courseId);

            if(studentInCourse == null)
            {
                return false;
            }

            studentInCourse.Grade = grade;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsTrainer(string userId, int courseId)
            => await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.TrainerId == userId);

        public async Task<IEnumerable<StudentInCourseServiceModel>> StudentsInCourseAsync(int courseId)
            => await this.db
                .Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(s => s.Student))
                .ProjectTo<StudentInCourseServiceModel>(new { courseId })
                .ToListAsync();
    }
}
