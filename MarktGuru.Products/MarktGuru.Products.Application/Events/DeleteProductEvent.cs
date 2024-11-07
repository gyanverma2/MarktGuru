using MediatR;
using Microsoft.Extensions.Logging;

namespace MarktGuru.Products.Application.Events
{
    public class DeleteProductEvent : INotification
    {
        public int ProductId { get; }
        public DeleteProductEvent(int productId)
        {
            ProductId = productId;
        }
    }
    public class DeleteProductEventHandler : INotificationHandler<DeleteProductEvent>
    {
        private readonly ILogger<DeleteProductEventHandler> _logger;

        public DeleteProductEventHandler(ILogger<DeleteProductEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeleteProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteProductEvent: Product ID: {ProductId}.", notification.ProductId);
            //Business logic to handle the event

            return Task.CompletedTask;
        }
    }
}
