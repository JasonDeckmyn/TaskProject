using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskViewer.Models
{
    public class TodoModel : INotifyPropertyChanged
    {
        private bool _isComplete;
        private string _title;
        private string _description;
        private UrgencyLevel _urgency;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public UrgencyLevel Urgency
        {
            get => _urgency;
            set
            {
                if (_urgency != value)
                {
                    _urgency = value;
                    OnPropertyChanged(nameof(Urgency));
                }
            }
        }
        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                if (_isComplete != value)
                {
                    _isComplete = value;
                    OnPropertyChanged(nameof(IsComplete));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum UrgencyLevel
    {
        Low,
        Medium,
        High
    }
}
