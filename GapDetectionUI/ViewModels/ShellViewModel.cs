using GapDetectionUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using GapDetectionUI.EventModels;

namespace GapDetectionUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly IEventAggregator _events;
        private readonly SimpleContainer _container;
        private GapDetectionViewModel _gaps;

        public ShellViewModel(IEventAggregator events, SimpleContainer container, GapDetectionViewModel gaps)
        {
            _events = events;
            _container = container;
            _gaps = gaps;
            _events.Subscribe(this);
            ActivateItem(_container.GetInstance<VenueSelectionViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            if (message.Model == "LogIn")
            {
                ActivateItem(_container.GetInstance<VenueSelectionViewModel>());
            }
            else if (message.Model == "Settings")
            {
                ActivateItem(_container.GetInstance<SettingsViewModel>());
            }
            else if (message.Model == "Gaps")
            {
                _gaps!.Venue = message.Venue;
                _gaps!.Id = message.Id;
                ActivateItem(_gaps);
                _gaps!.DisplayGapGrid();
            }
        }
    }
}
