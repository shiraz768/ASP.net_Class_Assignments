using System;
using System.Collections.Generic;

namespace Shiraz.models;

public partial class PermanentCustomer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
