CREATE PROCEDURE [dbo].[spPositions_UpdateGapOrder]
	@VenueId int,
	@Id int,
	@GapOrder int
AS

begin
	set nocount on;

	UPDATE dbo.Positions
	SET GapOrder =  @GapOrder
	where VenueId = @VenueId and Id = @Id;
end
