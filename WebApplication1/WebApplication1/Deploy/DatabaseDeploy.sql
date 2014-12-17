CREATE TABLE [dbo].[School] (
    [Id]           INT         IDENTITY (1, 1) NOT NULL,
    [Name]         NCHAR (255) NULL,
    [DirName]      NCHAR (100) NULL,
    [Address]      NCHAR (255) NULL,
    [Phone_Number] NCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Status]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Status] NCHAR(50) NULL, 
    [Description] NCHAR(250) NULL
);


CREATE TABLE [dbo].[Type]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(20) NULL, 
    [Description] NCHAR(255) NULL
);


CREATE TABLE [dbo].[AspNetUsers] (
    [Id]            NVARCHAR (128) NOT NULL,
    [UserName]      NVARCHAR (MAX) NULL,
    [PasswordHash]  NVARCHAR (MAX) NULL,
    [SecurityStamp] NVARCHAR (MAX) NULL,
    [Discriminator] NVARCHAR (128) NOT NULL,
    [School_id] INT NULL, 
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [Schoolid_] FOREIGN KEY ([School_id]) REFERENCES [School]([id])
);


CREATE TABLE [dbo].[Report] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Text]      TEXT           NULL,
    [Date]      DATE           NULL,
    [Status_id] INT            NULL,
    [Type_id]   INT            NULL,
    [School_id] INT            NULL,
    [User_id]   NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Typeid] FOREIGN KEY ([Type_id]) REFERENCES [dbo].[Type] ([Id]),
    CONSTRAINT [Statusid] FOREIGN KEY ([Status_id]) REFERENCES [dbo].[Status] ([Id]),
    CONSTRAINT [Userid] FOREIGN KEY ([User_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [Schoolid] FOREIGN KEY ([School_id]) REFERENCES [dbo].[School] ([Id])
);




CREATE TABLE [dbo].[Сomments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Text] TEXT NULL, 
    [Date_time] DATE NULL, 
    [Report_id] INT NULL, 
    [Users_id] NVARCHAR(128) NULL, 
    CONSTRAINT [Reportid] FOREIGN KEY ([Report_id]) REFERENCES [Report]([id]), 
    CONSTRAINT [Usersid] FOREIGN KEY ([Users_id]) REFERENCES [AspNetUsers]([id])
);