namespace ApiBackend.Dto.ProductDto;
public class AddProductDto
{
    public int prodId { get; set; }
    public int categoryId { get; set; }
    public string prodName { get; set; }
    public string prodDescription { get; set; }
    public Decimal prodPrice { get; set; }
    public string prodSertficate { get; set; }
}