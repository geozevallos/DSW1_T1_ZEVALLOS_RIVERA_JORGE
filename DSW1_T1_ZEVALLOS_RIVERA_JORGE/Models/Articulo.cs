using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_ZEVALLOS_RIVERA_JORGE.Models
{
    public class Articulo
    {
        [Display(Name = "Código del artículo")] public string CodArt { get; set; }
        [Display(Name = "Nombre del artículo")] public string NomArt { get; set; }
        [Display(Name = "Unidad de medida")] public string UniMed { get; set; }
        [Display(Name = "Precio")] public decimal PreArt { get; set; }
        [Display(Name = "Stock")] public int StkArt { get; set; }

        [Display(Name = "Id categoría")] public int CodCat { get; set; }

        [Display(Name = "Nombre categoría")] public string? NombreCategoria { get; set; }
    }
}
