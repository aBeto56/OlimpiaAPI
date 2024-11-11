using System;
using System.Collections.Generic;

namespace OlimpiaAPI.Models;

public partial class Player
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int Weight { get; set; }

    public int Height { get; set; }

    public DateTime CreatedTime { get; set; }
}
