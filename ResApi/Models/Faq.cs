using System;
using System.Collections.Generic;
using RealesApi.Models.Shared;

#nullable disable

namespace RealesApi.Models
{
    public partial class Faq : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid FaqTypeId { get; set; }

        public virtual Faqtype FaqType { get; set; }
    }
}
