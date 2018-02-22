namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using LearningSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseListingServiceModel>> Active()
            => await this.db
                .Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .ProjectTo<CourseListingServiceModel>()
                .ToListAsync();

        public async Task<bool> AddStudentToCourseAsync(string userId, int courseId)
        {
            var userCourse = await this.db
                    .FindAsync<StudentCourse>(userId, courseId);

            if (userCourse != null)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                StudentId = userId,
                CourseId = courseId
            };

            this.db.Add(studentCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class
            => await this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> RemoveStudentFromCourseAsync(string userId, int courseId)
        {
            var userCourse = await this.db
                    .FindAsync<StudentCourse>(userId, courseId);

            if(userCourse == null)
            {
                return false;
            }

            this.db
                .Remove(userCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserIsEnrolledInCourseByIdAsync(string userId, int courseId)
            => await this.db
                .Courses
                .AnyAsync(c => c.Id == courseId && c.Students.Any(st => st.Student.Id == userId));
    }
}
