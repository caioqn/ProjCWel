using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using asdf_api.DapperExtensions;
using asdf_api.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asdf_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComMorteController : ControllerBase
    {
        // GET api/<ComMorteController>/GetLimited
        [HttpGet("GetLimited")]
        public IEnumerable<dynamic> Get([FromServices] MySqlConnection connection)
        {
            var asdf = connection.Query(() => new
            {
                com_morte_ani_pk = default(int),
                com_morte_ani_nome = default(string),
                com_morte_ani_dt_morte = default(DateTime),
            }, @"SELECT cma.com_morte_ani_pk,
                            cma.com_morte_ani_nome,
                            cma.com_morte_ani_dt_morte 
                            FROM com_morte_ani cma limit 10").ToArray();
            return asdf;
        }
        // GET api/<ComMorteController>/ByDate/aniData1&aniData2
        [HttpGet("ByDate/{aniData1}&{aniData2}")]
        public IEnumerable<string?> Get(DateTime aniData1, DateTime aniData2, [FromServices] MySqlConnection connection)
        {
            var asdf = connection
                .Query<ComMorte>(@"SELECT 
                                   cma.com_morte_ani_nome
                                   FROM com_morte_ani cma
                                   WHERE cma.com_morte_ani_dt_morte  BETWEEN @aniData1 AND @aniData2",
                                        new { aniData1, aniData2 });
            return asdf.Select(x => x.com_morte_ani_nome).ToArray();
        }

        // GET api/<ComMorteController>/ByName/ComMorteAniNome
        [HttpGet("ByName/{comMorteAniNome}")]
        public IEnumerable<ComMorte> Get(string comMorteAniNome, [FromServices] MySqlConnection connection)
        {
            var encodedSearch = $"%{comMorteAniNome}%";

            return connection
                .Query<ComMorte>(@"SELECT 
                                        cma.com_morte_ani_pk,
                                        cma.com_morte_ani_fk,
                                        cma.com_morte_criador_fk,
                                        cma.com_morte_ani_nome, 
                                        cma.com_morte_tipo_fk,
                                        cma.com_morte_ani_causa_morte,
                                        cma.com_morte_protocolo,
                                        cma.com_morte_ani_dt_morte,
                                        cma.com_morte_ani_dt_com,
                                        cma.com_morte_ani_ts
                                        FROM com_morte_ani cma
                                        WHERE cma.com_morte_ani_nome like @encodedSearch",
                                        new { encodedSearch }).ToArray();
        }

        // POST api/<ComMorteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ComMorteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComMorteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
