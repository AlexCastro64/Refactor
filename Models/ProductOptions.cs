using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Models
{
    public class ProductOptions
    {

        public List<ProductOption> Items { get; private set; }

        public ProductOptions()
        {
            LoadProductOptions(null);
        }

        public ProductOptions(Guid productId)
        {
            LoadProductOptions($"where productid = '{productId}' collate nocase");
        }

        // Load the product options
        private void LoadProductOptions(string where)
        {
            Items = new List<ProductOption>();
            var database = new Database();
            var sql = $"select id from productoptions {where}";
            database.Query(sql);
            while (database.reader.Read())
            {
                var id = Guid.Parse(database.reader.GetString(0));
                Items.Add(new ProductOption(id));
            }
        }
    }
}
