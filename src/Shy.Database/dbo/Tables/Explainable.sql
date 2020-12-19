CREATE TABLE [dbo].[Explainable] (
    [ShyId]       INT            IDENTITY (1, 1) NOT NULL,
    [Subject]     NVARCHAR (32)  NOT NULL,
    [Explanation] NVARCHAR (128) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Explainable_Subject]
    ON [dbo].[Explainable]([Subject] ASC);

