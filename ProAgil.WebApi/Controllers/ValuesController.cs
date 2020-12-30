using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebApi.Data;
using ProAgil.WebApi.Model;

namespace ProAgil.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        //public ActionResult<IEnumerable<Evento>> Get()
        // async => método assincrono
        // Task => para que o controller saiba que tem que abrir uma thread a cada nova requisição no metodo get
        public async Task<IActionResult> Get()
        {
            try
            {
                // var results = _context.Eventos.ToList();
                // Para metodo assincrono deve mudar o evento ToList para ToLIstAsync (assincrono) da biblioteca EntityFrameworkCore
                var results = await _context.Eventos.ToListAsync();
                // O evento ToListAsync executa em thread portanto os demais codigo serao executados, para evitar
                // qualquer problema durante a consulta ao banco, é acrescentado o termo "await" para prosseguir somente depois que o evento terminar
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }
            

            //return new Evento[]
            //{
                

            //    //new Evento()
            //    //{
            //    //    EventoId = 1,
            //    //    Tema = "Angular e .NET Core",
            //    //    Local = "Belo Horizonte",
            //    //    Lote = "1º Lote",
            //    //    QuantidadePessoas = 250,
            //    //    DataEvento = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy")
            //    //},
            //    //new Evento()
            //    //{
            //    //    EventoId = 2,
            //    //    Tema = "Angular e Suas novidades",
            //    //    Local = "São Paulo",
            //    //    Lote = "2º Lote",
            //    //    QuantidadePessoas = 350,
            //    //    DataEvento = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy")
            //    //}
            //};
        }

        // GET api/values/5

        //[HttpGet("{id}")]
        // ActionResult é Padrão MVC do RAZOR
        // public ActionResult<Evento> Get(int id)
        //{
        //    return _context.Eventos.FirstOrDefault(e => e.EventoId == id);
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        //public IActionResult Get(int id)
        public async Task<IActionResult> Get(int id)
        {
            // Para o IActionResult o retorno tem que ser o método "OK"
            // return Ok(_context.Eventos.FirstOrDefault(e => e.EventoId == id));

            // Trantando os erros de retornos
            try
            {
                var results = await _context.Eventos.FirstOrDefaultAsync(e => e.EventoId == id);
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
