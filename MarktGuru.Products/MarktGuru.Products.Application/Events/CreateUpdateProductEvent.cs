using MediatR;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Events
{
    public class CreateUpdateProductEvent : INotification
    {
        public int ProductId { get; }
        public string ProductName { get; }

        public CreateUpdateProductEvent(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }
    }
    public class CreateUpdateProductEventHandler : INotificationHandler<CreateUpdateProductEvent>
    {
        private readonly ILogger<CreateUpdateProductEventHandler> _logger;

        public CreateUpdateProductEventHandler(ILogger<CreateUpdateProductEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateUpdateProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateUpdateProductEvent: Product ID: {ProductId}, Name: {ProductName}.", notification.ProductId, notification.ProductName);
            //Business logic to handle the event

            return Task.CompletedTask;
        }
    }
}
