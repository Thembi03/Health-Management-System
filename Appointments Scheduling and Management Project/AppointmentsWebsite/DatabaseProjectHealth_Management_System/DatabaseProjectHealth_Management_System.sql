CREATE TABLE [dbo].[Appointment] (
    [Appointment_ID]   VARCHAR (50) NOT NULL,
    [Appointment_Type] VARCHAR (50) NULL,
    [Appointment_Time] TIME (7)     NULL,
    [Doctor_ID]        VARCHAR (50) NULL,
    [Patient_ID]       VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Appointment_ID] ASC),
    FOREIGN KEY ([Doctor_ID]) REFERENCES [dbo].[Doctor] ([Doctor_ID]),
    FOREIGN KEY ([Patient_ID]) REFERENCES [dbo].[Patient] ([Patient_ID])
);


GO

CREATE TABLE [dbo].[Feedback] (
    [Feedback_No]    INT          NOT NULL,
    [Feeback]        TEXT         NULL,
    [Appointment_ID] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Feedback_No] ASC),
    FOREIGN KEY ([Appointment_ID]) REFERENCES [dbo].[Appointment] ([Appointment_ID])
);


GO

CREATE TABLE [dbo].[Users] (
    [Users_ID] VARCHAR (50) NOT NULL,
    [pass]     VARCHAR (12) NULL,
    [Access]   VARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Users_ID] ASC)
);


GO

CREATE TABLE [dbo].[apnmt] (
    [appointment_id]   INT            IDENTITY (1, 1) NOT NULL,
    [fullname]         NVARCHAR (255) NOT NULL,
    [national_id]      NVARCHAR (20)  NOT NULL,
    [email]            NVARCHAR (255) NOT NULL,
    [phone]            NVARCHAR (20)  NOT NULL,
    [department]       NVARCHAR (100) NOT NULL,
    [message]          NVARCHAR (MAX) NULL,
    [appointment_date] DATE           NOT NULL,
    [appointment_time] TIME (7)       NOT NULL,
    [created_at]       DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([appointment_id] ASC)
);


GO

CREATE TABLE [dbo].[Patient] (
    [Patient_ID]           VARCHAR (50)  NOT NULL,
    [Users_ID]             VARCHAR (50)  NULL,
    [First_Name]           VARCHAR (50)  NULL,
    [Last_Name]            VARCHAR (50)  NULL,
    [National_ID]          VARCHAR (50)  NULL,
    [Date_of_Birth]        DATE          NULL,
    [Sex]                  VARCHAR (6)   NULL,
    [Street_Address]       VARCHAR (255) NULL,
    [City]                 VARCHAR (100) NULL,
    [Phone_Number]         VARCHAR (20)  NULL,
    [Email]                VARCHAR (100) NULL,
    [Conditions_Allergies] TEXT          NULL,
    PRIMARY KEY CLUSTERED ([Patient_ID] ASC)
);


GO

CREATE TABLE [dbo].[Doctor] (
    [Doctor_ID]        VARCHAR (50)  NOT NULL,
    [Users_ID]         VARCHAR (50)  NULL,
    [First_Name]       VARCHAR (50)  NULL,
    [Last_Name]        VARCHAR (50)  NULL,
    [Specialty]        VARCHAR (50)  NULL,
    [Phone_Number]     VARCHAR (20)  NULL,
    [Email]            VARCHAR (100) NULL,
    [Telephone_Number] VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Doctor_ID] ASC),
    FOREIGN KEY ([Users_ID]) REFERENCES [dbo].[Users] ([Users_ID])
);


GO

