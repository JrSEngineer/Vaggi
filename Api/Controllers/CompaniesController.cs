using Application.Dtos.User.Company;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompaniesRepository _repository;

    public CompaniesController(ICompaniesRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewCompanyDto dto, CancellationToken cancellationToken)
    {
        var accessDto = await _repository.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = accessDto.OwnerId }, accessDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _repository.GetAllAsync(cancellationToken);
        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync(id, cancellationToken);
        return Ok(company);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCompanyDto dto, CancellationToken cancellationToken)
    {
        var company = await _repository.UpdateAsync(id, dto, cancellationToken);
        return Ok(company);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
