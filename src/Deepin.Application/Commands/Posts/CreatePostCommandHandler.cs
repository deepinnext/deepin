using System;
using Deepin.Application.Queries;
using Deepin.Application.Services;
using Deepin.Domain.PostAggregates;
using MediatR;

namespace Deepin.Application.Commands.Posts;

public class CreatePostCommandHandler(
    IPostRepository postRepository,
    IUserContext userContext) : IRequestHandler<CreatePostCommand, int>
{
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(_userContext.UserId, request.Title, request.Content, request.Summary);
        // Publish post
        if (request.IsPublished)
        {
            post.Publish();
        }
        // Add tags
        if (request.TagIds is not null)
        {
            request.TagIds.ToList().ForEach(post.AddTag);
        }
        // Add categories
        if (request.CategoryIds is not null)
        {
            request.CategoryIds.ToList().ForEach(post.AddCategory);
        }
        await _postRepository.AddAsync(post, cancellationToken);
        await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return post.Id;
    }
}
