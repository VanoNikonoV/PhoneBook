using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly PhoneBookContext _context;

        public ContactsController(PhoneBookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод-GET, отображает все контакты в усеченном виде (страница по умолчанию см. Program)
        /// </summary>
        /// <param name="searchString">строка для параметр фильта по фамилии</param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Contact == null)
            {
                return Problem("Проблема с базой данных!");
            }

            var contactFiltr = from contact in _context.Contact select contact;

            if (!String.IsNullOrEmpty(searchString))
            {
                contactFiltr = contactFiltr.Where(s => s.LastName!.Contains(searchString));
            }
            return View(await contactFiltr.ToListAsync());
        }

        /// <summary>
        /// Метод-GET для отображени деталей контакта
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        /// <summary>
        /// Метод-GET для прехода на страницу с формой для нового контакта
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Метод-POST для валидации данных и добавления контакта в базу данных
        /// В случае ошибок в модели - вернет ту же страницу
        /// </summary>
        /// <param name="contact">модель данных</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,Telefon,Address,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        /// <summary>
        /// Метод-GET для редактирования данных
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        /// <summary>
        /// Метод-POST для валидации данных, исключения паралеллизма и добавления обнавленных данных в базу данных
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <param name="contact">модель данных</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Telefon,Address,Description")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Problem("Этот контакт сейчас кто-то редактирует, попробуйте позже");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        /// <summary>
        /// Метод-GET для отбражения страницы с данными выбранного контакта
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        /// <summary>
        /// Метод-POST для удаления контакта из базы данных
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contact == null)
            {
                return Problem("Проблема с базой данных!");
            }
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Метод-GET для отбражения страницы с информацией о приложении
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            return View();
        }

        private bool ContactExists(int id)
        {
          return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
