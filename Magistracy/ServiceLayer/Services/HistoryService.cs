//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.UI;
//using AutoMapper;
//using DataLayer.Interfaces;
//using DataLayer.Models;
//using ServiceLayer.Interfaces;
//using ServiceLayer.Models;
//using ServiceLayer.Models.KnowledgeSession;

//namespace ServiceLayer.Services
//{
//    public class HistoryService : IHistoryService
//    {
//        private readonly IUnitOfWork _db;

//        public HistoryService(IUnitOfWork db)
//        {
//            _db = db;
//        }

//        public bool AddRecord(int sesionId, int nodeId, string name, string userId, int? suggestionId)
//        {
//            var session = _db.KnowledgeSessions.Get(sesionId);

//            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
//            var user = _db.Users.Get(userId);
//            if (node == null) return false;

//            var suggestion = node.Suggestions.FirstOrDefault(m => m.Id == suggestionId);

//            _db.NodesHistory.Create(new NodeHistory
//            {
//                ByUser = user,
//                Date = DateTime.Now,
//                Node = node,
//                Value = name,
//                SuggestionId = suggestion == null ? (int?)null : suggestion.Id
//            });

//            try
//            {
//                _db.Save();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public void UpdateHistoryWithWinner(int sessionId, int level, string winnerId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            var suggesttedNodes = session.NodesSuggestions.Where(m => m.SuggestedBy == winnerId && m.Level == level).ToList();
//            foreach (var node in suggesttedNodes)
//            {
//                AddRecord(sessionId, node.Id, node.Name, winnerId, null);
//            }
//            _db.Save();
//        }

//        public List<NodeHistoryViewModel> Get(int sessionId, int nodeId)
//        {
//            var session = _db.KnowledgeSessions.Get(sessionId);
//            var node = session.NodesSuggestions.FirstOrDefault(m => m.Id == nodeId);
//            if (node == null) return null;
//            var result = new List<NodeHistoryViewModel>();
//            var orderedSuggestions = node.Suggestions.OrderBy(m => m.SuggestionDate).ToList();

//            foreach (var record in node.History)
//            {
//                var historyModel = new NodeHistoryViewModel
//                 {
//                     ByUser = Mapper.Map<ApplicationUser, UserViewModel>(record.ByUser),
//                     Date = record.Date,
//                     Value = record.Value,
//                     Node = Mapper.Map<SessionNodes,NodeViewModel>(node)
//                 };

//                if (record.SuggestionId != null)
//                {
//                    var suggestions = orderedSuggestions.TakeWhile(m => m.Id != record.SuggestionId).ToList();
//                    suggestions.Add(orderedSuggestions.FirstOrDefault(m => m.Id == record.SuggestionId));
//                    foreach (var remove in suggestions)
//                    {
//                        orderedSuggestions.Remove(remove);
//                    }

//                    historyModel.Suggestions = new List<SuggestionViewModel>();
//                    historyModel.Suggestions.AddRange(
//                        Mapper.Map<List<Suggestion>, List<SuggestionViewModel>>(suggestions.OrderByDescending(m=>m.SuggestionDate).ToList()));


//                }

//                result.Add(historyModel);
//            }


//            //if (node == null) return null;

//            //foreach (var suggestion in node.Suggestions)
//            //{

//            //}
//            return result;

//        }
//    }
//}
