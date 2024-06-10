using ChineseSaleServer.Dal;
using ChineseSaleServer.Models;

namespace ChineseSaleServer.DAL
{
    public class RandomDal:IRandomDal
    {
        private readonly ChineseSaleContext _chineseSaleContext;

        public RandomDal(ChineseSaleContext chineseSaleContext)
        {
            this._chineseSaleContext = chineseSaleContext ?? throw new ArgumentNullException(nameof(chineseSaleContext));
        }
    


    public async Task AddAsync(RandomClass r)
    {
       await _chineseSaleContext.Random.AddAsync(r);
       await _chineseSaleContext.SaveChangesAsync();
    }

        public async Task<List<RandomClass>> GetAsync()
        {
            return await _chineseSaleContext.Random.ToListAsync();
        }
    }
}