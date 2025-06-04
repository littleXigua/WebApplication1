

namespace WebAPI.services
{


    public class StudentRepo
    { 
        public static List<Student> list= new List<Student>()
        {
            new Student()
            {
                Id = 1,
                Name = "John Doe"
            },
            new Student()
            {
                Id = 2,
                Name = "Jane Smith"
            },
            new Student()
            {
                Id = 6,
                Name = "Jane Smith"
            }
        };

    }
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

        public Task<List<Student>> GetAllStudentsAsync()
        {
            Console.WriteLine("实际执行了 GetAllStudentsAsync");
            return Task.FromResult(StudentRepo.list);
        }

        public Task<Student> GetStudentByIdAsync(int id)
        {

            Console.WriteLine("实际执行了 GetStudentByIdAsync");
           
            var s = StudentRepo.list.FirstOrDefault(x => x.Id == id);   

            return Task.FromResult<Student>(s);
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }


    public interface IStudentService
    {       
        Task<List<Student>> GetAllStudentsAsync();

       
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }


    public class Student
    {
        public string? Name { get; set; }
        public int Id { get; set; }
    }

}


