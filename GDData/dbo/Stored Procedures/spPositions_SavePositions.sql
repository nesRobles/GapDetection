CREATE PROCEDURE [dbo].[spPositions_SavePositions]
	@GapOrder int,
	@VenueId int,
	@Position nvarchar(50)

AS
Begin
	set nocount on;

	if not exists (select 1 from dbo.Positions where Position = @Position)

	begin
		insert into dbo.Positions (GapOrder, VenueId, Position)
		values (@GapOrder, @VenueId, @Position)
	end
end
