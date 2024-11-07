namespace MarktGuru.Products.Common.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base("Record not found")
        {
        }
    }
}
