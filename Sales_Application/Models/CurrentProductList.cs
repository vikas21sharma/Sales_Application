using System;
using System.Collections.Generic;

namespace Sales_Application.Models;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
