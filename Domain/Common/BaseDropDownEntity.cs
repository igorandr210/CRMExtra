namespace Domain.Common
{
    public class BaseDropDownEntity<T>:BaseAuditableEntity
    {
        public T Value { get; set; }
    }
}