using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Persistence;

public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll() => DataSet.AllProducts;

    public Product GetProductById(int productId)
    {
        return DataSet.AllProducts.Where(x => x.Id == productId).FirstOrDefault();
    }
}