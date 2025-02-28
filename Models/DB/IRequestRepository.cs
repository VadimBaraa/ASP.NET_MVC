using ASP.NET_MVC.Models.Db;

namespace ASP.NET_MVC.Models.Db
{
    public interface IRequestRepository
    {
        Task AddRequest(Request request);
    }
}
