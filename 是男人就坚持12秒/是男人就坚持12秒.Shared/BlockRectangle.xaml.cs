using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace 是男人就坚持12秒
{
    public sealed partial class BlockRectangle : UserControl
    {
        public BlockRectangle()
        {
            this.InitializeComponent();
        }

        public new string Tag
        {
            get { return (string)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        public void StartFlash()
        {
            FromFu1500ToFu300.Begin();
        }




        public void StartDoudon()
        {
            FromFu300DouDon.Begin();
        }

        private void FromFu300DouDon_Completed(object sender, object e)
        {
            sb2.Begin();
        }


        public void StartFu1500ToFu300()
        {
            FromFu1500ToFu300.Begin();
        }


        public void StartFu1500To0()
        {
            FromFu1500To0.Begin();
        }
        public void Start0ToFu1500()
        {
            sb1.Begin();
        }
        public void Start1500ToFu1500()
        {
            From1500ToFu1500.Begin();
        }
        public void StartFu1500To1500()
        {
            FromFu1500To1500.Begin();
        }
        public void Start0To1500()
        {
            From0To1500.Begin();
        }
        public void Start1500To0()
        {
            From1500To0.Begin();
        }
        public void RecTheme(SolidColorBrush recColBrush)
        {
            rectangle.Fill = recColBrush;
        }
    }
}
