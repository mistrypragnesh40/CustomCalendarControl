using CustomCalendarControl.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomCalendarControl.ViewModels
{
    public class CalendarPageViewModel : INotifyPropertyChanged
    {
        #region Properties & Event
        public event PropertyChangedEventHandler PropertyChanged;

        int MaxYear;
        int MinYear;

        public static List<CalendarModel> _staticCalendarList;
        public static List<int> dummyYearList;
        public ObservableRangeCollection<int> YearList { get; } = new ObservableRangeCollection<int>();
        public ObservableRangeCollection<Grouping<CalendarHeader, CalendarModel>> CalendarList { get; } = new ObservableRangeCollection<Grouping<CalendarHeader, CalendarModel>>();


        private bool _isCalendarDetailsVisible = true;
        public bool IsCalendarDetailsVisible
        {
            get => _isCalendarDetailsVisible;
            set { _isCalendarDetailsVisible = value; OnPropertyChanged(); }
        }


        private string _currentMonthYear = string.Empty;
        public string CurrentMonthYear
        {
            get => _currentMonthYear;
            set { _currentMonthYear = value; OnPropertyChanged(); }
        }

        private int _currentYear;
        public int CurrentYear
        {
            get => _currentYear;
            set { _currentYear = value; OnPropertyChanged(); }
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value; OnPropertyChanged();
            }
        }

        private DateTime? _selectedDate { get; set; }
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value; OnPropertyChanged();
            }
        }

        private string _selectedDateInString;
        public string SelectedDateInString
        {
            get => _selectedDateInString;
            set { _selectedDateInString = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public CalendarPageViewModel(DateTime _selectedDate)
        {
            AddDates();
            BindDates(_selectedDate.Year, _selectedDate.Month, _selectedDate.ToString("MMM"), _selectedDate);
        }

        public CalendarPageViewModel()
        {

        }

        #endregion

        #region Methods
        private void AddDates()
        {
            MaxYear = DateTime.Now.Year + 30;
            MinYear = DateTime.Now.Year - 100;

            if (dummyYearList == null)
                dummyYearList = new List<int>();

            if (_staticCalendarList == null)
                _staticCalendarList = new List<CalendarModel>();

            if (_staticCalendarList.Count == 0)
            {

                for (int year = MinYear; year <= MaxYear; year++)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        int daysCount = DateTime.DaysInMonth(year, month);
                        var dates = new List<CalendarModel>();
                        for (int days = 1; days <= daysCount; days++)
                        {
                            DateTime date = new DateTime(year, month, days);
                            CalendarModel obj = new CalendarModel();
                            obj.Date = date;
                            obj.Year = year;
                            obj.Month = month;
                            obj.DateInNumber = days;
                            obj.DateInNumberStr = days + "";
                            obj.DayName = date.ToString("ddd");
                            dates.Add(obj);
                        }
                        _staticCalendarList.AddRange(dates);
                    }
                    dummyYearList.Add(year);
                }
            }
            else
            {
                if (dummyYearList.Count == 0)
                {
                    for (int year = MinYear; year <= MaxYear; year++)
                    {
                        dummyYearList.Add(year);
                    }
                }
                var selectedDates = _staticCalendarList.Where(f => f.CurrentDate == true).ToList();
                selectedDates.ForEach(f => { f.CurrentDate = false; });
            }
            YearList.AddRange(dummyYearList.OrderByDescending(f => f).ToList());
        }
        public void BindDates(int year, int month, string MonthName, DateTime? newDate)
        {
            CalendarList.Clear();
            bool isFoundStartDate = false;
            int days = DateTime.DaysInMonth(year, month);
            CurrentMonthYear = MonthName + "  " + year.ToString();

            var dates = _staticCalendarList.Where(f => f.Year == year && f.Month == month).ToList();
            if (dates.Count > 0)
            {
                if (newDate.HasValue)
                {
                    var currentDate = dates.Where(f => f.DateInNumber == newDate?.Day).FirstOrDefault();
                    currentDate.CurrentDate = true;
                    SelectedDate = newDate.Value;
                    CurrentDate = newDate.Value;
                    CurrentYear = year;
                    SelectedDateInString = CurrentDate.ToString("ddd, MMMM dd");
                }

                string startDayName = dates.Where(f => f.DateInNumber == 1).FirstOrDefault().DayName.ToLower();

                var list = new List<CalendarModel>();
                var header = new CalendarHeader();
                header.DayName = "Sun";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));

                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Mon";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));


                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Tue";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));

                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Wed";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));

                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Thu";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));

                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Fri";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));


                list.Clear();
                header = new CalendarHeader();
                header.DayName = "Sat";
                if (startDayName != header.DayName.ToLower() && !isFoundStartDate)
                {
                    list.Add(new CalendarModel());
                }
                else
                {
                    isFoundStartDate = true;
                }
                list.AddRange(dates.Where(f => f.DayName.ToLower() == header.DayName.ToLower()).ToList());
                CalendarList.Add(new Grouping<CalendarHeader, CalendarModel>(header, list));
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyValue = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyValue));
        }
        #endregion

        #region Commands
        // used for Traverse through date
        public ICommand CurrentDateCommand
        {
            get
            {
                return new Command<CalendarModel>((item) =>
                {
                    if (item?.DayName != null)
                    {
                        var selectedDates = _staticCalendarList.Where(f => f.CurrentDate == true).ToList();
                        selectedDates.ForEach(f => { f.CurrentDate = false; });

                        CurrentDate = item.Date;
                        CurrentYear = item.Date.Year;
                        item.CurrentDate = true;
                        SelectedDateInString = item.Date.ToString("ddd, dd MMMM");
                        SelectedDate = CurrentDate;
                    }

                });
            }
        }


        //Executed on the Tap of Cancel Button.
        public ICommand CancelCommand
        {
            get
            {
                return new Command(() =>
                {
                    SelectedDate = null;
                });
            }
        }

        //Executed on the Tap of > Sign.
        public ICommand NextMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CurrentDate != null)
                    {
                        bool isMaxDateReach = false;
                        if (MaxYear == CurrentDate.Year && CurrentDate.Month == 12)
                        {
                            isMaxDateReach = true;
                        }
                        if (!isMaxDateReach)
                        {
                            CurrentDate = CurrentDate.Date.AddMonths(1);
                            BindDates(CurrentDate.Year, CurrentDate.Month, CurrentDate.ToString("MMM"), null);
                        }
                    }
                });
            }
        }

        //Executed on the Tap of < Sign.
        public ICommand PreviousMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CurrentDate != null)
                    {
                        bool isMinDateReach = false;
                        if (MinYear == CurrentDate.Year && CurrentDate.Month == 1)
                        {
                            isMinDateReach = true;
                        }
                        if (!isMinDateReach)
                        {
                            CurrentDate = CurrentDate.Date.AddMonths(-1);
                            BindDates(CurrentDate.Year, CurrentDate.Month, CurrentDate.ToString("MMM"), null);
                        }
                    }
                });
            }
        }


        public ICommand YearSelectionCommand
        {
            get
            {
                return new Command<int>((year) =>
                {
                    if (year > 0 && SelectedDate.HasValue)
                    {
                        DateTime newDate;
                        try
                        {
                            newDate = new DateTime(year, (int)SelectedDate?.Month, (int)SelectedDate?.Day);

                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            newDate = new DateTime(year, (int)SelectedDate?.Month, (int)SelectedDate?.Day - 1);
                        }

                        BindDates(year, newDate.Month, newDate.ToString("MMM"), newDate);
                        IsCalendarDetailsVisible = true;
                    }
                });
            }
        }

        public ICommand ShowYearListCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CurrentDate != null)
                    {
                        IsCalendarDetailsVisible = !IsCalendarDetailsVisible;
                    }
                });
            }
        }
        #endregion
    }
}
