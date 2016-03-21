using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class NodeHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual SessionNodeSuggestions Node { get; set; }
        public int SessionNodeSuggestionsId { get; set; }

        public string Value { get; set; }

        public virtual ApplicationUser ByUser { get; set; }
        public string ApplicationUserId { get; set; }

        public int? SuggestionId { get; set; }
    }


}
