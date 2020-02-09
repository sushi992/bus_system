﻿using System.Collections.ObjectModel;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using TrackAChild.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TrackAChild.ViewModels
{
    public class AssignDriverViewModel : Observable
    {
        IDriverService driverService;

        // Retrieve drivers from driver service
        public ObservableCollection<DriverModel> Drivers
        {
            get { return driverService.Drivers; }
        }

        // Selected Driver
        private DriverModel selectedDriver;
        public DriverModel SelectedDriver
        {
            get { return selectedDriver; }
            set { Set(ref selectedDriver, value);
                if (SelectedDriver != null) { IsAssignButtonEnabled = true; } else { IsAssignButtonEnabled = false; }
            }
        }

        private bool isAssignButtonEnabled = false;
        public bool IsAssignButtonEnabled
        {
            get { return isAssignButtonEnabled; }
            set { isAssignButtonEnabled = value; OnPropertyChanged(nameof(IsAssignButtonEnabled)); }
        }

        public AssignDriverViewModel()
        {
            // Retrieve driver service
            driverService = (App.Current as App).Container.GetService<IDriverService>();
        }
    }
}
