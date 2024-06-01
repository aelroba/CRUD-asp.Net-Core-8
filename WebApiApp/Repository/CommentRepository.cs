using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Data;
using WebApiApp.Dtos.Comment;
using WebApiApp.Interfaces;
using WebApiApp.Models;

namespace WebApiApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
                return false;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var item = await _context.Comments.FindAsync(id);
            if (item == null)
                return null;
            return item;
        }

        public async Task<Comment?> Update(int id,Comment comment)
        {
            var item = await _context.Comments.FindAsync(id);
            if (item == null)
                return null;
            else {
                item.Title = comment.Title;
                item.Content = comment.Content;

                await _context.SaveChangesAsync();
                return item;
            }
        }
    }
}