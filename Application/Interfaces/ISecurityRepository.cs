using Applicatio.Dtos.Credential;
using Application.Dtos.Credential;

namespace Application.Interfaces.Repositories;

public interface ISecurityRepository
{
    Task<AccessDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
    Task RecoverPasswordAsync(RecoveryDto dto, CancellationToken cancellationToken);
}
