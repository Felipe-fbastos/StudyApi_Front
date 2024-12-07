using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EstudosApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudyApi_Front.Models;

namespace StudyApi_Front.Controllers
{
    [Route("[controller]")]
    public class AlunoController : Controller
    {

        public string UriBase = "http://studyapi.somee.com/EstudosAPI/";
        private readonly ILogger<AlunoController> _logger;

        public AlunoController(ILogger<AlunoController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("CadastratarAluno");
        }

        [HttpPost]

        public async Task<IActionResult> LoginAluno(AlunoViewModel aluno)
        {

            try
            {
                /*Como funciona:
                * Você (o cliente): Faz uma solicitação, como pedir informações de um site ou API.
                * Servidor: Responde com os dados ou confirma a ação, como enviar uma página HTML ou dados JSON.
                */

                //Cliente Http é um ferramenta que é usada para se comunicar com servidores.

                //HttpClint cria um clinte que pode fazer requisições para servidores
                HttpClient httpClient = new HttpClient();
                string UriComplementar = "LoginAluno";

                var content = new StringContent(JsonConvert.SerializeObject(aluno));
                
                //Deifine que o tipo da requsição como Json
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                //Envia uma requisição post para a URL iformada nos parentêses e a resposta é armezenada em response
                HttpResponseMessage response = await httpClient.PostAsync(UriBase + UriComplementar, content);

                //Converte a resposta par JSon
                string serialized = await response.Content.ReadAsStringAsync();

                //Se a resposta do da resquisição for OK, execute o bloco abaixo
                if(response.StatusCode == System.Net.HttpStatusCode.OK){

                    TempData["Mensagem"] =
                        string.Format("Aluno {0} foi registrado com sucesso! Faça login para aceessar", aluno.Nome);
                        return View("AlunoExistente");
                }
                
                else{
                    throw new System.Exception(serialized);
                }

            }
            catch (System.Exception ex)
            {

                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}