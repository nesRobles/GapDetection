CREATE TABLE [dbo].[Positions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GapOrder] INT NOT NULL, 
    [VenueId] INT NOT NULL, 
    [Position] NVARCHAR(20) NOT NULL
)
