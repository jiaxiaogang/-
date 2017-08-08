using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace 是男人就坚持12秒
{
    public class Theme : INotifyPropertyChanged
    {
        private string _name;
        private SolidColorBrush _themeBackground;

        public SolidColorBrush ThemeBackground
        {
            get { return _themeBackground; }
            set
            {
                if (_themeBackground != value)
                {
                    _themeBackground = value;
                    FirePropertyChanged("ThemeBackground");
                }
            }
        }
     

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                FirePropertyChanged("Name");
            }
        }
        private void FirePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
