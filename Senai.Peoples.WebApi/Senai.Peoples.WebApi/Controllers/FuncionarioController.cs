using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Dominios;
using Senai.Peoples.WebApi.Interface;
using Senai.Peoples.WebApi.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private IFuncionariosRepository _funcionarioRepository { get; set; }

        public FuncionarioController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<FuncionarioDomain> listaFuncionarios = _funcionarioRepository.Listar();

            return Ok(listaFuncionarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if(funcionarioBuscado == null)
            {
                return NotFound("Achei nada n");
            }

            return Ok(funcionarioBuscado);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FuncionarioDomain funcionarioAtualizado)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if(funcionarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionário não encontrado!",
                            erro = true
                        }
                    ) ;
            }

            try
            {
                _funcionarioRepository.Atualizar(id, funcionarioAtualizado);

                return NoContent();
            }

            catch(Exception codErro)
            {
                return BadRequest(codErro);
            }
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            _funcionarioRepository.Inserir(novoFuncionario);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _funcionarioRepository.Deletar(id);

            return StatusCode(204);
        }
    }
}
