
CREATE PROCEDURE [dbo].[sb_FetchDiscordChannels]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
    SET NOCOUNT ON

	SELECT * FROM [dbo].[DiscordChannel]
END