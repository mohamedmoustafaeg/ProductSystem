using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductSystem.BusinessLogic.Interface;
using ProductSystem.DataAccess.Interface;
using ProductSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace ProductSystem.BusinessLogic.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _context;
        public ProductService(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            await _context.Products.Add(product);
            await _context.Compelete();
        }


        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products.GetAll();
            return products;
        }
        public async Task DeleteById(int id)
        {
            _context.Products.Delete(id);
            await _context.Compelete();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.GetById(id);
        }

        public async Task Update(int Id, Product product, string userId)
        {
            var existingProduct = await _context.Products.GetById(Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.CreationDate = product.CreationDate;
                existingProduct.StartDate = product.StartDate;
                existingProduct.Price = product.Price;
                existingProduct.ImageData = product.ImageData;
                existingProduct.CategoryId = product.CategoryId;
                await _context.Products.Update(existingProduct, userId);
                await _context.Compelete();
            }
        }
    }
        
    }
