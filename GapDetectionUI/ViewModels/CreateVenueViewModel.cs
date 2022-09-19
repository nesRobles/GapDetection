using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GapDetectionDataManager.Library.DataAccess;
using GapDetectionUI.Models;
using System.ComponentModel;
using GapDetectionUI.EventModels;

namespace GapDetectionUI.ViewModels
{
    public class CreateVenueViewModel : Conductor<object>
    {
        private readonly IWindowManager _window;
        private readonly IEventAggregator _events;
        private readonly IVenueData _db;
        private string _venue = string.Empty;

        public CreateVenueViewModel(IWindowManager window, IEventAggregator events, IVenueData db)
        {
            _window = window;
            _events = events;
            _db = db;
        }

        public string Venue
        {
            get { return _venue; }
            set
            {
                _venue = value;
                NotifyOfPropertyChange(() => Venue);
                NotifyOfPropertyChange(() => CanCreateVenue);
            }
        }

        public bool CanCreateVenue
        {
            get 
            {
                return !string.IsNullOrWhiteSpace(Venue);
            }
        }

        public void CreateVenue()
        {
            _db.CreateNewVenue(Venue);
            TryClose();
            _events.PublishOnUIThread(new LogOnEvent { Model = "Settings" });
        }
    }
}
