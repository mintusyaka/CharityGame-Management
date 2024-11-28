using CharityGame_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CharityGame_Management.Pages
{
    public class UpdateRowModel : PageModel
    {
        private readonly CharityGameContext _context;  // Контекст вашої БД

        // Конструктор для ін'єкції контексту
        public UpdateRowModel(CharityGameContext context)
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

            var existingCharacter = _context.Characters.Find(NewCharacter.CharacterId);

            if (existingCharacter == null)
            {
                return Page();
            }

            existingCharacter.Name = NewCharacter.Name;
            existingCharacter.Description = NewCharacter.Description;
            existingCharacter.Strength = NewCharacter.Strength;
            existingCharacter.Speed = NewCharacter.Speed;
            
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
