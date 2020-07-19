using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Drafts
{
    public interface IDraftRepo
    {
        bool SaveChanges();
        IEnumerable<Draft> GetDraftsByUserId(string userid);
        Draft GetDraftById(long id);
        void CreateDraft(Draft draft);
        void AddHashtagsForNewDraft(List<DraftHashtag> hashtags);
        void UpdateDraft(Draft draft);
        void DeleteDraft(Draft draft);
    }
}