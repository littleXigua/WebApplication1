using EasyCaching.Core.Interceptor;

namespace WebAPI.services
{
    public class StudentService : IStudentService
    {
        public Task AddStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByIdAsync(int id)
        {
            var s= new Student()
            {
                id = 1,
                name = "John Doe"
            };

            return Task.FromResult<Student>(s);
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }


    public interface IStudentService
    {
        [EasyCachingAble(Expiration = 300)]
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        [EasyCachingAble(Expiration = 300)]
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }


    public class Student
    {
        public string name { get; set; }
        public int id { get; set; }
    }

}


