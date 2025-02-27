using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Magazine.Core.Models;
using Magazine.WebApi;
using Magazine.Core.Services;

namespace Magazine.WebApi.Controllers
{
    /// <summary>
    /// Provides endpoints for interacting with products
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //public Dictionary<Guid, Product> _products;
        private IProductService ProductServ;
        public ProductsController(IProductService newProductServ)
        {
            ProductServ = newProductServ;
            //_products = new Dictionary<Guid, Product>();
            //this.InitProducts();
        }
        //private void InitProducts()
        //{
        //    foreach (var item in Enumerable.Range(0, 100))
        //    {
        //        var product = new Product()
        //        {
        //            Definition = $"Information about {item}",
        //            Id = Guid.NewGuid(),
        //            Price = Random.Shared.NextDouble(),
        //        };
        //        _products.Add(product.Id, product);
        //    }
        //}
        ///// <summary>
        ///// Get Products
        ///// </summary>
        ///// <returns>Json array</returns>
        //[HttpGet]
        //[Route("list")]

        //public IEnumerable<Product> GetProducts()
        //{
        //    ProductServ.Search();

        //    return Enumerable.Range(1, 5).Select(index => new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Definition = "Information about product",
        //        Price = Random.Shared.NextDouble(),
        //        Name = "Name of priduct"
        //    })
        //    .ToArray();
        //}
        //-----------------------------------------------------------------------------
        /// <summary>
        /// SEARCH PRODUCT
        /// </summary>
        /// <param name="id"></param>

        [HttpGet]
        
        public void SearchProducts(Guid id)
        {
            var product = new Product();
            product = ProductServ.Search(id);
        }
        //-----------------------------------------------------------------------------
        /// <summary>
        /// ADD PRODUCT
        /// </summary>
        /// <param name="defin"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>

        [HttpPost]

        public void AddProducts(string defin, string name, double price, string image)
        {
            var product = new Product();
            product = ProductServ.Add(defin, name, price, image);
        }
        //-----------------------------------------------------------------------------
        /// <summary>
        /// DELETE PRODUCT
        /// </summary>
        /// <param name="id"></param>

        [HttpDelete]

        public void RemoveProducts(Guid id)
        {
            //var product = new Product();
            ProductServ.Remove(id);
        }
        //-----------------------------------------------------------------------------
        /// <summary>
        /// EDIT PRODUCT
        /// </summary>
        /// <param name="id"></param>

        [HttpPut]
        public void EditProducts(Guid id, string defin, string name, double price, string image)
        {
            var product = new Product();
            product = ProductServ.Edit(id, defin, name, price, image);
        }


        //public IEnumerable<Product> GetProducts(int start = 0, int take = 10)
        //{
        //    return this._products.Skip(start).Take(take).Select(x => x.Value).ToArray();
        //}



        ///// <summary>
        ///// Creates a product with the specified description
        ///// </summary>
        ///// <param name="definition"></param>
        ///// <param name="price"></param>
        ///// <returns></returns>
        //[HttpPost]
        ////[Route("create")]
        //public Guid CreateProduct(string definition, double price)
        //{
        //    if (string.IsNullOrEmpty(definition) || price < 0)
        //    {
        //        return Guid.Empty;
        //    }
        //    var product = new Product()
        //    {
        //        Id = Guid.NewGuid(),
        //        Definition = definition,
        //        Price = price
        //    };
        //    if(this._products.TryAdd(product.Id, product))
        //    {
        //        return product.Id;
        //    }
        //    return Guid.Empty;  
        //}


        ///// <summary>
        ///// Get Information about product by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        ////[Route("product")]
        //public Product? GetProductById(Guid id)
        //{
        //    if(this._products.ContainsKey(id)) return this._products[id];
        //    return null;
        //}
    }
}
