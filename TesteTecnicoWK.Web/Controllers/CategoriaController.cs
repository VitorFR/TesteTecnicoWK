using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TesteTecnicoWK.Dominio.Entidades;
using TesteTecnicoWK.Web.Infraestructure.Interfaces;

namespace TesteTecnicoWK.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IConfigureAPIService _ConfigureAPIService;
        public CategoriaController(IConfigureAPIService ConfigureAPIService)
        {
            _ConfigureAPIService = ConfigureAPIService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Listar()
        {
            IEnumerable<Categoria> categorias = new List<Categoria>();
            HttpResponseMessage response;
            HttpClient client = _ConfigureAPIService.Configure();
            response = client.GetAsync("/categorias").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                categorias = response.Content.ReadFromJsonAsync<IEnumerable<Categoria>>().Result;
            }

            return PartialView(categorias);
        }

        public IActionResult _SelectListaCategoria()
        {
            IEnumerable<Categoria> categorias = new List<Categoria>();
            HttpResponseMessage response;
            HttpClient client = _ConfigureAPIService.Configure();
            response = client.GetAsync("/categorias").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                categorias = response.Content.ReadFromJsonAsync<IEnumerable<Categoria>>().Result;
            }

            return PartialView(categorias);
        }

        public IActionResult Novo(string? id, string nome)
        {
            Categoria categoria = new Categoria()
            {
                Nome = nome
            };

            string jsonObject = JsonSerializer.Serialize(categoria);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                       
            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.PostAsync("/categoria/{categoria}", content);
            response.Wait();

            return Json(response.Result);
        }

        public IActionResult Editar(string id, string nome)
        {
            Categoria categoria = new Categoria()
            {
                Id = Guid.Parse(id),
                Nome = nome
            };

            string jsonObject = JsonSerializer.Serialize(categoria);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                       
            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.PutAsync("/categoria/{categoria}", content);
            response.Wait();

            return Json(response.Result);
        }

        public IActionResult Deletar(string id, string nome)
        {
            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.DeleteAsync($"/categoria/{id}");
            response.Wait();

            return Json(response.Result);
        }
    }
}
