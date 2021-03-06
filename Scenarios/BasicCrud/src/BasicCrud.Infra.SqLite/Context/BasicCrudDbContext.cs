﻿using BasicCrud.Domain.Entities;
using BasicCrud.Infra.SqLite.Context.Builders;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqLite.Context
{
    public class BasicCrudDbContext : TnfDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
        public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        }
    }
}