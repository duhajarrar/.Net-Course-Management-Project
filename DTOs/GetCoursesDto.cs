namespace DTOs;
public record class GetCoursesDto(
    int id,
    string Name, 
    string Description,
    int NoOfChapters,
    string InstructorId

);