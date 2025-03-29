using System;
using System.Collections.Generic;
using ResApi.Models.Shared;

#nullable disable

namespace ResApi.Models
{
    public partial class Faqtype : BaseEntity
    {
        public Faqtype()
        {
            Faqs = new HashSet<Faq>();
        }

        public string TypeName { get; set; }

        public virtual ICollection<Faq> Faqs { get; set; }
    }
}
