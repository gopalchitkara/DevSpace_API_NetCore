using System;
using System.Collections.Generic;
using System.Linq;
using DevSpace_API.DataContext;
using DevSpace_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevSpace_API.Data.Drafts
{
    public class SqlDraftRepo : IDraftRepo
    {
        private readonly ApplicationContext _context;

        public SqlDraftRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateDraft(Draft draft)
        {
            if (draft == null)
            {
                throw new ArgumentNullException(nameof(draft));
            }

            _context.Drafts.Add(draft);
        }

        public void AddHashtagsForNewDraft(List<DraftHashtag> hashtags)
        {
            if (hashtags == null)
            {
                throw new ArgumentNullException(nameof(hashtags));
            }

            _context.DraftHashtags.AddRange(hashtags);
        }

        public void DeleteDraft(Draft draft)
        {
            if (draft == null)
            {
                throw new ArgumentNullException(nameof(draft));
            }

            _context.Drafts.Remove(draft);
        }

        public Draft GetDraftById(long id)
        {
            return _context.Drafts.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Draft> GetDraftsByUserId(string userid)
        {
            return _context.Drafts.Where(d => d.AuthorId == userid).Include(x => x.Hashtags).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateDraft(Draft draft)
        {
            //nothing
        }
    }
}