using PhoneBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Interfaces;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactData _context;

        public ContactsController(IContactData context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод-GET, получает все контакты из webApi, есть возможность фильрации по фамилии
        /// </summary>
        /// <param name="searchString">строка для параметр фильта по фамилии</param>
        /// <returns>IActionResult</returns>
        public IActionResult Index(string searchString)
        {
            try
            {
                var contactFiltr = _context.GetAllContact();

                if (!String.IsNullOrEmpty(searchString))
                {
                    contactFiltr = contactFiltr.Where(s => s.LastName!.Contains(searchString));
                }
                return View(contactFiltr);
            }
            catch (Exception ex)  { return Problem(ex.Message); }
        }

        /// <summary>
        /// Метод-GET для отображени деталей контакта
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public IActionResult Details(int? id)
        {
            Contact contact = _context.GetContact(id);

            if (id == null || contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        /// <summary>
        /// Метод-GET для прехода на страницу с формой для нового контакта
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Create() => View();
 
        /// <summary>
        /// Метод-POST для валидации данных и добавления контакта в базу данных
        /// В случае ошибок в модели - вернет ту же страницу
        /// </summary>
        /// <param name="contact">модель данных</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,MiddleName,LastName,Telefon,Address,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.CreateContact(contact);
                
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
            Contact contact = _context.GetContact(id);

            if (id == null || contact == null)
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
                    _context.UpdateContact(id, contact);
  
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new Exception("это уже совсем другая история");
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
            Contact contact = _context.GetContact(id);

            if (id == null || contact == null)
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
            _context.DeleteContact(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Метод-GET для отбражения страницы с информацией о приложении
        /// </summary>
        /// <returns></returns>
        public IActionResult About() => View();
    }
}
