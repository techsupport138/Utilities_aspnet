namespace Utilities_aspnet.Product.Dto;

public class VoteFieldCreateDto {
    public string Title { get; set; }
}

public class VoteReadDto {
    public string Title { get; set; }
    public double Point { get; set; }
}