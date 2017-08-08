using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace 是男人就坚持12秒
{
    public class Block : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private bool _isVis = true;
        private string _tag;
        private bool _isVisSmallMan = false;

        public bool IsVisSmallMan
        {
            get { return _isVisSmallMan; }
            set
            {
                _isVisSmallMan = value;
                FirePropertyChanged("IsVisSmallMan");
            }
        }

        public string Tag
        {
            get { return this.X.ToString() + this.Y.ToString(); }
            set
            {
                _tag = value;
                FirePropertyChanged("Tag");
            }
        }





        //private string _tag;

        //public string Tag
        //{
        //    get { return this.X.ToString() + this.Y.ToString(); }
        //    set
        //    {
        //        _tag = value;//this.X.ToString()+this.Y.ToString();
        //        FirePropertyChanged("Tag");
        //    }
        //}



        public bool IsVis
        {
            get { return _isVis; }
            set
            {
                _isVis = value;
                FirePropertyChanged("IsVis");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                FirePropertyChanged("Y");
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                FirePropertyChanged("X");
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
