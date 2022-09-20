using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Helsingborg.Models;
using System.Security.Principal;

//Ganska straight forward tycker jag, behövs inte kommenteras direkt..

namespace Helsingborg.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    private readonly ILogger<HomeController> _logger;

    public DataCustomer dataCustomer;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        //return View();
        return View(await _context.DataCust.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DataCustomer model)
    {
        if (ModelState.IsValid)
        {

            dataCustomer = new DataCustomer
            {
                Date = model.Date,
                PlaceOfPurchase = model.PlaceOfPurchase,
                AmountIncludingVAT = model.AmountIncludingVAT,
                VAT = model.VAT,
                Reason = model.Reason,
                Members = model.Members,
                Comment = model.Comment,
            };

            _context.Add(dataCustomer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(dataCustomer);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var datacus = await _context.DataCust.FindAsync(id);

        if (datacus == null)
        {
            return NotFound();
        }

        return View(datacus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Date,PlaceOfPurchase,AmountIncludingVAT,VAT,Reason,Members,Comment")] DataCustomer datacus)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(datacus);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(datacus.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(datacus);
    }

    private bool DataExists(int id)
    {
        return _context.DataCust.Any(e => e.Id == id);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var datacus = await _context.DataCust
            .FirstOrDefaultAsync(m => m.Id == id);
        if (datacus == null)
        {
            return NotFound();
        }

        return View(datacus);

    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var datacus = await _context.DataCust.FindAsync(id);

        if (datacus is not null)
        {
            _context.DataCust.Remove(datacus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return NotFound();
        }
    }
}

