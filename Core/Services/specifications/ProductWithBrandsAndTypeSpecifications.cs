using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.specifications
{
   public class ProductWithBrandsAndTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandsAndTypeSpecifications(int id):base(p=>p.Id==id)
        {
            ApplayInclude();
        }
        public ProductWithBrandsAndTypeSpecifications(ProductSpecificationsParamters product) :base(
            
            p=> (string.IsNullOrEmpty(product.Search)||p.Name.ToLower().Contains(product.Search.ToLower())) && 
            (!product.BrandId.HasValue||p.BrandId==product.BrandId)&&
            (!product.TypeId.HasValue|| p.TypeId==product.TypeId)
            )
        {
            ApplayInclude();
            ApplaySorting(product.Sort);
            ApplyPagination(product.PageIndex, product.PageSize);

        }
        private void ApplayInclude()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        private void ApplaySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
    }
}
