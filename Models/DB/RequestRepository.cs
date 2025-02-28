using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC.Models.Db;

namespace ASP.NET_MVC.Models.Db
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;

        public RequestRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddRequest(Request request)
        {
            // Добавление запроса
            await _context.Requests.AddAsync(request);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
    }
}
