using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Services.specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
           
            var brand = await unitOfWork.GetGenericRepository<ProductBrand, int>().GellAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brand);
            return result;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typrId)
        {
            var spec = new ProductWithBrandsAndTypeSpecifications(brandId, typrId);

            //Get All Products Throught ProductRepository
            var products=await unitOfWork.GetGenericRepository<Product, int>().GellAllAsync(spec);
            //Mapping IEnumerable<Product> to IEnumerable<ProductResultDto>:AutoMapper
           var result= mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
        {
            var types = await unitOfWork.GetGenericRepository<ProductType, int>().GellAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

        public async Task<ProductResultDto?> GettProductByIdAsync(int Id)
        {
            var spec = new ProductWithBrandsAndTypeSpecifications(Id);

            var product =await unitOfWork.GetGenericRepository<Product, int>().GetAsync(spec);
            if (product is null) return null;
           var result= mapper.Map<ProductResultDto>(product);
            return result;
        }
    }
}
