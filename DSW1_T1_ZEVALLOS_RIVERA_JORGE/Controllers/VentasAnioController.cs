using DSW1_T1_ZEVALLOS_RIVERA_JORGE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DSW1_T1_ZEVALLOS_RIVERA_JORGE.Controllers
{
    public class VentasAnioController : Controller
    {
        private readonly IConfiguration _configuration;

        public VentasAnioController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        IEnumerable<VentasAnio> ListarVentasAnio(int anio)
        {
            List<VentasAnio> ventas = new List<VentasAnio>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener_ventas_por_anio", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@year", anio);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    VentasAnio venta = new VentasAnio
                    {
                        NumVta = sqlDataReader.GetInt32(0),
                        FecVta = sqlDataReader.GetDateTime(1),
                        NomCli = sqlDataReader.GetString(2),
                        NomVen = sqlDataReader.GetString(3),
                        TotVta = sqlDataReader.GetDecimal(4)
                    };
                    ventas.Add(venta);
                }
            }
            return ventas;
        }


        public IActionResult Index(int anio, int pagina = 0)
        {
            IEnumerable<VentasAnio> ventas;

            if (anio == 0)
            {
                ventas = new List<VentasAnio>();
            }
            else
            {
                ventas = ListarVentasAnio(anio);
            }

            int cantidadRegistrosPorPagina = 10;
            int cantidadTotalRegistros = ventas.Count();
            int cantidadTotalPaginas = cantidadTotalRegistros % cantidadRegistrosPorPagina == 0 ?
                                        cantidadTotalRegistros / cantidadRegistrosPorPagina :
                                        cantidadTotalRegistros / cantidadRegistrosPorPagina + 1;
            ViewBag.Anio = anio;
            ViewBag.PaginaActual = pagina;
            ViewBag.CantidadTotalPaginas = cantidadTotalPaginas;
            return View(ventas.Skip(cantidadRegistrosPorPagina * pagina).Take(cantidadRegistrosPorPagina));
        }
    }
}
