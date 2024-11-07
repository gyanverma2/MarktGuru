using System.ComponentModel.DataAnnotations;

namespace MarktGuru.Products.Common.Enums
{
    public enum SourceTypeId
    {
        [Display(Name = "Any")]
        Any = 1,
        [Display(Name = "Web")]
        Web = 2,
        [Display(Name = "Mobile")]
        Mobile = 3,
        [Display(Name = "Tablet")]
        Tablet = 4
    }
}
