using CharityGame_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CharityGame_Management.Pages
{
    public class ExecProcedureModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-Q01DVK6;Database=charity_game;Trusted_Connection=True;TrustServerCertificate=True";

        [BindProperty]
        public int PlayerId { get; set; }

        public List<Player> PlayerList { get; set; }

        private readonly CharityGameContext _context;  // Контекст вашої БД

        // Конструктор для ін'єкції контексту
        public ExecProcedureModel(CharityGameContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            PlayerList = _context.Players.ToList();
        }

        public IActionResult OnPost()
        {

            PlayerList = _context.Players.ToList();
            if (_context.Players.Find(PlayerId) == null)
            {
                return Page();
            }/*
            if (PlayerList == null)
            {
                PlayerList = _context.Players.ToList();
            }*/

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetPlayerCharacterScoresIntoString2", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Параметри збереженої процедури
                    command.Parameters.Add(new SqlParameter("@p_player_id", SqlDbType.Int) { Value = PlayerId });

                    // Параметр для отримання результату
                    SqlParameter resultMessageParam = new SqlParameter("@resultMessage", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(resultMessageParam);

                    // Виконання процедури
                    command.ExecuteNonQuery();

                    // Отримуємо результат
                    string resultMessage = resultMessageParam.Value.ToString();
                    TempData["ProcedureResult"] = resultMessage;

                    TempData.Keep();

                    return Page();
                }
            }
        }
    }
}
