using System;
using System.Collections.Generic;

namespace TestPrj2.Models;

public partial class InvoiceDetail
{
    public string InvoiceDetailId { get; set; } = null!;

    public string? InvoiceId { get; set; }

    public string? ProductId { get; set; }

    public int Quantity { get; set; }

    public double TotalPrice { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
