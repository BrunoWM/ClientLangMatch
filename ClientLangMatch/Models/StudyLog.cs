using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLangMatch.Models
{
    public class StudyLog : INotifyPropertyChanged
    {
        int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value) return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        int _timeSpent;
        public int TimeSpent
        {
            get => _timeSpent;
            set
            {
                if (_timeSpent != value) return;

                _timeSpent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeSpent)));
            }
        }
        string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value) return;

                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
