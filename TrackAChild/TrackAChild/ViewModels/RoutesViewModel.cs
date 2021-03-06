﻿using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Services;
using TrackAChild.Views;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using System;

namespace TrackAChild.ViewModels
{
    public class RoutesViewModel : Observable
    {
        IRouteService routeService;

        public ICommand NewRouteCommand { private set; get; }
        public ICommand AssignBusCommand { private set; get; }
        public ICommand AssignPassengersCommand { private set; get; }
        public ICommand ViewPassengersCommand { private set; get; }
        public ICommand EditRouteCommand { private set; get; }
        public ICommand RemoveRouteCommand { private set; get; }

        private Visibility isAssignBusButtonVisible = Visibility.Collapsed;
        public Visibility IsAssignBusButtonVisible
        {
            get { return isAssignBusButtonVisible; }
            set { isAssignBusButtonVisible = value; OnPropertyChanged(nameof(IsAssignBusButtonVisible)); }
        }

        private Visibility isAssignPassengersButtonVisible = Visibility.Collapsed;
        public Visibility IsAssignPassengersButtonVisible
        {
            get { return isAssignPassengersButtonVisible; }
            set { isAssignPassengersButtonVisible = value; OnPropertyChanged(nameof(IsAssignPassengersButtonVisible)); }
        }

        private RouteModel selectedRoute;
        public RouteModel SelectedRoute
        {
            get { return selectedRoute; }
            set
            {
                selectedRoute = value;
                OnPropertyChanged(nameof(SelectedRoute));

                if (SelectedRoute != null)
                {
                    IsAssignBusButtonVisible = Visibility.Visible;
                    IsAssignPassengersButtonVisible = Visibility.Visible;
                }
                else
                {
                    IsAssignBusButtonVisible = Visibility.Collapsed;
                    IsAssignPassengersButtonVisible = Visibility.Collapsed;
                }
            }
        }

        // Retrieve routes from route service
        public ObservableCollection<RouteModel> Routes
        {
            get { return routeService.Routes; }
        }

        public RoutesViewModel()
        {
            routeService = (App.Current as App).Container.GetService<IRouteService>();

            // Go to new route page
            NewRouteCommand = new RelayCommand(() =>
            {
                NavigationService.Navigate(typeof(RouteNewPage));
            });

            ViewPassengersCommand = new RelayCommand(async () =>
            {
                routeService.SetRouteToEdit(SelectedRoute);
                await new ViewPassengerListContentDialog().ShowAsync();
            });

            EditRouteCommand = new RelayCommand(() =>
            {
                // TODO
            });

            RemoveRouteCommand = new RelayCommand(() =>
            {
                // TODO
            });

            AssignBusCommand = new RelayCommand(async () =>
            {
                routeService.SetRouteToEdit(SelectedRoute);
                await new AssignBusContentDialog().ShowAsync();
            });

            AssignPassengersCommand = new RelayCommand(async () =>
            {
                routeService.SetRouteToEdit(SelectedRoute);
                await new AssignPassengersContentDialog().ShowAsync();
            });
        }
    }
}
