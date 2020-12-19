CREATE TABLE [dbo].[DiscordChannel] (
    [ShyId]           INT                IDENTITY (1, 1) NOT NULL,
    [ServerShyId]     INT                NULL,
    [Id]              VARBINARY (8)      NOT NULL,
    [CategoryId]      VARBINARY (8)      NULL,
    [GuildId]         VARBINARY (8)      NULL,
    [Name]            NVARCHAR (100)     NOT NULL,
    [IsNsfw]          BIT                CONSTRAINT [DF_DiscordChannel_IsNsfw] DEFAULT ((0)) NOT NULL,
    [IsShyRpgChannel] BIT                CONSTRAINT [DF_DiscordChannel_IsShyRpgChannel] DEFAULT ((0)) NOT NULL,
    [IsUserDM]        BIT                CONSTRAINT [DF_DiscordChannel_IsUserDM] DEFAULT ((0)) NOT NULL,
    [Position]        INT                NOT NULL,
    [CreatedAt]       DATETIMEOFFSET (7) NOT NULL
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DiscordChannel_Id]
    ON [dbo].[DiscordChannel]([Id] ASC);

