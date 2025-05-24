using Application.Dtos.User.Candidate;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly ICandidatesRepository _repository;

    public CandidatesController(ICandidatesRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewCandidateDto dto, CancellationToken cancellationToken)
    {
        var accessDto = await _repository.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = accessDto.OwnerId }, accessDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCandidateDto dto, CancellationToken cancellationToken)
    {
        var user = await _repository.UpdateAsync(id, dto, cancellationToken);
        return Ok(user);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
