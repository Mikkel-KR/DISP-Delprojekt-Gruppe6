using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class VaerktoejskasseController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string BackendClientName = "backend";
        private readonly string VaerktoejskasseBaseUrl = "api/Vaerktoejskasse";

        public VaerktoejskasseController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Vaerktoejskasse
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync(VaerktoejskasseBaseUrl);

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoejskasser = JsonConvert.DeserializeObject<List<Vaerktoejskasse>>(json);

            return View(vaerktoejskasser);
        }

        // GET: Vaerktoejskasse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejskasseBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoejskasse = JsonConvert.DeserializeObject<Vaerktoejskasse>(json);

            if (vaerktoejskasse == null)
                return NotFound();

            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaerktoejskasse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VTKId,VTKAnskaffet,VTKFabrikat,VTKEjesAf,VTKModel,VTKSerienummer,VTKFarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient(BackendClientName);

                var json = JsonConvert.SerializeObject(vaerktoejskasse);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Create
                var result = await client.PostAsync(VaerktoejskasseBaseUrl, content);

                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejskasseBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoejskasse = JsonConvert.DeserializeObject<Vaerktoejskasse>(json);

            if (vaerktoejskasse == null)
                return NotFound();

            return View(vaerktoejskasse);
        }

        // POST: Vaerktoejskasse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VTKId,VTKAnskaffet,VTKFabrikat,VTKEjesAf,VTKModel,VTKSerienummer,VTKFarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (id != vaerktoejskasse.VTKId)
                return NotFound();

            if (ModelState.IsValid)
            {

                var client = _clientFactory.CreateClient(BackendClientName);

                var json = JsonConvert.SerializeObject(vaerktoejskasse);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Update
                var result = await client.PutAsync(VaerktoejskasseBaseUrl, content);

                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejskasseBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoejskasse = JsonConvert.DeserializeObject<Vaerktoejskasse>(json);

            if (vaerktoejskasse == null)
                return NotFound();

            return View(vaerktoejskasse);
        }

        // POST: Vaerktoejskasse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient(BackendClientName);
            var result = await client.DeleteAsync($"{VaerktoejskasseBaseUrl}/{id}");

            if (!result.IsSuccessStatusCode)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
