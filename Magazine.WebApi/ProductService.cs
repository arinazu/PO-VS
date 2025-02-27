
using Magazine.Core.Models;
using Magazine.Core.Services;
using Magazine.WebApi.Controllers;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;

namespace Magazine.WebApi
{
    public class ProductService:IProductService
    {

        private readonly Dictionary<Guid, Product> _products;
        private readonly IConfiguration _config;
        private readonly string _databaseFilePath;
        private static readonly Mutex _mutex = new Mutex();
        public ProductService(IConfiguration config)
        {
            _config = config;
            _products = new Dictionary<Guid, Product>();
            _databaseFilePath = _config["DataBaseFilePath"];

            Console.WriteLine($"Путь к базе данных: {_databaseFilePath}");
            InitFromFile(); // Загружаем данные из файла
        }
        private void InitFromFile()
        {
            if (!File.Exists(_databaseFilePath))
            {
                Console.WriteLine("Файл базы данных не найден");
                return;
            }

            string json = File.ReadAllText(_databaseFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("Файл базы данных пуст, инициализируем его.");
                File.WriteAllText(_databaseFilePath, "{}");
                json = "{}";
            }
            var loadedProducts = JsonSerializer.Deserialize<Dictionary<Guid, Product>>(json);

            if (loadedProducts != null)
            {
                _products.Clear(); 
                foreach (var i in loadedProducts)
                {
                    _products[i.Key] = i.Value;
                }
            }
            Console.WriteLine($"Загружено {_products.Count} продуктов из файла.");
        }

        private void SaveToFile()
        {
            //_mutex.WaitOne();
            string json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_databaseFilePath, json);
            Console.WriteLine("База данных обновлена.");
            //_mutex.ReleaseMutex();
        }

        public Product Add(string defin, string name, double price, string image)
        {
            _mutex.WaitOne();
            Console.WriteLine("using add");
            if (string.IsNullOrEmpty(defin) || string.IsNullOrEmpty(name) || price < 0 || string.IsNullOrEmpty(image))
            {
                return null;
            }
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Definition = defin,
                Name = name,
                Price = price,
                Image = image
            };
            //Console.WriteLine("Add Пошли спать ");
            //Thread.Sleep(25000);
            //Console.WriteLine("Add Проснулись");
            if (_products.TryAdd(product.Id, product))
            {
                SaveToFile();
                Console.WriteLine("GOOD add");
                Console.WriteLine("id продукта: " + product.Id);
                Console.WriteLine("Добавление: " + product.Definition + " " + product.Name + " " + product.Price + " " + product.Image);
                _mutex.ReleaseMutex();
                return product;
            }
            Console.WriteLine("BAD add");
            _mutex.ReleaseMutex();
            return null;
        }
        public void Remove(Guid id)
        {
            _mutex.WaitOne();
            Console.WriteLine("using remove");
            if (_products.ContainsKey(id))
            {
                var product = _products[id];
                if (_products.Remove(id))
                {
                    SaveToFile();
                    Console.WriteLine("GOOD remove");
                    _mutex.ReleaseMutex();
                    //return product;
                    return;
                }
            }
            Console.WriteLine("BAD remove");
            _mutex.ReleaseMutex();
            //return;
            //return product;
        }
        public Product Edit(Guid id, string defin, string name, double price, string image)
        {
            _mutex.WaitOne();
            Console.WriteLine("using edit");

            if (_products.ContainsKey(id))
            {
                var product = _products[id];

                
                if (defin != "-")
                {
                    product.Definition = defin;
                }
                if (name != "-")
                {
                    product.Name = name;
                }
                if (price > 0)
                {
                    product.Price = price;
                }
                if (image != "-")
                {
                    product.Image = image;
                }
                //(!string.IsNullOrEmpty(image))
                //Console.WriteLine("Edit Пошли спать ");
                //Thread.Sleep(25000);
                //Console.WriteLine("Edit Проснулись");

                SaveToFile();
                Console.WriteLine("GOOD edit");
                Console.WriteLine("Редактирование: ");
                Console.WriteLine(product.Definition);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.Image);
                _mutex.ReleaseMutex();
                return product;
            }
            Console.WriteLine("BAD edit");
            _mutex.ReleaseMutex();
            return null;
        }
        public Product Search(Guid id)
        {
            _mutex.WaitOne();
            Console.WriteLine("using search");

            foreach (var product in _products.Values)
            {
                if (product.Id == id)
                {
                    Console.WriteLine("GOOD search");
                    Console.WriteLine("Поиск: " + product.Definition + " " + product.Name + " " + product.Price + " " + product.Image);
                    _mutex.ReleaseMutex();
                    return product;
                }
            }
            Console.WriteLine("BAD search");
            _mutex.ReleaseMutex();
            return null;
        }
    }
}
