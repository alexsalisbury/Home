
CREATE PROCEDURE [dbo].[sb_EnsureDiscordChannel]
(
	@ServerShyId int,
	@Id varbinary(8),
	@CategoryId varbinary(8),
	@GuildId varbinary(8),
	@Name nvarchar(100),
	@IsNsfw bit,
	@IsShyRpgChannel bit,
	@IsUserDM bit,
	@Position int,
	@CreatedAt datetimeoffset(7)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON

	IF (NOT EXISTS(SELECT * FROM [dbo].[DiscordChannel] WHERE [Id] = @Id))
		BEGIN

		INSERT INTO [dbo].[DiscordChannel]
					([ServerShyId],[Id],[CategoryId],[GuildId],[Name],[IsNsfw],[IsShyRpgChannel],[IsUserDM],[Position],[CreatedAt])
		VALUES		(@ServerShyId, @Id, @CategoryId, @GuildId, @Name, @IsNsfw, @IsShyRpgChannel, @IsUserDM, @Position, @CreatedAt)
	END
END