using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.Dtos.Comment;
using WebApiApp.Interfaces;
using WebApiApp.Mappers;
using WebApiApp.Repository;

namespace WebApiApp.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository)
        {
            _commentRepo = commentRepo;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var results = comments.Select(x => x.ToCommentDto());
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(id);
            if(comment == null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute]int stockId, CreateCommentRequestDto commentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(! await _stockRepository.StockExists(stockId))
            return BadRequest("Stock Not Exist!");

            var comment = await _commentRepo.CreateComment(commentDto.ToCommentFromCreateDto(stockId));

            return CreatedAtAction(nameof(GetById),new {id = comment.Id}, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, UpdateCommentRequestDto commantDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var commentModel = await _commentRepo.Update(id, commantDto.ToCommentFromUpdateDto());
            if(commentModel == null)
                return NotFound("The request not fund");
            else
                return CreatedAtAction(nameof(GetById), new {id}, commentModel.ToCommentDto());
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if(!comment)
                return BadRequest("The Comment not found!");
            return Ok("We deleted it successfully");
        }
    }
}