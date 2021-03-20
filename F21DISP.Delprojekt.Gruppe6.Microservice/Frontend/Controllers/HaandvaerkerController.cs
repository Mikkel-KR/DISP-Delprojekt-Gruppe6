using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Controllers
{
    public class HaandvaerkerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _client;
        private readonly string BackendClientName = "backend";
        private readonly string HaandvaerkerBaseUrl = "api/Haandvaerker";

        public HaandvaerkerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient(BackendClientName);
        }

        // GET: Haandvaerker
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(HaandvaerkerBaseUrl);

            if(!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var haandvaerkers = JsonConvert.DeserializeObject<List<Haandvaerker>>(json);

            return View(haandvaerkers);
        }

        // GET: Haandvaerker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _client.GetAsync($"{HaandvaerkerBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(json);

            if (haandvaerker == null)
                return NotFound();

            return View(haandvaerker);
        }

        // GET: Haandvaerker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Haandvaerker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(haandvaerker);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Create
                var result = await _client.PostAsync(HaandvaerkerBaseUrl, content);

                if(result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: Haandvaerker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _client.GetAsync($"{HaandvaerkerBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(json);

            if (haandvaerker == null)
                return NotFound();

            return View(haandvaerker);
        }

        // POST: Haandvaerker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(haandvaerker);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //Update
                var result = await _client.PutAsync($"{HaandvaerkerBaseUrl}/{id}", content);

                if(result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: Haandvaerker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _client.GetAsync($"{HaandvaerkerBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var haandvaerker = JsonConvert.DeserializeObject<Haandvaerker>(json);

            if (haandvaerker == null)
                return NotFound();

            return View(haandvaerker);
        }

        // POST: Haandvaerker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _client.DeleteAsync($"{HaandvaerkerBaseUrl}/{id}");

            if (!result.IsSuccessStatusCode)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
