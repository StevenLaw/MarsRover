using MarsRoverApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace MarsRoverApp.ViewModel
{
    //enum Direction
    //{
    //    North = 'N',
    //    East = 'E',
    //    South = 'S',
    //    West = 'W'
    //}
    public class MarsRoverModel : INotifyPropertyChanged
    {
        private string command = "";
        private string result;
        private string initialHeading;
        private int initialPositionY;
        private int initialPositionX;
        private int upperBoundY;
        private int upperBoundX;

        public int UpperBoundX 
        { 
            get => upperBoundX;
            set 
            {
                upperBoundX = value; 
                NotifyPropertyChanged();
            }
        }
        public int UpperBoundY
        {
            get => upperBoundY;
            set
            {
                upperBoundY = value;
                NotifyPropertyChanged();
            }
        }
        public int InitialPositionX 
        { 
            get => initialPositionX;
            set
            {
                initialPositionX = value;
                NotifyPropertyChanged();
            }
        }
        public int InitialPositionY
        {
            get => initialPositionY;
            set
            {
                initialPositionY = value;
                NotifyPropertyChanged();
            }
        }
        //Maybe set the name and pull first char when sending call
        public string InitialHeading 
        { 
            get => initialHeading;
            set
            {
                initialHeading = value;
                NotifyPropertyChanged();
            }
        }
        public char HeadingChar
        {
            get => initialHeading.First();
        }
        public string Command
        {
            get => command;
            set
            {
                command = value;
                NotifyPropertyChanged();
            }
        }
        public string Result
        {
            get => result;
            set
            {
                result = value;
                NotifyPropertyChanged();
            }
        }

        public SetupPlateauCommand SetupPlateauCommand { get; set; }
        public SetupRoverCommand SetupRoverCommand { get; set; }
        public SendCommand SendCommand { get; set; }
        public ArrowButtonCommand ArrowButtonCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MarsRoverModel()
        {
            InitialHeading = "North";

            SetupPlateauCommand = new SetupPlateauCommand(this);
            SetupRoverCommand = new SetupRoverCommand(this);
            SendCommand = new SendCommand(this);
            ArrowButtonCommand = new ArrowButtonCommand(this);
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
