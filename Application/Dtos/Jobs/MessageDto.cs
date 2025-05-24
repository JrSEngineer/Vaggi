namespace Application.Dtos.Jobs;

public sealed record MessageDto(
    Guid Id,
    Guid SenderId,
    Guid ReceiverId,
    DateTime SendedAt,
    string Content
);
