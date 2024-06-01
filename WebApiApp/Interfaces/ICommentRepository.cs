using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.Dtos.Comment;
using WebApiApp.Models;

namespace WebApiApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();

        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> CreateComment(Comment comment);

        Task<Comment?> Update(int id, Comment comment);

        Task<bool> DeleteAsync(int id);
    }
}