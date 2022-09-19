using GapDetectionDataManager.Library.Internal.DataAccess;
using GapDetectionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GapDetectionDataManager.Library.DataAccess
{
    public class VenueData : IVenueData
    {
        private readonly string connectionStringName = "SqliteDb";
        private readonly ISqliteDataAccess _db;

        public VenueData(ISqliteDataAccess db)
        {
            _db = db;
        }

        public void CreateNewVenue(string venue)
        {
            string sql = @"insert into Venues (Venue) 
                         values (@Venue);";

            _db.SaveData(sql, new { Venue = venue }, connectionStringName, false);
        }

        public List<VenueModel> GetAllVenues()
        {
            string sql = @"select * from Venues;";

            var venues = _db.LoadData<VenueModel, dynamic>(sql, new { }, connectionStringName, false);

            return venues;
        }

        public List<PositionModel> GetVenuePositions(int venueId)
        {
            string sql = @"select [Id], [GapOrder], [VenueId], [Position] from Positions where VenueId = @VenueId;";

            var positions = _db.LoadData<PositionModel, dynamic>(sql, new { VenueId = venueId}, connectionStringName, false);

            return positions;
        }

        public void CreatePosition(PositionModel position)
        {
            string sql = @"select 1 from Positions where VenueId = @VenueId and Position = @Position;";

            var result = _db.LoadData<PositionModel, dynamic>(sql, new { position.GapOrder, position.VenueId, position.Position }, connectionStringName, false).Count();

            if (result == 0)
            {
                sql = @"insert into Positions (GapOrder, VenueId, Position)
                      values(@GapOrder, @VenueId, @Position);";

                _db.SaveData(sql, new { position.GapOrder, position.VenueId, position.Position }, connectionStringName, false);
            }

            
        }

        public void CreatePositions(List<PositionModel> positions)
        {
            string sql = string.Empty;

            foreach (var position in positions)
            {
                sql = @"select 1 from Positions where VenueId = @VenueId and Position = @Position;";

                var result = _db.LoadData<PositionModel, dynamic>(sql, new { position.GapOrder, position.VenueId, position.Position }, connectionStringName, false).Count();

                if (result == 0)
                {
                    sql = @"insert into Positions (GapOrder, VenueId, Position)
                          values(@GapOrder, @VenueId, @Position);";

                    _db.SaveData(sql, new { position.GapOrder, position.VenueId, position.Position }, connectionStringName, false); 
                }
            }

            sql = @"select [Id], [GapOrder], [VenueId], [Position] from Positions where VenueId = @VenueId;";

            var dataPositions = _db.LoadData<PositionModel, dynamic>(sql, new { positions[0].VenueId}, connectionStringName, false);

            foreach (var position in positions)
            {
                foreach (var dataPosition in dataPositions)
                {
                    if (position.VenueId == dataPosition.VenueId)
                    {
                        if (position.Position == dataPosition.Position)
                        {
                            position.Id = dataPosition.Id;
                        }
                    }
                }
            }

            foreach (var position in positions)
            {
                sql = @"update Positions set GapOrder = @GapOrder where VenueId = @VenueId and Id = @Id;";

                _db.SaveData(sql, new { position.VenueId, position.Id, position.GapOrder }, connectionStringName, false);
            }
        }

        public void DeletePosition(PositionModel position)
        {
            string sql = "delete from Positions where VenueId = @VenueId and Id = @Id;";

            _db.SaveData(sql, new { position.Id, position.VenueId}, connectionStringName, false);
        }
    }
}
