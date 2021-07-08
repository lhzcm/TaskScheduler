IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [t_log] (
    [Id] int NOT NULL IDENTITY,
    [TaskId] int NOT NULL,
    [Level] int NOT NULL,
    [Message] varchar(max) NOT NULL,
    [WriteTime] datetime NOT NULL,
    CONSTRAINT [PK_t_log] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [t_task] (
    [Id] int NOT NULL IDENTITY,
    [TaskGuid] uniqueidentifier NOT NULL DEFAULT (newid()),
    [Name] varchar(128) NOT NULL,
    [ExecFile] varchar(512) NOT NULL,
    [UpdateTime] datetime NOT NULL DEFAULT (getdate()),
    [Status] int NOT NULL,
    [WriteTime] datetime NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_t_task] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [t_task_command] (
    [Id] int NOT NULL IDENTITY,
    [TaskId] int NOT NULL,
    [Description] varchar(128) NOT NULL,
    [Command] varchar(64) NOT NULL,
    [WriteTime] datetime NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_t_task_command] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [t_task_config] (
    [Id] int NOT NULL IDENTITY,
    [TaskId] int NOT NULL,
    [Key] varchar(256) NOT NULL,
    [Value] varchar(2048) NOT NULL,
    [WriteTime] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_t_task_config] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_t_task_command_TaskId] ON [t_task_command] ([TaskId]);
GO

CREATE INDEX [IX_t_task_config_TaskId] ON [t_task_config] ([TaskId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210708060511_initial', N'5.0.7');
GO

COMMIT;
GO

