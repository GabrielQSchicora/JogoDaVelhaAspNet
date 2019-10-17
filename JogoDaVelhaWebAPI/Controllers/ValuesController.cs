using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JogoDaVelhaWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        static List<int> valor = new List<int>(){10, 100, 20, 15, 40, 30, 11};

        // GET api/values
        [HttpGet]
        public List<int> Get()
        {
            return valor;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var resultado = valor.Where(v => v == id).ToList();
            return (resultado.Count() > 0) ? id.ToString() : $"O valor {id} não existe";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var resultado = valor.Where(v => v == int.Parse(value)).ToList();
            if (resultado.Count() == 0)
            {
                valor.Add(int.Parse(value));
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            var resultado = valor.Where(v => v == int.Parse(value)).ToList();
            if (id>=0 && id < valor.Count())
            {
                valor[id] = int.Parse(value);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var resultado = valor.Where(v => v == id).ToList();
            if(resultado.Count() > 0){
                foreach(int meuValor in resultado)
                {
                    valor.Remove(meuValor);
                }
            }
        }
    }
}
