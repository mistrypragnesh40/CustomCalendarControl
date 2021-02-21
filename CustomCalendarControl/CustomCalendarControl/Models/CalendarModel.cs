using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CustomCalendarControl.Models
{
    public class CalendarModel : INotifyPropertyChanged
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string DayName { get; set; }
        public int DateInNumber { get; set; }
        public string DateInNumberStr { get; set; }

        private bool _currentDate;
        public bool CurrentDate
        {
            get => _currentDate; set
            {
                _currentDate = value; OnPropertyChanged();
            }
        }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyValue = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyValue));
        }
    }
}
