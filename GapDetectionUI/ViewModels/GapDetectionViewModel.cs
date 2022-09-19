using Caliburn.Micro;
using GapDetectionDataManager.Library.DataAccess;
using GapDetectionDataManager.Library.Models;
using WPFExcelBump.Library;
using WPFExcelBump.Library.ExcelLogic;
using GapDetectionUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GapDetectionUI.EventModels;

namespace GapDetectionUI.ViewModels
{
    public class GapDetectionViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private readonly IVenueData? _db;
        private string _venue = string.Empty;
        private int _id;
        private ExcelFileModel? _excelBump;
        public GapDetectionView? _gapView;
        public bool isValidFile;

        public GapDetectionViewModel(IEventAggregator events, IVenueData db, GapDetectionView gapView)
        {
            BumpGrid = gapView.BumpGrid;
            _events = events;
            _db = db;
        }

        private Grid? _bumpGrid;

        public Grid? BumpGrid
        {
            get { return _bumpGrid; }
            set
            {
                _bumpGrid = value;

                NotifyOfPropertyChange(() => BumpGrid);
            }
        }


        private BindingList<PositionModel>? _positions;

        public BindingList<PositionModel>? Positions
        {
            get { return _positions; }
            set 
            {
                _positions = value; 
                
                NotifyOfPropertyChange(() => Positions);
            }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Venue
        {
            get { return _venue; }
            set 
            { 
                _venue = value;

                NotifyOfPropertyChange(() => Venue);
            }
        }

        public bool CanOpenBump
        {
            get
            {
                return true;
            }
        }

        public void OpenBump()
        {
            _excelBump = new ExcelFileModel();
            ExcelFileMethods.OpenExcelFileDialog(_excelBump);

            if (_excelBump.FilePath != "")
            {
                isValidFile = ExcelFileMethods.OpenExcelFile(_excelBump);
            }
        }

        public bool CanCheckGaps
        {
            get
            {
                return true;
            }
        }

        public void CheckGaps()
        {
            if (isValidFile)
            {
                ExcelBumpData.GetBumpGapData(_excelBump!, _db!, Id);

                GapDisplay.DisplayEmptyGapGrid(_excelBump!.GapData!.GetLength(0), _excelBump.GapData.GetLength(1) - 1, 25, 27, BumpGrid!);

                GapDisplay.DisplayGapData(_excelBump.GapData, 25, 27, BumpGrid!);
            }
        }
        
        public void DisplayGapGrid()
        {
            GapDisplay.CreateGapArea(BumpGrid!, _db!, Id);
        }

        public bool CanMainMenu 
        {
            get 
            { 
                return true; 
            }
        }

        public void MainMenu()
        {
            _events.PublishOnUIThread(new LogOnEvent { Model = "LogIn" });
        }
    }
}
