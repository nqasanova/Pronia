using System;
namespace Pronia.Areas.Admin.ViewModels.Category
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}