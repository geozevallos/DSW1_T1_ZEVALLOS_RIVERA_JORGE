using DSW1_T1_ZEVALLOS_RIVERA_JORGE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace DSW1_T1_ZEVALLOS_RIVERA_JORGE.Controllers
{
    public class ExamenT1Controller : Controller
    {
        private readonly IConfiguration _configuration;

        public ExamenT1Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        IEnumerable<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener_categorias", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        CodCat = sqlDataReader.GetInt32(0),
                        Nombre = sqlDataReader.GetString(1),
                        Descripcion = sqlDataReader.GetString(2),
                    };
                    categorias.Add(categoria);
                }
            }
            return categorias;
        }

        IEnumerable<Articulo> ListarArticulos()
        {
            List<Articulo> articulos = new List<Articulo>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("usp_obtener_articulos", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Articulo articulo = new Articulo
                    {
                        CodArt = sqlDataReader.GetString(0),
                        NomArt = sqlDataReader.GetString(1),
                        UniMed = sqlDataReader.GetString(2),
                        PreArt = sqlDataReader.GetDecimal(3),
                        StkArt = sqlDataReader.GetInt32(4),
                        CodCat = sqlDataReader.GetInt32(5),
                        NombreCategoria = sqlDataReader.GetString(6)
                    };
                    articulos.Add(articulo);
                }
            }
            return articulos;
        }

        public IActionResult Index()
        {
            return View(ListarArticulos());
        }

        String registrarArticulo(Articulo articulo)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:database"]))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("usp_insertar_articulos", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CodArt", articulo.CodArt);
                sqlCommand.Parameters.AddWithValue("@NomArt", articulo.NomArt);
                sqlCommand.Parameters.AddWithValue("@UniMed", articulo.UniMed);
                sqlCommand.Parameters.AddWithValue("@PreArt", articulo.PreArt);
                sqlCommand.Parameters.AddWithValue("@StkArt", articulo.StkArt);
                sqlCommand.Parameters.AddWithValue("@CodCat", articulo.CodCat);
                int cantidadFilas = sqlCommand.ExecuteNonQuery();
                string mensaje = cantidadFilas > 0 ? "Artículo guardado" : "Error al guardar al artículo";
                return mensaje;
            }
        }

        public IActionResult Agregar()
        {
            IEnumerable<Categoria> categorias = ListarCategorias();
            ViewBag.Categorias = new SelectList(categorias, "CodCat", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Articulo articulo)
        {
            string mensaje = registrarArticulo(articulo);
            ViewBag.Mensaje = mensaje;
            return View(articulo);
        }
    }
}
