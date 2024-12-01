using Deepin.Application.Commands.Notes;
using Deepin.Application.Queries;
using Deepin.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class NotesController(IMediator mediator, INoteQueries noteQueries, IUserContext userContext) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly INoteQueries _noteQueries = noteQueries ?? throw new ArgumentNullException(nameof(noteQueries));
        private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] NoteQuery query, CancellationToken cancellationToken)
        {
            var result = await _noteQueries.GetListAsync(_userContext.UserId, query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> Get(int id, CancellationToken cancellationToken)
        {
            var post = await _noteQueries.GetByIdAsync(id, cancellationToken);
            return post is null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create(CreateNoteCommand command, CancellationToken cancellationToken = default)
        {
            var note = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteNoteCommand(id), cancellationToken);
            return result ? NoContent() : NotFound();
        }
    }
}
