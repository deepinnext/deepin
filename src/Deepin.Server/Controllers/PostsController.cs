using Deepin.Application.Commands.Posts;
using Deepin.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class PostsController(IMediator mediator, IPostQueries postQueries) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly IPostQueries _postQueries = postQueries ?? throw new ArgumentNullException(nameof(postQueries));

        [HttpGet]
        public async Task<IActionResult> SearchPostsAsync([FromQuery] PostPagedQuery query, CancellationToken cancellationToken)
        {
            var result = await _postQueries.GetListAsync(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var post = await _postQueries.GetByIdAsync(id, cancellationToken);
            return post is null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePostCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdatePostCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePostCommand(id), cancellationToken);
            return result ? NoContent() : NotFound();
        }
    }
}