using Caliburn.Micro;
using GapDetectionDataManager.Library.DataAccess;
using GapDetectionUI.EventModels;
using GapDetectionUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GapDetectionUI.ViewModels
{
    public class VenueSelectionViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private readonly IVenueData _db;
        private ObservableCollection<VenueModel>? _venues;

        public ObservableCollection<VenueModel> Venues
        {
            get { return _venues!; }
            set 
            { 
                _venues = value; 
                NotifyOfPropertyChange(() => Venues);
                NotifyOfPropertyChange(() => CanGapMenu);
            }
        }

        public VenueSelectionViewModel(IEventAggregator events, IVenueData db)
        {
            _events = events;
            _db = db;

            _venues = GetVenues();
        }

        private VenueModel? _selectedVenue;

        public VenueModel SelectedVenue
        {
            get { return _selectedVenue!; }
            set 
            {
                _selectedVenue = value;
                NotifyOfPropertyChange(() => SelectedVenue);
                NotifyOfPropertyChange(() => CanGapMenu);
            }
        }

        public bool CanGapMenu
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedVenue?.Venue);
            }
        }

        public void GapMenu()
        {
            _events.PublishOnUIThread(new LogOnEvent { Model = "Gaps", Venue = SelectedVenue.Venue, Id = SelectedVenue.Id });
        }

        public bool CanSettings()
        {
            return true;
        }

        public void Settings()
        {
            _events.PublishOnUIThread(new LogOnEvent { Model = "Settings"});
        }

        public ObservableCollection<VenueModel> GetVenues()
        {
            var dataVenues = _db?.GetAllVenues();

            ObservableCollection<VenueModel> venues = new ObservableCollection<VenueModel>();

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
    }
}
