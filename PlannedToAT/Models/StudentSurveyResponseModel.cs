using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class StudentSurveyResponseModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentEmail { get; set; }

    [Required]
    public string StudentName { get; set; } // NEW

    [Required]
    public string Question { get; set; }

    [Required]
    public string Response { get; set; }
}

public class StudentSurveyAnswers
{
    public string StudentName { get; set; } // NEW
    public string StudentEmail { get; set; } // NEW
    public Dictionary<string, string> Responses { get; set; }
}
