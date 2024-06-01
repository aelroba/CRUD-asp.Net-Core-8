using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Please enter correct title.")]
        [MaxLength(255, ErrorMessage = "Please enter correct title.")]
        public string Title { get; set; } = string.Empty;   
        [Required]
        [MinLength(5, ErrorMessage = "Please enter correct title.")]
        [MaxLength(255, ErrorMessage = "Please enter correct title.")]
        public string Content { get; set; } = string.Empty;

    }
}