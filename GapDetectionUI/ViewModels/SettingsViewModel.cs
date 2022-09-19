using Caliburn.Micro;
using GapDetectionDataManager.Library.DataAccess;
using GapDetectionUI.EventModels;
using GapDetectionUI.Models;
using GapDetectionUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GapDetectionUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private readonly IEventAggregator? _events;
        private readonly IWindowManager? _window;
        private CreateVenueViewModel? _createVenue;
        private readonly IVenueData? _db;
        private List<VenueModel>? _dbVenues;

        public SettingsViewModel(IEventAggregator events, IWindowManager window, CreateVenueViewModel createVenue, IVenueData db)
        {
            _events = events;
            _window = window;
            _db = db;
            _dbVenues = GetVenues();
            Venues = new BindingList<VenueModel>(_dbVenues);
            _createVenue = createVenue;
        }

        private BindingList<VenueModel>? _venues;

        public BindingList<VenueModel> Venues
        {
            get { return _venues!; }
            set { _venues = value; }
        }

        private BindingList<PositionModel>? _positions;

        public BindingList<PositionModel>? Positions
        {
            get { return _positions!; }
            set 
            { 
                _positions = value;
                NotifyOfPropertyChange(() => Positions);
                NotifyOfPropertyChange(() => CanAddPosition);
                NotifyOfPropertyChange(() => CanSavePositions);
            }
        }

        private string? _newPosition;

        public string? NewPosition
        {
            get { return _newPosition; }
            set 
            { 
                _newPosition = value; 

                NotifyOfPropertyChange(() => NewPosition);
                NotifyOfPropertyChange(() => CanAddPosition);
            }
        }

        private VenueModel? _selectedVenue;

        public VenueModel? SelectedVenue
        {
            get { return _selectedVenue!; }
            set 
            { 
                _selectedVenue = value;
                NotifyOfPropertyChange(() => SelectedVenue);
                NotifyOfPropertyChange(() => CanSelectVenue);
            }
        }

        private string? _venueHeader;

        public string VenueHeader
        {
            get { return _venueHeader!; }
            set 
            {
                _venueHeader = value;
                NotifyOfPropertyChange(() => VenueHeader);
            }
        }

        public bool CanCreateVenue
        {
            get 
            {
                return true;
            }
        }

        public void CreateVenue()
        {
            _window!.ShowWindow(_createVenue);
        }

        public bool CanBack()
        {
            return true;
        }

        public void Back()
        {
            _events.PublishOnUIThread(new LogOnEvent { Model = "LogIn" });
        }

        public bool CanSelectVenue
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedVenue?.Venue);
            }
        }

        public void SelectVenue()
        {
            VenueHeader = SelectedVenue!.Venue;

            Positions = GetVenuePositions();

            SortPositions();
            //sitions = new BindingList<PositionModel>(positions);
        }

        private PositionModel? _selectedPosition;

        public PositionModel? SelectedPosition
        {
            get { return _selectedPosition; }
            set 
            { 
                _selectedPosition = value;
                
                NotifyOfPropertyChange(() => SelectedPosition);
                NotifyOfPropertyChange(() => CanDeletePosition);
                NotifyOfPropertyChange(() => CanMoveUp);
                NotifyOfPropertyChange(() => CanMoveDown);
            }
        }

        public bool CanAddPosition
        {
            get
            {
                bool output = !string.IsNullOrWhiteSpace(NewPosition);
                return output;
            }
        }

        public void AddPosition()
        {
            Positions!.Add(new PositionModel());

            Positions.Last().GapOrder = Positions.Count;
            Positions.Last().VenueId = SelectedVenue!.Id;
            Positions.Last().Position = NewPosition!;

            NotifyOfPropertyChange(() => CanSavePositions);

            SavePosition(Positions.Last());

            Positions = GetVenuePositions();

            SortPositions();

            NewPosition = string.Empty;
        }

        public bool CanDeletePosition
        {
            get
            {
                return true;
            }
        }

        public void DeletePosition()
        {
            if (SelectedPosition != null)
            {
                int selectedPosIndex = SelectedPosition!.GapOrder - 1;
                int selectedPosId = SelectedPosition.Id;

                if (selectedPosId != 0)
                {
                    var dataPosition = MapToDataPositionModel();

                    _db!.DeletePosition(dataPosition);
                }

                Positions!.RemoveAt(selectedPosIndex);

                for (int i = selectedPosIndex; i < Positions.Count; i++)
                {
                    Positions[i].GapOrder = i + 1;
                }

                SavePositions();
            }
        }

        public bool CanSavePositions
        {
            get
            {
                bool output = PositionNullCheck(Positions!);
                return output;
            }
        }

        public void SavePositions()
        {
            SortPositions();

            List<GapDetectionDataManager.Library.Models.PositionModel> dPosition = new();
            
            foreach (var uiPosition in Positions!)
            {
                dPosition.Add(new GapDetectionDataManager.Library.Models.PositionModel
                {
                    Id = uiPosition.Id,
                    GapOrder = uiPosition.GapOrder,
                    VenueId = uiPosition.VenueId,
                    Position = uiPosition.Position,
                });
            }

            _db?.CreatePositions(dPosition);

            Positions = GetVenuePositions();
            SortPositions();
            //SelectedVenue = null;
            //Positions = null;
        }

        public void SavePosition(PositionModel position)
        {
            GapDetectionDataManager.Library.Models.PositionModel dPosition = new();

            dPosition.GapOrder = position.GapOrder;
            dPosition.VenueId = position.VenueId;
            dPosition.Position = position.Position;

            _db?.CreatePosition(dPosition);

            Positions = GetVenuePositions();
            SortPositions();
        }

        public bool CanMoveUp
        {
            get
            {
                return SelectedPosition != null;
            }
        }

        public void MoveUp()
        {
            if (SelectedPosition!.GapOrder > 1)
            {
                MoveSelecetedPositionUp();

                SortPositions();
            }
        }

        public bool CanMoveDown
        {
            get
            {
                return SelectedPosition != null;
            }
        }

        public void MoveDown()
        {
            if (SelectedPosition!.GapOrder > 0)
            {
                if (SelectedPosition.GapOrder < Positions!.Count)
                {
                    MoveSelecetedPositionDown();

                    SortPositions(); 
                }
            }
        }

        public List<VenueModel> GetVenues()
        {
            var dataVenues = _db?.GetAllVenues();

            List<VenueModel> venues = new List<VenueModel>();

            foreach (var daVavenue in dataVenues!)
            {
                venues.Add(new VenueModel
                {
                    Id = daVavenue.Id,
                    Venue = daVavenue.Venue
                });
            }

            return venues;
        }

        public BindingList<PositionModel> GetVenuePositions()
        {
            var dataPositions = _db?.GetVenuePositions(SelectedVenue!.Id);

            BindingList<PositionModel> positions = new BindingList<PositionModel>();

            foreach (var position in dataPositions!)
            {
                positions.Add(new PositionModel
                {
                    Id = position.Id,
                    GapOrder = position.GapOrder,
                    VenueId = position.VenueId,
                    Position = position.Position,
                    ShortName = position.ShortName
                });
            }

           
            return positions;
        }

        public bool PositionNullCheck(BindingList<PositionModel> positions)
        {
            bool result = false;

            if (positions != null)
            {
                if (positions.Count > 0)
                {
                    return true;
                }
            }

            return result;
        }

        public void MoveSelecetedPositionUp()
        {
            int selectedPosGap = SelectedPosition!.GapOrder;
            int selectedPosIndex = SelectedPosition.GapOrder - 1;

            int previousPosGap = Positions![selectedPosIndex - 1].GapOrder;

            SelectedPosition.GapOrder = previousPosGap;
            Positions[selectedPosIndex - 1].GapOrder = selectedPosGap;
        }

        public void MoveSelecetedPositionDown()
        {
            int selectedPosGap = SelectedPosition!.GapOrder;
            int selectedPosIndex = SelectedPosition.GapOrder - 1;

            int NextPosGap = Positions![selectedPosIndex + 1].GapOrder;

            SelectedPosition.GapOrder = NextPosGap;
            Positions[selectedPosIndex + 1].GapOrder = selectedPosGap;
        }

        public void SortPositions()
        {
            Positions = new BindingList<PositionModel>(Positions!.OrderBy(x => x.GapOrder).ToList());
        }

        public GapDetectionDataManager.Library.Models.PositionModel MapToDataPositionModel()
        {
            GapDetectionDataManager.Library.Models.PositionModel dPosition = new();
            dPosition.Id = SelectedPosition!.Id;
            dPosition.VenueId = SelectedPosition.VenueId;
            dPosition.GapOrder = SelectedPosition.GapOrder;

            return dPosition;
        }
    }
}
