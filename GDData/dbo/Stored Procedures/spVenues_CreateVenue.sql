CREATE PROCEDURE [dbo].[spVenues_CreateVenue]
	@Venue nvarchar(50)
AS
begin
	set nocount on;

	insert into dbo.Venues(Venue)
	values(@Venue);
end
