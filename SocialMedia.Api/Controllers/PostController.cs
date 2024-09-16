using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository,IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDtos = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDtos);     
            //    posts.Select(x => new PostDto
            //{
            //    PostId = x.PostId,
            //    Date= x.Date,
            //    Description = x.Description,
            //    Image = x.Image,
            //    UserId = x.UserId
            //});
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
			return Ok(response);
			//    new PostDto
			//{
			//    PostId = post.PostId,
			//    Date = post.Date,
			//    Description = post.Description,
			//    Image = post.Image,
			//    UserId = post.UserId
			//};
		}

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            try
            {
                /*  Aca se puede validar si el Modelo es valido
                 *  al deshabilitar la funcion del ApiController
                 *  if(!ModelState.IsValid)
                 *  {
                 *      return BadRequest(ModelState);
                 *  }
                 */
                var post = _mapper.Map<Post>(postDto);
                //    new Post
                //{
                //    Date = postDto.Date,
                //    Description = postDto.Description,
                //    Image = postDto.Image,
                //    UserId = postDto.UserId
                //};
                await _postRepository.InsertPost(post);
                postDto = _mapper.Map<PostDto>(post);
                var response = new ApiResponse<PostDto>(postDto);
                return Ok(response);

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("{id}")]
		public async Task<IActionResult> Put(int id,  PostDto postDto)
		{
			try
			{
				
				var post = _mapper.Map<Post>(postDto);
                post.PostId = id;
				var result =  await _postRepository.UpdatePost(post);
				var response = new ApiResponse<bool>(result);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        [HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var result =  await _postRepository.DeletePost(id);
                var response = new ApiResponse<bool>(result);
				return Ok(response);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}





	}
}
