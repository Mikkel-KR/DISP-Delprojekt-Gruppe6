using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

namespace Frontend.Controllers
{
    public class VaerktoejController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string BackendClientName = "backend";
        private readonly string VaerktoejBaseUrl = "api/Vaerktoej";

        public VaerktoejController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Vaerktoej
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync(VaerktoejBaseUrl);

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoejList = JsonSerializer.Deserialize<List<Vaerktoej>>(json);

            return View(vaerktoejList);
        }

        // GET: Vaerktoej/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoej = JsonSerializer.Deserialize<Vaerktoej>(json);

            if (vaerktoej == null)
                return NotFound();

            return View(vaerktoej);
        }

        // GET: Vaerktoej/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaerktoej/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VTId,VTAnskaffet,VTFabrikat,VTModel,VTSerienr,VTType,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient(BackendClientName);

                var json = JsonSerializer.Serialize(vaerktoej);
                var content = new StringContent(json);

                //Create
                var result = await client.PostAsync(VaerktoejBaseUrl, content);

                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(vaerktoej);
        }

        // GET: Vaerktoej/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoej = JsonSerializer.Deserialize<Vaerktoej>(json);

            if (vaerktoej == null)
                return NotFound();

            return View(vaerktoej);
        }

        // POST: Vaerktoej/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VTId,VTAnskaffet,VTFabrikat,VTModel,VTSerienr,VTType,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (id != vaerktoej.VTId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient(BackendClientName);

                var json = JsonSerializer.Serialize(vaerktoej);
                var content = new StringContent(json);

                //Update
                var result = await client.PutAsync(VaerktoejBaseUrl, content);

                if (result.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(vaerktoej);
        }

        // GET: Vaerktoej/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return NotFound();

            var client = _clientFactory.CreateClient(BackendClientName);
            var response = await client.GetAsync($"{VaerktoejBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var vaerktoej = JsonSerializer.Deserialize<Vaerktoej>(json);

            if (vaerktoej == null)
                return NotFound();

            return View(vaerktoej);
        }

        // POST: Vaerktoej/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var client = _clientFactory.CreateClient(BackendClientName);
            var result = await client.DeleteAsync($"{VaerktoejBaseUrl}/{id}");

            if (!result.IsSuccessStatusCode)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
