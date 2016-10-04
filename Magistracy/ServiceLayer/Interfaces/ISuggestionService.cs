﻿using System.Collections.Generic;
using DataLayer.Models;
using ServiceLayer.Models;
using ServiceLayer.Models.KnowledgeSession;

namespace ServiceLayer.Interfaces
{
    public interface ISuggestionService
    {
        //bool MakeNodeSuggestion(NodeSuggestionViewModel nodeSuggestionViewModel);
        //void UpdateNodeWithSuggestions(int sessionId, int? level, NodeViewModel winnerNode);
        //bool AddComment(int sessionId, string comment, int nodeView, string getUserId);
        //List<CommentViewModel> GetComments(int sessionId, int nodeId);
        SuggestionSessionUserViewModel GetNodeStructureSuggestionWinner(int nodeId);
        bool CheckStructureSuggestionVoteDone(int sessionId, int nodeId, NodeStructureVoteTypes voteType);
        int? CheckUserStructureSuggestionVote(string userId, int nodeId);
        List<SuggestionSessionUserViewModel> GetSuggestions(int sessionId, int nodeId);
        int CreateSuggestion(string userId, int parentId);
        void VoteNodeStructureSuggestion(NodeStructureSuggestionVoteViewModel suggestion);
    }
}