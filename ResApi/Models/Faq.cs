using System;
using System.Collections.Generic;
using ResApi.Models.Shared;

#nullable disable

namespace ResApi.Models
{
    public partial class Faq : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid FaqTypeId { get; set; }

        public virtual Faqtype FaqType { get; set; }
    }
}
