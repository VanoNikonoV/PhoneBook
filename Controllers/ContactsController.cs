using PhoneBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using FluentValidation;
using System;
using FluentValidation.Results;
using FluentValidation.AspNetCore;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactData _context;
        private IValidator<Contact> _validator;

        public ContactsController(IContactData context, IValidator<Contact> validator)
        {
            _context = context;
            _validator = validator;
        }

        /// <summary>
        /// Метод-GET, получает все контакты из webApi, есть возможность фильрации по фамилии
        /// </summary>
        /// <param name="searchString">строка для параметр фильта по фамилии</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                IEnumerable<IContact> contactFiltr = await _context.GetAllContact();

                if (!String.IsNullOrEmpty(searchString))
                {
                    contactFiltr = contactFiltr.Where(s => s.LastName!.Contains(searchString));
                }
                return View(contactFiltr);
            }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        /// <summary>
        /// Метод-GET для отображени деталей контакта
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Details(int? id)
        {
            var tuple = await _context.GetContact(id);
            IContact contact = tuple.Item1;
            HttpStatusCode statusCode = tuple.Item2;

            if (statusCode == HttpStatusCode.OK) { return View(contact); }
            if (contact == null || statusCode == HttpStatusCode.NotFound) { return NotFound(); }
            if (statusCode == HttpStatusCode.Unauthorized) { return RedirectToAction(nameof(NotAuthentication)); }
            return RedirectToAction(nameof(Index));

            //IContact contact =await _context.GetContact(id);

            //if (id == null || contact == null)
            //{
            //    return NotFound();
            //}
            //return View(contact);
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
        public async Task<IActionResult> Create(Contact contact)
        {
            ValidationResult result = await _validator.ValidateAsync(contact);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View(contact);
            }
            else
            {
                HttpStatusCode statusCode = await _context.CreateContact(contact);

                if (statusCode == HttpStatusCode.OK) { return RedirectToAction(nameof(Index)); }
                if (contact == null || statusCode == HttpStatusCode.NotFound) { return NotFound(); }
                if (statusCode == HttpStatusCode.Unauthorized) { return RedirectToAction(nameof(NotAuthentication)); }

                return RedirectToAction(nameof(Index));
            }
            
        }

        /// <summary>
        /// Метод-GET для редактирования данных
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            var tuple = await _context.GetContact(id);
            IContact contact = tuple.Item1;
            HttpStatusCode statusCode = tuple.Item2;

            if (statusCode == HttpStatusCode.OK) { return View(contact);}
            if (contact == null || statusCode == HttpStatusCode.NotFound) { return NotFound(); }
            if (statusCode == HttpStatusCode.Unauthorized) {  return RedirectToAction(nameof(NotAuthentication)); }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Метод-POST для валидации данных, исключения паралеллизма и добавления обнавленных данных в базу данных
        /// </summary>
        /// <param name="id">параметр для поиска контакта</param>
        /// <param name="contact">модель данных</param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
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
                    var tuple = await _context.UpdateContact(id, contact);
                    IContact returnContact = tuple.Item1;
                    HttpStatusCode statusCode = tuple.Item2;

                    if (statusCode == HttpStatusCode.OK) { return View(returnContact); }
                    if (contact == null || statusCode == HttpStatusCode.NotFound) { return NotFound(); }
                    if (statusCode == HttpStatusCode.Unauthorized) { return RedirectToAction(nameof(NotAuthentication)); }
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new Exception("это уже совсем другая история");
                }
               
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
            var tuple = await _context.GetContact(id);
            IContact contact = tuple.Item1;
            HttpStatusCode statusCode = tuple.Item2;

            if (contact == null || statusCode == HttpStatusCode.NotFound) { return NotFound(); }
            if (statusCode == HttpStatusCode.OK) { return View(contact); }
            if (statusCode == HttpStatusCode.Unauthorized) { return RedirectToAction(nameof(NotAuthentication)); }
            return RedirectToAction(nameof(DeleteConfirmed));
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
            HttpStatusCode returnHttpStatusCode = await _context.DeleteContact(id);

            if (returnHttpStatusCode == HttpStatusCode.OK) { return RedirectToAction(nameof(Index)); }

            if (returnHttpStatusCode == HttpStatusCode.Unauthorized 
                || returnHttpStatusCode == HttpStatusCode.Forbidden) 
                    { return RedirectToAction(nameof(NotAuthentication)); }

            else return RedirectToAction(nameof(Index));


        }

        /// <summary>
        /// Метод-GET для отбражения страницы с информацией о приложении
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult About() => View();

        public IActionResult NotAuthentication() => View();
    }
}
