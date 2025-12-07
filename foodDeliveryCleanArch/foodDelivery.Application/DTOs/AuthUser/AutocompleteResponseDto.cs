namespace foodDelivery.Application.DTOs.AuthUser;

public record AutocompleteResponseDto(List<AutocompleteItemDto> Items);

public record AutocompleteItemDto(Guid Id, string Name);