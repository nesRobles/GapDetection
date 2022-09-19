using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GapDetectionDataManager.Library.Models
{
    public class PositionModel
    {
        public int Id { get; set; }
        public int GapOrder { get; set; }
        public int VenueId { get; set; }
        public string Position { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
    }
}
