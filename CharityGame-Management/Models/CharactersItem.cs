using System;
using System.Collections.Generic;

namespace CharityGame_Management.Models;

public partial class CharactersItem
{
    public int CharacterId { get; set; }

    public int ItemId { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
