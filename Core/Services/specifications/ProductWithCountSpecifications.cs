using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.specifications
{
   public class ProductWithCountSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParamters product) :base(

            p => 
            (string.IsNullOrEmpty(product.Search) || p.Name.ToLower().Contains(product.Search.ToLower())) &&
            (!product.BrandId.HasValue || p.BrandId == product.BrandId) &&
            (!product.TypeId.HasValue || p.TypeId == product.TypeId)
            
            )
        {
            
        }
    }
}
