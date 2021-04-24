using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MODEL
{
    internal class progressbar
    {
    }

    public class Secont : INotifyPropertyChanged
    {
        private string status;

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                this.status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        private string note1;

        public string Note1
        {
            get
            {
                return note1;
            }
            set
            {
                this.note1 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Note1"));
                }
            }
        }

        private string statusname;

        public string Statusname
        {
            get
            {
                return statusname;
            }
            set
            {
                this.statusname = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Statusname"));
                }
            }
        }


        private int value;

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }


        private string note2;

        public string Note2
        {
            get
            {
                return note2;
            }
            set
            {
                this.note2 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Note2"));
                }
            }
        }

        private int maxvalue;

        public int Maxvalue
        {
            get
            {
                return maxvalue;
            }
            set
            {
                this.maxvalue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Maxvalue"));
                }
            }
        }

        private string now;

        public string Now
        {
            get
            {
                return now;
            }
            set
            {
                this.now = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Now"));
                }
            }
        }

        private string all;

        public string All
        {
            get
            {
                return all;
            }
            set
            {
                this.all = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("All"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }

}
