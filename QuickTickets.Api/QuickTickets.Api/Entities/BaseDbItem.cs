using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTickets.Api.Entities
{
    public abstract class BaseDbItem
    {
        [Key]
        public Guid Id { get; set; }
    }
}
