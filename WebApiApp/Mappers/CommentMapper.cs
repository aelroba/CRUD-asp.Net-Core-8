using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.Dtos.Comment;
using WebApiApp.Models;

namespace WebApiApp.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
            };
        }

         public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto comment, int stockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
            };
        }
    }
}