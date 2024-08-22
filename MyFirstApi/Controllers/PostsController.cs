using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyFirstApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(IPostService postsService, ILogger<PostsController> logger) : ControllerBase
{ 
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await postsService.CreatePost(post);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Post>> UpdatePost(int id, Post post)
    {
        if (id != post.Id)
            return BadRequest();
        Post? _post = await postsService.UpdatePost(id, post);
        if (_post == null)
            return NotFound();
        return Ok(_post);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost([FromKeyedServices("SillyServiceFixedContent")] ISillyService sillyService, int id)
    {
        logger.LogInformation($"Service GetData: {sillyService.GetData()}");
        sillyService.DoSillyThing();
        Post? post = await postsService.GetPost(id);
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    
    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts([FromKeyedServices("SillyServiceRandom")] ISillyService sillyServiceRandom, ISillyService sillyService)
    {
        logger.LogInformation($"Service GetData: {sillyService.GetData()}");
        sillyService.DoSillyThing();
        logger.LogInformation($"Service Random GetData: {sillyServiceRandom.GetData()}");
        sillyServiceRandom.DoSillyThing();
        return Ok(await postsService.GetAllPosts());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await postsService.DeletePost(id);
        return NoContent();
    }
}

internal interface IPostsService
{
}