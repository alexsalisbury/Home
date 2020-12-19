CREATE TABLE [dbo].[DiscordServer] (
    [ShyId]       INT            IDENTITY (1, 1) NOT NULL,
    [Id]          VARBINARY (8)  NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [IconUrl]     NVARCHAR (128) NULL,
    [SplashUrl]   NVARCHAR (128) NULL,
    [MemberCount] INT            NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DiscordServer_Id]
    ON [dbo].[DiscordServer]([Id] ASC);

