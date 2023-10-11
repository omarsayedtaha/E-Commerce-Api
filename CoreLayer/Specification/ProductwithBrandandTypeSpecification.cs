
using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatApi.Helper;



namespace CoreLayer.Specification
{
    public class ProductwithBrandandTypeSpecification:BaseSpecification<Product>
    {
        public ProductwithBrandandTypeSpecification(ProductSpecParams specparams)
            :base(P=>(!specparams.brandId.HasValue ||P.ProductBrandId== specparams.brandId.Value) &&
            (!specparams.typeId.HasValue ||P.ProductTypeId== specparams.typeId.Value)&&
            (string.IsNullOrEmpty(specparams.Search)||P.Name.ToLower().Contains(specparams.Search.ToLower())))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(specparams.Sort))
            {
                switch (specparams.Sort)
                {
                    case "PriceAscen": OrderBy = P => P.Price;
                        break;
                    case "Pricedescen": OrderByDescending = P => P.Price;
                        break;
                    default: OrderBy = P => P.Name;
                        break;

                }

            }

            
             ApplyPagination((specparams.PageIndex - 1) * specparams.PageSize, specparams.PageSize);

        }

        public ProductwithBrandandTypeSpecification(int id ):base(P=>P.Id==id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
