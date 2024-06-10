using ChineseSaleServer.DAL;
using ChineseSaleServer.Models;
using System.Net;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ChineseSaleServer.BL
{
    public class RandomService : IRandomService
    {
        private readonly IPurchasesDal _purchasesDal;
        private readonly IOrderDal _orderDal;
        private readonly IRandomDal _randomDal;
        private readonly IGiftDal _giftDal;

        public RandomService(IPurchasesDal purchasesDal,IOrderDal orderDal ,IRandomDal randomDal,IGiftDal giftDal)
        {
            this._purchasesDal = purchasesDal ?? throw new ArgumentNullException(nameof(purchasesDal));
            this._orderDal = orderDal ?? throw new ArgumentNullException(nameof(orderDal));
            this._randomDal = randomDal;
            _giftDal = giftDal ?? throw new ArgumentNullException(nameof(giftDal));
        }

        public async Task<List<RandomClass>> Get()
        {
            return await this._randomDal.GetAsync();
        }


        public async Task SendEmail1(string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Chinese Sale", "37326150968@mby.co.il"));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = subject;
            var body1 = new TextPart("plain")
            {
                Text = body
            };
            var attachment = new MimePart("image", "jpeg")
            {
                Content = new MimeContent(File.OpenRead("E:\\דדדס\\ChineseSaleServer\\ChineseSaleServer\\wwwroot\\imag\\logo.png")),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName("E:\\דדדס\\ChineseSaleServer\\ChineseSaleServer\\wwwroot\\imag\\logo.png")
            };
            var multipart = new Multipart("mixed");
            multipart.Add(body1);
            multipart.Add(attachment);
            message.Body = multipart;

            var client = new SmtpClient();
            try
            {

                await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(new NetworkCredential("37326150968@mby.co.il", "Student@264"));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

            }
            catch (Exception ex) { }
            finally
            {
                client.Dispose();
            }
        }






        public async Task<User> GiftRandom(int giftId)
        {
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            orderDetailsList = await _purchasesDal.PurchasesOrderDetailsAsync(giftId);
            Random random = new();


            // אם הרשימה לא ריקה, גרור פריט אקראי מהרשימה
            if (orderDetailsList.Count > 0)
            {
                // גרור מספר אקראי בין 0 לאורך הרשימה - 1
                int randomIndex = random.Next(0, orderDetailsList.Count);

                // גש לפריט האקראי ברשימה
                OrderDetails randomOrderDetails = orderDetailsList[randomIndex];

                User user=await _orderDal.GetUserByOrderId(randomOrderDetails.OrderId);
                Gift gift = await _giftDal.GetByIdAsync(giftId);

                RandomClass r = new RandomClass { GiftId = giftId ,UserID=user.Id};
                await _randomDal.AddAsync(r);

                await SendEmail1(user.Email, user.FirstName + ", you won a gift!!!☺","Dear " +user.FirstName+ " you won the "+gift.Name+ " gift!!! our congratulations!!!");
                

                return user;
             }
            return null;
            
        }
    }
}
