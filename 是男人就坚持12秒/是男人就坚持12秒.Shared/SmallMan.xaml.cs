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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace 是男人就坚持12秒
{
    public sealed partial class SmallMan : UserControl
    {
        public SmallMan()
        {
            this.InitializeComponent();
        }
        public SmallMan(int photoName)
        {
            this.InitializeComponent();
            imgPhoto.Source = new BitmapImage(new Uri("ms-appx:///Images/" + photoName.ToString() + ".png"));
        }
        public new string Tag
        {
            get { return (string)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }
        public void StartUP()//开始跳起;
        {
            smallManUP.Begin();
        }
        public void StartDown()//开始下落;
        {
            smallManDown.Begin();
        }

        /// <summary>
        /// 更新小人照片;
        /// </summary>
        /// <param name="photoName"></param>
        public void UpdatePhoto(int photoName)
        {
            imgPhoto.Source = new BitmapImage(new Uri("ms-appx:///Images/" + photoName.ToString() + ".png"));
        }

        /// <summary>
        /// 小人开始变大;
        /// </summary>
        public void StartBig()
        {
            scale.CenterX = imgPhoto.ActualWidth / 2;
            scale.CenterY = imgPhoto.ActualHeight / 3;
            manToBig.Begin();
        }
        /// <summary>
        /// 小人开始变小;
        /// </summary>
        public void StartSmall()
        {
            scale.CenterX = imgPhoto.ActualWidth / 2;
            scale.CenterY = imgPhoto.ActualHeight / 3;
            manToSmall.Begin();
        }
        //public void VisPhotoBackGround()//显示出小人背景的方法;
        //{
        //    imgPhotoBackGround.Visibility = Visibility.Visible;
        //}
        //public void ColPhotoBackGround()//隐藏起小人背景的方法;
        //{
        //    imgPhotoBackGround.Visibility = Visibility.Collapsed;
        //}

        private void smallManDown_Completed(object sender, object e)
        {
            smallManDown2.Begin();
        }

        public void StartUpBack()
        {
            smallManUPBack.Begin();
        }
    }
}
