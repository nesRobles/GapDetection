CREATE PROCEDURE [dbo].[spPositions_DeletePosition]
	@VenueId int,
	@Id int
AS
begin
	set nocount on;

	DELETE FROM dbo.Positions
	where VenueId = @VenueId and Id = @Id;
end