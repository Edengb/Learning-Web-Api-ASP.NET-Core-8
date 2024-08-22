using MyFirstApi.Models;
using System;

namespace MyFirstApi.Services;

public class PostsService : IPostService
{
    private static readonly List<Post> AllPosts = new();

    public Task CreatePost(Post item)
    {
        AllPosts.Add(item);
        return Task.CompletedTask;
    }

    public Task<Post?> UpdatePost(int id, Post item)
    {
        Post? post = AllPosts.FirstOrDefault(post => post.Id == id);
        if (post != null)
        {
            post.UserId = item.UserId;
            post.Title = item.Title;
            post.Body = item.Body;    
        }
        return Task.FromResult(post);
    }

    public Task<Post?> GetPost(int id)
    {
        Post? post = AllPosts.FirstOrDefault(post => post.Id == id);
        return Task.FromResult(post);
    }

    public Task<List<Post>> GetAllPosts()
    {
        return Task.FromResult(AllPosts);
    }

    public Task DeletePost(int id)
    {
        Post? post = AllPosts.FirstOrDefault(post => post.Id == id);
        if(post != null)
            AllPosts.Remove(post);
        return Task.CompletedTask;
    }
}
