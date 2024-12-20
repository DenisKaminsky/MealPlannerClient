﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MealPlannerClient.App.Helpers;

namespace MealPlannerClient.App
{
    public partial class App : Application
    {
        public static ViewModels.MainViewModel MainViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

            //initialize resources bases on saved preferences
            LanguagePreferencesManager.Initialize();
            FontPreferencesManager.Initialize();
            ThemePreferencesManager.Initialize();

            MainPage = new AppShell();
            MainViewModel = new ViewModels.MainViewModel();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Height = 1200;

            /*
            #region transition to "Running"
            window.Created += (s, e) =>
            {
                Toast.Make("Created", ToastDuration.Short, 15).Show();
            };

            window.Activated += (s, e) =>
            {
                Toast.Make("Activated", ToastDuration.Short, 15).Show();
            };
            #endregion

            #region Transition from "Running" to "Deactivated"
            window.Deactivated += (s, e) =>
            {
                Toast.Make("Deactivated", ToastDuration.Short, 15).Show();
            };
            #endregion

            #region Transition to "Stopped"
            window.Stopped += (s, e) =>
            {
                Toast.Make("Stopped", ToastDuration.Short, 15).Show();
            };
            #endregion

            #region Transition from "Stopped" to "Running"
            window.Resumed += (s, e) =>
            {
                Toast.Make("Resumed", ToastDuration.Short, 15).Show();
            };
            #endregion

            #region Transition to "Not Running"
            window.Destroying += (s, e) =>
            {
                Toast.Make("Destroying", ToastDuration.Short, 15).Show();
            };
            #endregion
            */

            return window;
        }
    }
}
