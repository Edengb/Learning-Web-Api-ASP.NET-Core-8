namespace ClientServerEvaluation.Dtos;

public record class CatDto
(
    Guid Id,

    string Nickname,

    int Age,
    
    string Breed
);
