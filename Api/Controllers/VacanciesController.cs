using Application.Dtos.Jobs;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacanciesController : ControllerBase
{
    private readonly IVacanciesRepository _repository;

    public VacanciesController(IVacanciesRepository repository)
    {
        _repository = repository;
    }


    [HttpPost]
    public async Task<IActionResult> Create(NewVacancyDto dto, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = vacancy.Id }, vacancy);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetAllAsync(cancellationToken);
        return Ok(vacancy);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetByIdAsync(id, cancellationToken);
        return Ok(vacancy);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateVacancyDto dto, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.UpdateAsync(id, dto, cancellationToken);
        return Ok(vacancy);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
