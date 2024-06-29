using CourseAPI.Dtos;
using DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



List<GetCoursesDto> courses = [
    
        new (
            1,
            "Node Backend Development",
            "This is a demo course",
            20,
            "1"
        ),
        new (
            2,
            "React Development",
            "This is a Full course",
            10,
            "2"
        ),
        new (
            3,
            "Java with OOP Internship Bootcamp",
            "This is a Full course",
            20,
            "2"
        )
];

app.MapGet("/", () => "Hello World!");
app.MapGet("/courses", () => courses);

app.MapGet("/courses/{id}", (int id) => {
    return courses.Find(course => course.id == id);
}).WithName("GetCourse");

app.MapPost("courses", (CreateCourseDto newCourse) => {
    int id = courses.Count + 1;

    GetCoursesDto coursesDto = new (id,
                                    newCourse.Name,
                                    newCourse.Description,
                                    newCourse.NoOfChapters,
                                    newCourse.InstructorId
                                    );
    courses.Add(coursesDto);
    
    return Results.CreatedAtRoute("GetCourse", new{id=id}, coursesDto);
});

// app.MapPut("courses/{id}", (int id, CreateCourseDto updatedCourse) => {
//     GetCoursesDto course = courses.Find(course => course.id == id);
//     if(course == null){
//         return Results.NotFound();
//     }

//     GetCoursesDto coursesDto = new (id,
//                                     updatedCourse.Name,
//                                     updatedCourse.Description,
//                                     updatedCourse.NoOfChapters,
//                                     updatedCourse.InstructorId
//                                     );
    
//     courses[id-1] = coursesDto;
//     Console.WriteLine("Hi: ",coursesDto.ToString());
//     return Results.Ok();
// });
app.MapPut("courses/{id}",(int id, CreateCourseDto updatedCourse) =>{
    GetCoursesDto? currCourse = courses.Find(course=> course.id == id);
    if(currCourse == null){
        return Results.NotFound();
    }
    GetCoursesDto newCourse = new(
        id,
        updatedCourse.Name,
        updatedCourse.Description,
        updatedCourse.NoOfChapters,
        updatedCourse.InstructorId
        
    );
    courses[id-1] = newCourse;
    return Results.Ok();
});

app.MapDelete("/courses/{id}",(int id) => {
    int courseId = courses.FindIndex(course => course.id == id);
    if (courseId == -1){
        return Results.NotFound();
    }
    courses.RemoveAt(courseId - 1);
    return Results.NoContent();
});

app.Run();
