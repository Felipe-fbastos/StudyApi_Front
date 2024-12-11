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
using System.Net.Http.Headers;
using StudyApi_Front.Models;

namespace StudyApi_Front.Controllers
{
    [Route("[controller]")]
    public class AlunoController : Controller
    {
        // URL base da API
        public string UriBase = "http://studyapi.somee.com/EstudosAPI/";

        // Exibe o formulário de cadastro de aluno
        [HttpGet("CadastrarAluno")]
        public IActionResult Index()
        {
            // Especifica o caminho completo para a view que está em /Views/Aluno/CadastrarAluno.cshtml
            return View("CadastrarAluno");
        }

        // Exibe o formulário de login de aluno
        [HttpGet("AutenticarAluno")]
        public ActionResult IndexLogin()
        {
            return View("AutenticarAluno");
        }

        // Método para registrar o aluno (POST)
        [HttpPost("SignUp")]
        public async Task<IActionResult> RegistrarAsync(AlunoViewModel u)
        {
            try
            {
                // Cria o cliente HTTP para enviar a requisição
                HttpClient httpClient = new HttpClient();
                string uriComplementar = "SignUp";

                // Serializa o objeto do aluno para JSON e define o tipo de conteúdo como "application/json"
                var content = new StringContent(JsonConvert.SerializeObject(u));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Envia a requisição POST para a API
                HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

                // Lê a resposta da API
                string serialized = await response.Content.ReadAsStringAsync();

                // Se o status da resposta for OK, exibe uma mensagem de sucesso e redireciona para a tela de login
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Usuario {0} Registrado com sucesso! Faça o login para acessar.", u.Nome);
                    return View("AutenticarAluno");  // Corrigido para 'AutenticarAluno' em vez de 'AutenticarUsuario'
                }
                else
                {
                    // Caso contrário, lança uma exceção com a mensagem de erro
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                // Caso ocorra um erro, exibe a mensagem de erro e redireciona para a página de cadastro
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Método para autenticar o aluno (POST)
        [HttpPost("AutenticarAluno")]
        public async Task<IActionResult> AutenticarAsync(AlunoViewModel u)
        {
            try
            {
                // Cria o cliente HTTP para enviar a requisição
                HttpClient httpClient = new HttpClient();
                string uriComplementar = "Login";

                // Serializa o objeto do aluno para JSON e define o tipo de conteúdo como "application/json"
                var content = new StringContent(JsonConvert.SerializeObject(u));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Envia a requisição POST para a API
                HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

                // Lê a resposta da API
                string serialized = await response.Content.ReadAsStringAsync();

                // Se o status da resposta for OK, deserializa o aluno e exibe uma mensagem de boas-vindas
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AlunoViewModel uLogado = JsonConvert.DeserializeObject<AlunoViewModel>(serialized);
                    TempData["Mensagem"] = string.Format("Bem-vindo {0}!!!", uLogado.Nome);
                    // Redireciona para a página de personagens após autenticação bem-sucedida
                    return RedirectToAction("Index", "Personagens");
                }
                else
                {
                    // Caso contrário, lança uma exceção com a mensagem de erro
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                // Caso ocorra um erro, exibe a mensagem de erro e redireciona para a página de login
                TempData["MensagemErro"] = ex.Message;
                return IndexLogin();
            }
        }

        // Método para login do aluno (POST)
        [HttpPost("LoginAluno")]
        public async Task<IActionResult> LoginAluno(AlunoViewModel aluno)
        {
            try
            {
                /*Como funciona:
                * Você (o cliente): Faz uma solicitação, como pedir informações de um site ou API.
                * Servidor: Responde com os dados ou confirma a ação, como enviar uma página HTML ou dados JSON.
                */

                // Cliente Http é uma ferramenta usada para se comunicar com servidores.
                // HttpClient cria um cliente que pode fazer requisições para servidores
                HttpClient httpClient = new HttpClient();
                string UriComplementar = "LoginAluno";

                // Serializa o objeto do aluno para JSON e define o tipo de conteúdo como "application/json"
                var content = new StringContent(JsonConvert.SerializeObject(aluno));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Envia a requisição POST para a API
                HttpResponseMessage response = await httpClient.PostAsync(UriBase + UriComplementar, content);

                // Lê a resposta da API
                string serialized = await response.Content.ReadAsStringAsync();

                // Se o status da resposta for OK, exibe uma mensagem de sucesso
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Aluno {0} foi registrado com sucesso! Faça login para acessar", aluno.Nome);
                    // Redireciona para a página de confirmação de aluno existente
                    return View("AlunoExistente");
                }
                else
                {
                    // Caso contrário, lança uma exceção com a mensagem de erro
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                // Caso ocorra um erro, exibe a mensagem de erro e redireciona para a página de cadastro
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
