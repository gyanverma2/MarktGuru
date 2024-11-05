namespace MarktGuru.Products.Common.Wrapper
{
    public interface IResult
    {
        List<string> Messages { get; set; }
        bool Succeeded { get; set; }
    }
}
