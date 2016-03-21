using System;
using System.Collections.Generic;

namespace ServiceLayer.Models.KnowledgeSession
{
    public class NodeHistoryViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public NodeViewModel Node { get; set; }
        public UserViewModel ByUser { get; set; }
        public List<SuggestionViewModel> Suggestions { get; set; }
    }
}