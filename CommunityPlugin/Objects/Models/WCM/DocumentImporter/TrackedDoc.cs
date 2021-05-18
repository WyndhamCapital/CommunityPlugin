using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlugin.Objects.Models.WCM.DocumentImporter
{
   public class TrackedDoc
    {
        public string TrackedDocumentId { get; set; }
        public List<string> AttachmentIds { get; set; }

        public TrackedDoc()
        {
            AttachmentIds = new List<string>();
        }

        public TrackedDoc(string trackedDocId, List<string> attachmentIds)
        {
            TrackedDocumentId = trackedDocId;
            AttachmentIds = attachmentIds;
        }

        public override bool Equals(object other)
        {
            return Equals(other as TrackedDoc);
        }


        public virtual bool Equals(TrackedDoc other)
        {
            if (other == null) { return false; }

            if (this.TrackedDocumentId != other.TrackedDocumentId) {  return false; }

            if(this.AttachmentIds.Count != other.AttachmentIds.Count) { return false; }

            return (new HashSet<string>(this.AttachmentIds)).SetEquals(new HashSet<string>(other.AttachmentIds));

        }


    }
}
