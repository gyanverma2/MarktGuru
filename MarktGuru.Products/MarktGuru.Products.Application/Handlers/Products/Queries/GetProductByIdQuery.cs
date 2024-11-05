using MarktGuru.Products.Domain.Models;
using MediatR;

namespace MarktGuru.Products.Application.Handlers.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
