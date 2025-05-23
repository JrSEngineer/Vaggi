namespace Application.Dtos.Jobs;

public record MessageDto(
    Guid Id,
    Guid SenderId,
    Guid ReceiverId,
    DateTime SendedAt,
    string Content
);
