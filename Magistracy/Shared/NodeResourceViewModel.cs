using System;

namespace Shared
{
    public class NodeResourceViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NodeId { get; set; }
        public string ResourceRaw { get; set; }
        public string Resource { get; set; }
        public string TextName { get; set; }
        public bool IsDeleted { get; set; }

        public string AddBy { get; set; }
        public string AvatarFilePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
