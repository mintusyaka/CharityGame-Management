using CharityGame_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CharityGame_Management.Pages
{
    public class AddNewRowModel : PageModel
    {
        private readonly CharityGameContext _context;  // Контекст вашої БД

        // Конструктор для ін'єкції контексту
        public AddNewRowModel(CharityGameContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Character NewCharacter { get; set; }

        // Метод для обробки POST запиту
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(_context.Characters.Find(NewCharacter.CharacterId) != null)
            {
                return Page();
            }

            // Додаємо нового персонажа в контекст
            _context.Characters.Add(NewCharacter);

            // Зберігаємо зміни в базі даних
            await _context.SaveChangesAsync();

            // Перенаправляємо на список персонажів після додавання
            return RedirectToPage("/Index"); // /Index.cshtml - ваша сторінка для перегляду списку персонажів
        }
    }
}
