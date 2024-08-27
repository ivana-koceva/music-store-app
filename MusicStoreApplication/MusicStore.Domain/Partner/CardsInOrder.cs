using MusicStore.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Web.Models.Domain
{
    public class CardsInOrder : BaseEntity
    {
        public Guid CardId { get; set; }
        public Card? OrderedCard { get; set; }
        public Guid OrderId { get; set; }
        public PartnerOrder? Order { get; set; }
        public int Quantity { get; set; }
    }
}
