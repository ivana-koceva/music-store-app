
using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Web.Models.Domain 
{
    public class PartnerOrder : BaseEntity
    {
        public string? OwnerId { get; set; }
        public IntegratedSystemsUser? Owner { get; set; }
        public ICollection<CardsInOrder>? CardsInOrders { get; set; }
    }
}
