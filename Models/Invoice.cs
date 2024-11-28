using System;
using System.Collections.Generic;

namespace TestPrj2.Models;

public partial class Invoice
{
    public string InvoiceId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
