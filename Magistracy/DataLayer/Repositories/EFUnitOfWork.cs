using System;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private KnowledgeSessionRepository _knowledgeSessionRepository;
        private NodesRepository _nodesRepository;
        private UserRepository _userRepository;
        private NodeStructureVotesRepository _nodeStructureVotesRepository;
        private NodeHistoryRepository _nodeHistoryRepository;
        private NodeStructureSuggestionRepository _nodeStructureSuggestion;
        private NodeModificationsRepository _nodeModificationsRepository;
        private NodeModificationVotesRepository _nodeModificationVotesRepository;
        private CommentsRepository _commentsRepository;
        private TextMergeSuggestionRepository _textMergeSuggestionRepository;
        private TextMergeSuggestionVoteRepository _textMergeSuggestionVoteRepository;
        private NodeResourceRepository _nodeResourceRepository;
        private ResourceClusterRepository _resourceClusterRepository;
        private bool _disposed = false;

        public IRepository<ResourceCluster> ResourceClusters
        {
            get
            {
                return _resourceClusterRepository ??
                     (_resourceClusterRepository = new ResourceClusterRepository(_db));
            }
        }

        public IRepository<TextMergeSuggestion> TextMergeSuggestions
        {
            get
            {
                return _textMergeSuggestionRepository ??
                     (_textMergeSuggestionRepository = new TextMergeSuggestionRepository(_db));
            }
        }

        public IRepository<TextMergeSuggestionVote> TextMergeSuggestionVotes
        {
            get
            {
                return _textMergeSuggestionVoteRepository ??
                     (_textMergeSuggestionVoteRepository = new TextMergeSuggestionVoteRepository(_db));
            }
        }




        public IRepository<NodeResource> NodeResources
        {
            get
            {
                return _nodeResourceRepository ??
                     (_nodeResourceRepository = new NodeResourceRepository(_db));
            }
        }


        public IRepository<NodeModification> NodeModifications
        {
            get
            {
                return _nodeModificationsRepository ??
                     (_nodeModificationsRepository = new NodeModificationsRepository(_db));
            }
        }

        public IRepository<NodeModificationVote> NodeModificationVotes
        {
            get
            {
                return _nodeModificationVotesRepository ??
                                     (_nodeModificationVotesRepository = new NodeModificationVotesRepository(_db));
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return _commentsRepository ??
                                    (_commentsRepository = new CommentsRepository(_db));
            }
        }

        public IRepository<SessionNode> Nodes
        {
            get
            {
                return _nodesRepository ??
                    (_nodesRepository = new NodesRepository(_db));
            }
        }


        public IRepository<NodeStructureSuggestion> NodeStructureSuggestions
        {
            get
            {
                return _nodeStructureSuggestion ??
                    (_nodeStructureSuggestion = new NodeStructureSuggestionRepository(_db));
            }
        }



        public IRepository<NodeHistory> NodesHistory
        {
            get
            {
                return _nodeHistoryRepository ??
                    (_nodeHistoryRepository = new NodeHistoryRepository(_db));
            }
        }

        public IRepository<KnowledgeSession> KnowledgeSessions
        {
            get
            {
                return _knowledgeSessionRepository ??
                    (_knowledgeSessionRepository = new KnowledgeSessionRepository(_db));
            }
        }

        public IRepository<NodeStructureSuggestionVote> NodeStructureSuggestionsVotes
        {
            get
            {
                return _nodeStructureVotesRepository ??
                    (_nodeStructureVotesRepository = new NodeStructureVotesRepository(_db));
            }
        }



        public ExtendedRepository<ApplicationUser> Users
        {
            get
            {
                return _userRepository ??
                       (_userRepository = new UserRepository(_db));
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


}
