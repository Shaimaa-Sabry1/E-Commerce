using Domain.Models;
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
        public ProductWithBrandsAndTypeSpecifications(int? brandId, int? typrId) :base(
            p=>(!brandId.HasValue||p.BrandId==brandId)&&
            (!typrId.HasValue|| p.TypeId==typrId)
            )
        {
            ApplayInclude();
        }
        private void ApplayInclude()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
