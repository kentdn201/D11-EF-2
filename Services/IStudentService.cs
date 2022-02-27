using D11.Data.Entites;

namespace D11.Services
{
    public interface IStudentService
    {
        public Task<IList<Student>?> GetAllAsync();

        public Task<Student?> GetOneAsync(int id);

        public Task<Student?> AddAsync(Student entity);

        public Task<Student?> EditAsync(int id, Student entity);

        public Task RemoveAsync(int id);
    }
}