CREATE PROCEDURE [dbo].[spPositions_GetVenuePositions]
	@VenueId int
AS
begin
	set nocount on;

	select *
	from dbo.Positions
	where VenueId = @VenueId;
end
