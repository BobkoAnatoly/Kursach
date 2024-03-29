﻿using ProfileClassLibrary.TrainClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Курсовая.ProgrammInterface;
using Курсовая.Setting;

namespace Курсовая.MainFrameForms.PlanePages
{
    public partial class TrainStationsPage : Page
    {
        private DataBase data;

        private IWorkWithBusList workWithBusList;

        public List<RouteStation> Stations { get; set; }
        private List<Train> AllTrains;

        public TrainStationsPage(int trainId, string route, List<Train> allTrains,string trainType)
        {
            InitializeComponent();
            data = new DataBase();
            workWithBusList = new WorkWithBusList();
            AllTrains = allTrains.ToList();
            TrainNameTb.Text = route;
            TrainTypeTb.Text = trainType;
            Stations = GetStation(trainId);
            FlightsListBox.ItemsSource = Stations;
        }

        private void Close_Click(object sender, RoutedEventArgs e)=>
            NavigationService.Navigate(new TrainPage(AllTrains));

        private List<RouteStation> GetStation(int trainId)
        {
            List<RouteStation> trains = new List<RouteStation>();
            string query = $"SELECT Station,Direction,StopTime,Departure FROM TrainRoute WHERE TrainId = {trainId};";
            SqlCommand command = new SqlCommand(query, data.GetConnection());
            data.OpenConnection();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dir = default, stop = default, dep = default;
                        try { dir = Convert.ToString(reader.GetValue(1)).Substring(0, 5); }
                        catch (Exception) { dir = "-"; }
                        try { stop = (string)reader.GetValue(2); if (stop == "NULL") stop = "-"; }
                        catch (Exception) { stop = "-"; }
                        try { dep = Convert.ToString(reader.GetValue(3)).Substring(0, 5); }
                        catch (Exception) { dep = "-"; }
                        trains.Add(new RouteStation((string)reader.GetValue(0), dir, dep, stop));
                    }
                }
            }
            catch (Exception) { }
            finally { data.CloseConnection(); }
            return trains;
        }

        private void FlightsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)=>
            workWithBusList.BucketListBoxFirst(sender, e);
    }
}
