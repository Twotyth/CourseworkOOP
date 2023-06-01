using DataLayer.Dtos;

namespace ApplicationTest;

[TestClass]
public class ClientServiceTest : Utils
{
    public ClientServiceTest()
    {
        ProductRepos.Add(new ProductDto(0, "test1", "test1", "test1", null, 0.4m, true));
        ProductRepos.Add(new ProductDto(0, "test2", "test2", "test2", null, 0.5m, true));
        ProductRepos.Add(new ProductDto(0, "test3", "test3", "test3", null, 0.4m, false));
        ProductRepos.Add(new ProductDto(0, "test4", "test4", "test4", null, 0.5m, false));
    }
    
    [TestMethod]
    public void OrderHappy()
    {
        
    }
    
}