using ChineseSaleServer.Models;

namespace ChineseSaleServer.BL
{
    public interface IRandomService
    {

        Task<User> GiftRandom(int giftId);
        Task<List<RandomClass>> Get();
    }
}
