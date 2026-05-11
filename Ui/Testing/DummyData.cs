using BL.Contracts;
using BL.DTOs;
using Domains.Enums;

namespace Ui.Testing
{
    public class DummyData(
        IUserService userService
    )
    {
        private readonly IUserService _userService = userService;
        public async Task<TbShipmentDTO> FillShipment()
        {
            var senderContact = new TbUserContactDTO
            {
                //Id = Guid.Empty,
                UserId = await _userService.GetLoggedInUserId(),
                FullName = "أحمد محمد",
                Email = "ahmed@example.com",
                Phone = "01012345678",
                CityId = Guid.Parse("4942c489-825f-45ea-a9f6-59a8d758fe38"),
                Address = "شارع جمال عبد الناصر، الإسكندرية",
                ContactType = ContactType.Sender_Only, 
                PostalCode = "21500",
                IsDefaultAddress = true
            };

            var receiverContact = new TbUserContactDTO
            {
                //Id = Guid.Empty,
                UserId = await _userService.GetLoggedInUserId(),
                FullName = "محمود رضا",
                Email = "mahmoud@example.com",
                Phone = "01287654321",
                CityId = Guid.Parse("4942c489-825f-45ea-a9f6-59a8d758fe38"),
                Address = "شارع رمسيس، القاهرة",
                ContactType = ContactType.Receiver_Only,
                PostalCode = "11511",
                IsDefaultAddress = false
            };

            // 2. إعداد بيانات الشحنة (Shipment)
            return new TbShipmentDTO
            {
                UserId = await _userService.GetLoggedInUserId(),
                ShippingDate = DateTime.Now.AddDays(1),
                DelivryDate = DateTime.Now.AddDays(3),
                SenderId = senderContact.Id,
                ReceiverId = receiverContact.Id,
                Sender = senderContact,
                Receiver = receiverContact,
                ShippingTypeId = Guid.Parse("bea5e7c8-2097-4e6d-8398-829c78a51a81"),
                Width = 50.0,
                Height = 30.0,
                Weight = 5.5,
                Length = 40.0,
                PackageValue = 1500.00m,
                ShippingRate = 1.5m,
                TrackingNumber = "TRK-99887766",
                ReferenceId = Guid.NewGuid()
            };
        }
    }
}
