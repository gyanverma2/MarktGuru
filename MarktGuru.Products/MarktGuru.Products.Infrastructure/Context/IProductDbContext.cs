﻿using MarktGuru.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace MarktGuru.Products.Infrastructure.Context
{
    public interface IProductDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}