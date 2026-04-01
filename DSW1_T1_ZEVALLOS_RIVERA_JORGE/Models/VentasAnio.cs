using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_ZEVALLOS_RIVERA_JORGE.Models
{
    public class VentasAnio
    {
        [Display(Name = "N° venta")] public int NumVta { get; set; }
        [Display(Name = "Fecha de venta")] public DateTime FecVta { get; set; }
        [Display(Name = "Nombre del cliente")] public string NomCli { get; set; }
        [Display(Name = "Nombre del vendedor")] public string NomVen { get; set; }
        [Display(Name = "Total de Venta")] public decimal TotVta { get; set; }
    }
}
