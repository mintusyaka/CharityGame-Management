using CharityGame_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CharityGame_Management.Pages
{
    public class AddNewRowModel : PageModel
    {
        private readonly CharityGameContext _context;  // �������� ���� ��

        // ����������� ��� ��'����� ���������
        public AddNewRowModel(CharityGameContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Character NewCharacter { get; set; }

        // ����� ��� ������� POST ������
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

            // ������ ������ ��������� � ��������
            _context.Characters.Add(NewCharacter);

            // �������� ���� � ��� �����
            await _context.SaveChangesAsync();

            // ��������������� �� ������ ��������� ���� ���������
            return RedirectToPage("/Index"); // /Index.cshtml - ���� ������� ��� ��������� ������ ���������
        }
    }
}
