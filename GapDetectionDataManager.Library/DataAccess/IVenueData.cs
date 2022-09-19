using GapDetectionDataManager.Library.Models;
using System.Collections.Generic;

namespace GapDetectionDataManager.Library.DataAccess
{
    public interface IVenueData
    {
        void CreateNewVenue(string venue);
        List<VenueModel> GetAllVenues();
        List<PositionModel> GetVenuePositions(int venueId);
        void CreatePosition(PositionModel position);
        void CreatePositions(List<PositionModel> positions);
        void DeletePosition(PositionModel position);
    }
}