using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain
{
    public class StripeSettings
    {
        public string? PublishableKey { get; set; }
        public string? SecretKey { get; set; }
        public string? Domain { get; set; } 
    }
}
