CREATE TABLE [dbo].[DiscordMessage] (
    [ShyId]        INT                IDENTITY (1, 1) NOT NULL,
    [ServerShyId]  INT                NULL,
    [ChannelShyId] INT                NOT NULL,
    [Id]           VARBINARY (8)      NOT NULL,
    [AuthorId]     VARBINARY (8)      NOT NULL,
    [ChannelId]    VARBINARY (8)      NOT NULL,
    [GuildId]      VARBINARY (8)      NULL,
    [IsPinned]     BIT                CONSTRAINT [DF_DiscordMessage_IsPinned] DEFAULT ((0)) NOT NULL,
    [Position]     INT                NOT NULL,
    [CreatedAt]    DATETIMEOFFSET (7) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DiscordMessage_Id]
    ON [dbo].[DiscordMessage]([Id] ASC);

