CREATE PROCEDURE [dbo].[spPositions_SavePosition]
	@GapOrder int,
	@VenueId int,
	@Position nvarchar(50)

AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Positions where VenueId = @VenueId and Position = @Position)

	begin
		insert into dbo.Positions (GapOrder, VenueId, Position)
	     values (@GapOrder, @VenueId, @Position);
	end
end
