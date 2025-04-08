CREATE TABLE AdminInputForm (
    Id INT PRIMARY KEY AUTO_INCREMENT, -- Auto-increment primary key
    Phone VARCHAR(15) NOT NULL, -- Assuming phone numbers will not exceed 15 characters
    FirstName VARCHAR(50) NOT NULL, -- Assuming a maximum length of 50 characters for names
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL, -- Assuming a max length of 100 for email
    Password VARCHAR(255) NOT NULL, -- Passwords should accommodate hashing storage
    ConfirmPassword VARCHAR(255) NOT NULL, -- Stored to compare during initial registration
    SecurityQuestion VARCHAR(255) NOT NULL, -- Allows for a variety of question lengths
    SecurityAnswer VARCHAR(255) NOT NULL -- Allows for a variety of answer lengths
);