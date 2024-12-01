using Deepin.Application.Commands.Posts;
using Deepin.Application.Queries;
using Deepin.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class PostsController(IMediator mediator, IUserContext userContext, IPostQueries postQueries) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        private readonly IPostQueries _postQueries = postQueries ?? throw new ArgumentNullException(nameof(postQueries));

        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] PostQuery query, CancellationToken cancellationToken)
        {
            var result = await _postQueries.GetListAsync(_userContext.UserId, query, cancellationToken);
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
