using Deepin.Application.Commands.Categories;
using Deepin.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class CategoriesController(IMediator mediator, ICategoryQueries categoryQueries) : ApiControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly ICategoryQueries _categoryQueries = categoryQueries ?? throw new ArgumentNullException(nameof(categoryQueries));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get(CancellationToken cancellationToken)
        {
            var categories = await _categoryQueries.GetListAsync(cancellationToken);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var category = await _categoryQueries.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
