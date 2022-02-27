using D11.Data.Entites;
using D11.Data;
using Microsoft.EntityFrameworkCore;

namespace D11.Services
{
    public class StudentService : IStudentService
    {
        private readonly MyDbContext _context;

        public StudentService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Student>?> GetAllAsync()
        {
            return _context.Students != null ? await _context.Students.ToListAsync(): new List<Student>();
        }

        public async Task<Student?> GetOneAsync(int id)
        {
            if(_context.Students == null) return null;

            return await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task<Student?> AddAsync(Student entity)
        {
            if(_context.Students == null) return null;

            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Student?> EditAsync(int id, Student entity)
        {
            if(_context.Students == null) return null;

            _context.Students.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            if(_context.Students == null) return;

            var entity = await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id);
            if(entity == null) return;

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}