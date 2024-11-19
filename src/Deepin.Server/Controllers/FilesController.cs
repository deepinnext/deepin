using Deepin.Application.Commands.Files;
using HeyRed.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers;
public class FilesController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{id}")]
    public async Task<ActionResult<FileDTO>> Get(Guid id)
    {
        var file = await _mediator.Send(new GetFileQuery(id));
        if (file is null) return NotFound();
        return Ok(file);
    }

    [HttpPost]
    public async Task<ActionResult<FileDTO>> Upload(IFormFile file)
    {
        await _mediator.Send(new UploadFileCommand(file.ContentType, file.FileName, file.Length, file.OpenReadStream()));
        return Created();
    }

    [HttpGet("{id}/download")]
    public async Task<ActionResult<FileDTO>> Download(Guid id)
    {
        var file = await _mediator.Send(new DownloadFileCommand(id));
        if (file is null) return NotFound();
        return File(file.Item2, MimeTypesMap.GetMimeType(file.Item1.Format), file.Item1.Name);
    }
}
