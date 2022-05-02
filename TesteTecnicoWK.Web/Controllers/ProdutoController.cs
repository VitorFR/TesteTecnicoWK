using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TesteTecnicoWK.Dominio.Entidades;
using TesteTecnicoWK.Web.Infraestructure.Interfaces;

namespace TesteTecnicoWK.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IConfigureAPIService _ConfigureAPIService;
        public ProdutoController(IConfigureAPIService ConfigureAPIService)
        {
            _ConfigureAPIService = ConfigureAPIService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Listar()
        {
            IEnumerable<Produto> produtos = new List<Produto>();
            HttpResponseMessage response;
            HttpClient client = _ConfigureAPIService.Configure();
            response = client.GetAsync("/produtos").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                produtos = response.Content.ReadFromJsonAsync<IEnumerable<Produto>>().Result;
            }

            return PartialView(produtos);
        }

        public IActionResult Novo(Produto produto)
        {

            string jsonObject = JsonSerializer.Serialize(produto);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.PostAsync("/produto/{produto}", content);
            response.Wait();

            return Json(response.Result);
        }

        public async Task<IActionResult> Get(string id)
        {
            HttpResponseMessage response;
            HttpClient client = _ConfigureAPIService.Configure();
            response = await client.GetAsync($"/produto/{id}");
            Produto produto = new Produto();

            if (response.IsSuccessStatusCode)
            {
                produto = response.Content.ReadFromJsonAsync<Produto>().Result;
            }

            return Json(produto);
        }

        public IActionResult Editar(Produto produto)
        {

            string jsonObject = JsonSerializer.Serialize(produto);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.PutAsync("/produto/{produto}", content);
            response.Wait();

            return Json(response.Result);
        }

        public IActionResult Deletar(Produto produto)
        {
            HttpClient client = _ConfigureAPIService.Configure();
            var response = client.DeleteAsync($"/produto/{produto.Id}");
            response.Wait();

            return Json(response.Result);
        }
    }
}
