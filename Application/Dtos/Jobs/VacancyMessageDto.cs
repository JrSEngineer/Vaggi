namespace Application.Dtos.Jobs;

public record VacancyMessageDto(
    Guid Id,
    Guid ApplicationId,
    Guid SenderId,
    Guid ReceiverId,
    DateTime SendedAt,
    DateTime ReadedAt,
    string Content
);
