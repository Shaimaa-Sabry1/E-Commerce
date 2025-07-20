using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public interface IProductService
    {

        //Get All Product
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId,int? typrId, string? sort);

        //Get Product By Id
        Task<ProductResultDto?> GettProductByIdAsync(int Id);

        //Get All Type

        Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();

        // Get All Brand
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();

    }
}
