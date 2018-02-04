namespace CarDealer.Web.Models.Parts
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Services.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PartFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive number")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public bool IsEdit { get; set; }
    }
}
