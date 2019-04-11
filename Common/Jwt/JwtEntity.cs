using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Jwt
{
    public class JwtEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public long Exp { get; set; }
        public string Source { get; set; }
        public long Nbf { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
