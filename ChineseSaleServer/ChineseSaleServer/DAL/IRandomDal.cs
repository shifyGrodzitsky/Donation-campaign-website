using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public interface IRandomDal
    {
        Task AddAsync(RandomClass r);
        Task<List<RandomClass>> GetAsync();
    }
}
