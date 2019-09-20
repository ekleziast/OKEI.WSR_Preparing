using esoft.Entity;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace esoft.Model
{
    class AgentModel : INotifyPropertyChanged
    {
        private int dealShare;

        public int DealShare
        {
            get
            {
                return dealShare;
            }
            set
            {
                dealShare = value;
                OnPropertyChanged("DealShare");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
