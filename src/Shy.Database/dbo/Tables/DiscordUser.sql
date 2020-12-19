CREATE TABLE [dbo].[DiscordUser] (
    [ShyId]              INT                IDENTITY (1, 1) NOT NULL,
    [Id]                 VARBINARY (8)      NOT NULL,
    [Username]           NVARCHAR (32)      NOT NULL,
    [DiscriminatorValue] NVARCHAR (4)       NOT NULL,
    [IsBot]              BIT                CONSTRAINT [DF_DiscordUser_IsBot] DEFAULT ((0)) NOT NULL,
    [IsExcluded]         BIT                CONSTRAINT [DF_DiscordUser_IsExcluded] DEFAULT ((1)) NOT NULL,
    [IsWebhook]          BIT                CONSTRAINT [DF_DiscordUser_IsWebhook] DEFAULT ((0)) NOT NULL,
    [AvatarId]           NVARCHAR (128)     NULL,
    [CreatedAt]          DATETIMEOFFSET (7) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DiscordUser_Id]
    ON [dbo].[DiscordUser]([Id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DiscordUser_UserName]
    ON [dbo].[DiscordUser]([Username] ASC);

