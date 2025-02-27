using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magazine.Core.Models;

namespace Magazine.Core.Services
{
    public interface IProductService
    {
        public Product Add(string defin, string name, double price, string image);
        public void Remove(Guid id);
        public Product Edit(Guid id, string defin, string name, double price, string image);
        public Product Search(Guid id);
    }
}
