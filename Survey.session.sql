CREATE TABLE Surveys (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200),
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE SurveyQuestions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SurveyId INT,
    QuestionText VARCHAR(500) NOT NULL,
    QuestionType VARCHAR(50) NOT NULL,  -- (Text, Textarea, Radio, Checkbox)
    Options VARCHAR(500),  -- Comma-separated options for multiple-choice questions
    FOREIGN KEY (SurveyId) REFERENCES Surveys(Id) -- Assuming you have a Surveys table
);

CREATE TABLE SurveyResponses (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SurveyQuestionId INT,
    StudentName VARCHAR(200),
    ResponseText TEXT,  -- For text and textarea answers
    ResponseOption VARCHAR(500),  -- For radio and checkbox answers (comma-separated for checkbox)
    FOREIGN KEY (SurveyQuestionId) REFERENCES SurveyQuestions(Id)  -- Foreign key referencing SurveyQuestions table
);
