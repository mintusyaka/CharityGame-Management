using CharityGame_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CharityGame_Management.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

        /*public List<Character> Characters { get; set; }*/

		public CharityGameContext _context;

        public int? MinStrength { get; set; }
        public int? MaxStrength { get; set; }
        public int? MinSpeed { get; set; }
        public int? MaxSpeed { get; set; }
        public int? Sorting { get; set; }

        string sqlQuery;

        public PaginatedList<Character> Characters { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public IndexModel(ILogger<IndexModel> logger, CharityGameContext context)
		{
			_logger = logger;
			_context = context;
		}

		public void OnGet()
		{
            MinStrength = TempData["MinStrength"] as int?;
            MaxStrength = TempData["MaxStrength"] as int?;
            MinSpeed = TempData["MinSpeed"] as int?;
            MaxSpeed = TempData["MaxSpeed"] as int?;
            Sorting = TempData["Sorting"] as int?;

            var charactersQuery = _context.Characters.AsQueryable();

            if(MinStrength == null && MaxStrength == null && MinSpeed == null && MaxSpeed == null)
            {
                /*Characters = _context.Characters.ToList();*/
                Characters = PaginatedList<Character>.Create(charactersQuery, PageNumber, PageSize);
            }
            else
            {
                updateSqlQuery();

                sqlQuery += " WHERE strength > " + MinStrength
                                   + " AND strength < " + MaxStrength
                                   + " AND speed > " + MinSpeed
                                   + " AND speed < " + MaxSpeed;

                /*Characters = _context.Characters.FromSqlRaw(sqlQuery).ToList();

                if (Sorting > 0)
                {
                    Characters = Characters.OrderBy(c => c.Name).ToList();
                }
                else if (Sorting < 0)
                {
                    Characters = Characters.OrderByDescending(c => c.Name).ToList();
                }*/

                charactersQuery = _context.Characters.FromSqlRaw(sqlQuery).AsQueryable();

                if (Sorting > 0)
                {
                    charactersQuery = charactersQuery.OrderBy(c => c.Name).AsQueryable();
                }
                else if (Sorting < 0)
                {
                    charactersQuery = charactersQuery.OrderByDescending(c => c.Name).AsQueryable();
                }
                Characters = PaginatedList<Character>.Create(charactersQuery, PageNumber, PageSize);
                TempData.Keep();


            }
        }


        private void updateSqlQuery()
        {
            sqlQuery = "SELECT character_id, name, description, strength, speed FROM character";
        }
    }

    public class FilterModel
    {
        public int MinStrength { get; set; }
        public int MaxStrength { get; set; }
        public int MinSpeed { get; set; }
        public int MaxSpeed { get; set; }
    }
}
