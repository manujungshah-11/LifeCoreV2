using LifeV2.Application.DTOs;
using LifeV2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LifeV2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PoliciesController : ControllerBase
{
    private readonly IPolicyService _policyService;

    public PoliciesController(IPolicyService policyService)
    {
        _policyService = policyService;
    }

    /// <summary>Returns all life policies.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PolicyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await _policyService.GetAllAsync(ct));

    /// <summary>Returns a single policy by id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await _policyService.GetByIdAsync(id, ct));

    /// <summary>Creates a new policy.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePolicyRequest request, CancellationToken ct)
    {
        var created = await _policyService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Updates an existing policy.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePolicyRequest request, CancellationToken ct)
        => Ok(await _policyService.UpdateAsync(id, request, ct));

    /// <summary>Deletes a policy.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _policyService.DeleteAsync(id, ct);
        return NoContent();
    }
}
