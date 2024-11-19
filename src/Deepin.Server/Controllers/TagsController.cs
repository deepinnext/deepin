using Deepin.Application.Commands.Tags;
using Deepin.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class TagsController(IMediator mediator, ITagQueries tagQueries) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly ITagQueries _tagQueries = tagQueries ?? throw new ArgumentNullException(nameof(tagQueries));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> Get(CancellationToken cancellationToken)
        {
            var tags = await _tagQueries.GetListAsync(cancellationToken);
            return Ok(tags);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var tag = await _tagQueries.GetByIdAsync(id, cancellationToken);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        [HttpPost]
        public async Task<ActionResult<TagDto>> Create(CreateTagCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TagDto>> Update(int id, UpdateTagCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
