using System.Collections.Generic;

namespace RealesApi.DTO.Kinde
{
    public class KindeUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
        public List<string> Scopes { get; set; } = new List<string>();
    }
}

