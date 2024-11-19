using MediatR;

namespace Deepin.Application.Commands.Posts;

/// <summary>
/// Represents a command to update a post.
/// </summary>
public class UpdatePostCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the post ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the post title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post summary.
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the post tag IDs.
    /// </summary>
    public IEnumerable<int>? TagIds { get; set; }

    /// <summary>
    /// Gets or sets the post category IDs.
    /// </summary>
    public IEnumerable<int>? CategoryIds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the post is published.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the post is public.
    /// </summary>
    public bool IsPublic { get; set; }
}