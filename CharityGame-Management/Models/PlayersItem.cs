﻿using System;
using System.Collections.Generic;

namespace CharityGame_Management.Models;

public partial class PlayersItem
{
    public int PlayerId { get; set; }

    public int ItemId { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
