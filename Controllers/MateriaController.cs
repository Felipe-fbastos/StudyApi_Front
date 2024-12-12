using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudyApi_Front.Models;


[Route("materia")]
public class MateriaController : Controller
{
    public string uriBase = "http://studyapi.somee.com/EstudosAPI/materia/";

    // Ação Create (GET)
    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    // Ação Index (GET)
    [HttpGet("index")]
    public async Task<IActionResult> IndexAsync()
    {
        try
        {
            string uriComplementar = "GetAll";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");

            HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<MateriaViewModel> listaMaterias = JsonConvert.DeserializeObject<List<MateriaViewModel>>(serialized);
                return View(listaMaterias);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    // Ação Details (GET)
    [HttpGet("details/{id}")]
    public async Task<ActionResult> DetailsAsync(int id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MateriaViewModel materia = JsonConvert.DeserializeObject<MateriaViewModel>(serialized);
                return View(materia);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    // Ação Edit (GET)
    [HttpGet("edit/{id}")]
    public async Task<ActionResult> EditAsync(int id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MateriaViewModel materia = JsonConvert.DeserializeObject<MateriaViewModel>(serialized);
                return View(materia);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    // Ação Edit (POST)
    [HttpPost("edit/{id}")]
    public async Task<ActionResult> EditAsync(MateriaViewModel materia)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            var content = new StringContent(JsonConvert.SerializeObject(materia));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PutAsync(uriBase + materia.Id, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] = $"Materia {materia.Nome} atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    // Ação Delete (GET)
    [HttpGet]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] = $"Materia Id {id} removida com sucesso!";
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }

        

    }

    [HttpPost]

        public async Task<ActionResult> CreateAsync(MateriaViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
              
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Materia {0}, salva com sucesso!", p.Nome);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }
}
