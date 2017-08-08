using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 是男人就坚持12秒
{
    /// <summary>
    /// 可独立使用或用于导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Map map;

        Block[] visBlocks;//隐藏的格格;

        List<Block> allBlocks;//所有的格格;

        List<string> allTags;//所有的Tag;

        List<Block> BlockUplists;//所有将要上升下降的格格;

        List<Block> FuBlockUplists;

        List<BlockRectangle> allBlockRectangles;//所有的UI里的格格;

        Block[] smallMans;//所有小人可以移动到的位置数据上下文组；

        List<SmallMan> allSmallMans;//所有的小人；

        //创建一个秒表;
        Stopwatch sw = new Stopwatch();

        int level = 1;//关卡计数器;
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
               

                map = new Map();
                allBlocks = new List<Block>();
                allTags = new List<string>();
                allBlockRectangles = new List<BlockRectangle>();
                smallMans = new Block[9];
                allSmallMans = new List<SmallMan>();

                for (int i = 0; i < 9; i++)
                {
                    //外层代码添加GridControlLeftRight到12个Rectangles.用来接收tapped事件控制左右走小人；
                    BlockRectangle recTapped = new BlockRectangle();
                    recTapped.Opacity = 0;
                    GridControlLeftRight.Children.Add(recTapped);
                    Grid.SetColumn(recTapped, i);
                    recTapped.Tag = i.ToString();
                    recTapped.Tapped += recTapped_Tapped;

                    for (int j = 0; j < 4; j++)
                    {
                        BlockRectangle rectangle = new BlockRectangle();

                        GridMap.Children.Add(rectangle);
                        Grid.SetRow(rectangle, j);
                        Grid.SetColumn(rectangle, i);

                        rectangle.DataContext = map[i, j];

                        rectangle.Tag = i.ToString() + j.ToString();
                        allTags.Add(rectangle.Tag);//allTags这个list似乎并没有用到。。。
                        allBlockRectangles.Add(rectangle);

                        //rectangle.Tapped += rectangle_Tapped;

                        SmallMan smallMan = new SmallMan();

                        GridSmallMan.Children.Add(smallMan);
                        Grid.SetRow(smallMan, j);
                        Grid.SetColumn(smallMan, i);

                        smallMan.DataContext = map[i, j];

                        smallMan.Tag = i.ToString() + j.ToString();
                        allSmallMans.Add(smallMan);
                        allBlocks.Add(map[i, j]);//初始化所有的格格list；或者不用这个。而转用smallManlist;
                    }
                }
            }
        }
         /// <summary>
        /// GridControlLeftRight表格里的12个Rectangle的tap事件;控制小人移动的事件；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void recTapped_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BlockRectangle rec1 = sender as BlockRectangle;
            int x = Convert.ToInt32(rec1.Tag);
            rec1.Opacity = 0.5;
            await Task.Delay(100);
            rec1.Opacity = 0;
            //遍历出活着的小人命名为b2;
            Block b2 = new Block();

            foreach (Block item in smallMans)
            {
                if (item.IsVisSmallMan == true)
                {
                    b2 = item;
                }
            }

            if (b2 == null)
            {
                foreach (Block item in allBlocks)
                {
                    item.IsVisSmallMan = false;
                }
                smallMans[4].IsVisSmallMan = true;
                b2 = smallMans[4];
            }
            else
            {
                foreach (Block item in allBlocks)
                {
                    item.IsVisSmallMan = false;
                }
                b2.IsVisSmallMan = true;
            }

            //触摸的grid.tag就是1-12；
            if (x == b2.X)
            {
                return;
            }
            else if (x > b2.X)
            {
                b2.IsVisSmallMan = false;
                await Task.Delay(20); //第一步让小人的位置隐藏，最后一步让点击的点显示。。中间步遍历循环控制。
                for (int i = b2.X + 1; i < x; i++)
                {
                    map[i, smallMans[i].Y].IsVisSmallMan = true;
                    await Task.Delay(100);
                    map[i, smallMans[i].Y].IsVisSmallMan = false;
                }//200毫秒
                await Task.Delay(20);
                map[x, smallMans[x].Y].IsVisSmallMan = true;
            }
            else if (x < b2.X)
            {
                b2.IsVisSmallMan = false;
                await Task.Delay(20);
                for (int i = b2.X - 1; i > x; i--)
                {
                    map[i, smallMans[i].Y].IsVisSmallMan = true;
                    await Task.Delay(100);
                    map[i, smallMans[i].Y].IsVisSmallMan = false;
                }
                await Task.Delay(20);
                map[x, smallMans[x].Y].IsVisSmallMan = true;
            }


            foreach (Block item in allBlocks)
            {
                item.IsVisSmallMan = false;
            }
            map[x, smallMans[x].Y].IsVisSmallMan = true;
        }


       

        //进入下一关的方法//本方法其实不用只要循环就行了开始动画和停止动画；


        /// <summary>
        /// 最后一次改于20140608下午运行正常。。。未写小人下落登场动画；；；；；；；；；；；；；；；；；；；；；；；；；；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GameOverBtn_Click(object sender, RoutedEventArgs e)
        {
            //GameOverScore.DataContext = sw;
            //txtbScore.DataContext = sw;


            GameOverGrid.Visibility = Visibility.Collapsed;

            if (GameOverBtn.Label.ToString() == "开始学习")
            {
                GameOverBtn.Label = "再走一圈";
            }
            
            //所有黑块变黑。。。小人变没。。。
            if (visBlocks != null)
            {
                foreach (Block item in visBlocks)//将没显示的黑块再次变黑
                {
                    item.IsVis = true;
                }
            }

            level = 1;

            //秒表开始计时;
            sw.Start();
            //把所有的格子放到底端去;
            foreach (BlockRectangle item in allBlockRectangles)
            {
                item.Start0To1500();
            }

            //二：生成uplists放到顶端
            //2.1得到新一关的visBlocks。。。//将新一关的visBlocks设置为不可见。
            
            if (level <= 4)
            {
                visBlocks = map.EasyBlock();
            }
            else if (level <= 10)
            {
                visBlocks = map.MediumBlock();
            }
            else
            {
                visBlocks = map.HardBlock();
            }

            foreach (Block item in visBlocks)
            {
                item.IsVis = false;
            }
            //2.2获取随机要压下来的Blocks//然后从1500放到-1500准备下一步的操作
            BlockUplists = map.UpBlocks(visBlocks);
            foreach (Block itemBlock in BlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == itemBlock.Tag);
                b1.Start1500ToFu1500();
            }

          
            //获取allblocks减去要压下来的blocks。。。得到FuBlockUplists;
            foreach (Block item in BlockUplists)
            {
                FuBlockUplists = allBlocks;
                FuBlockUplists.Remove(item);
            }

            await Task.Delay(100);

            //小人可以移动到的12个小人数组初始化
            for (int i = 0; i < 9; i++)
            {
                smallMans[i] = map[i, map.smallManY[i]];
            }
            //小人初始化;//初始化前先写一个下落的动画；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；
            smallMans[4].IsVisSmallMan = true;


            //将-1500处的uplists放下到300处//将1500处的FuBlockUplists放到0
            foreach (Block item in BlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                b1.StartFu1500ToFu300();
            }
            foreach (Block item in FuBlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                b1.Start1500To0();
            }
            //把Uplists加回到allBlocks;
            foreach (Block item in BlockUplists)
            {
                allBlocks.Add(item);
            }
            FromFu1500ToFu300.Begin();
            From1500To0.Begin();
        }


        /// <summary>
        /// 第一步。。。。-1500到-300 1500到0 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sb1_Completed(object sender, object e)
        {
            ///////////////////////////////////////////过关的话自动开始下一关//////////////////////////////////////


            //把跑到顶端的格子移到底端去。以重新生成下一关的UPLists;
            foreach (Block item in BlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                b1.StartFu1500To1500();
            }
            await Task.Delay(10);
            foreach (Block item in visBlocks)//将没显示的黑块再次变黑
            {
                item.IsVis = true;
            }
            foreach (Block item in allBlocks)
            {
                item.IsVisSmallMan = false;
            }

            //得到新一关的visBlocks。。。//将新一关的visBlocks设置为不可见。
            level++;
            if (level <= 3)
            {
                visBlocks = map.EasyBlock();
            }
            else if (level <= 8)
            {
                visBlocks = map.MediumBlock();
            }
            else
            {
                visBlocks = map.HardBlock();
            }

            foreach (Block item in visBlocks)
            {
                item.IsVis = false;
            }


            //获取随机要压下来的Blocks//然后从1500放到-1500准备下一步的操作
            BlockUplists = map.UpBlocks(visBlocks);
            foreach (Block itemBlock in BlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == itemBlock.Tag);
                b1.Start1500ToFu1500();
            }
            //获取allblocks减去要压下来的blocks。。。得到FuBlockUplists;
            foreach (Block item in BlockUplists)
            {
                FuBlockUplists = allBlocks;
                FuBlockUplists.Remove(item);
            }



            await Task.Delay(100);

   

            //小人可以移动到的12个小人数组初始化
            for (int i = 0; i < 9; i++)
            {
                smallMans[i] = map[i, map.smallManY[i]];
            }
            //小人初始化;//初始化前先写一个下落的动画；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；；
            smallMans[4].IsVisSmallMan = true;

            //将-1500处的uplists放下到300处//将1500处的FuBlockUplists放到0
            foreach (Block item in BlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                b1.StartFu1500ToFu300();
            }
            foreach (Block item in FuBlockUplists)
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                b1.Start1500To0();
            }

            //把Uplists加回到allBlocks;
            foreach (Block item in BlockUplists)
            {
                allBlocks.Add(item);
            }

            FromFu1500ToFu300.Begin();
            From1500To0.Begin();
        }


        /// <summary>
        /// 第二步:等待难度秒数后抖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FromFu1500ToFu300_Completed(object sender, object e)
        {
            //生成延时 难度速度控制
            if (level <= 4)
            {
                await Task.Delay(2500 + (1000 / level));
            }
            else if (level <= 10)
            {
                await Task.Delay(2000 + (1000 / level - 4));
            }
            else if (level <= 18)
            {
                await Task.Delay(1500 + (1000 / level - 10));
            }
            else if (level < 168)
            {
                await Task.Delay(1000 - ((level - 68) * 10));
            }

            foreach (Block itemBlock in BlockUplists)//所有要下落的格子全抖动三下;
            {
                BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == itemBlock.Tag);
                b1.StartDoudon();
            }

            FromFu300DouDon.Begin();
            await Task.Delay(100);
        }


        /// <summary>
        /// 抖动完成开始sb2.Begin()开始下压;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromFu300DouDon_Completed(object sender, object e)
        {
            sb2.Begin();
        }


        //下压完成。判断小人方位。这个是最后一步sb1.Begin();
        private async void sb2_Completed(object sender, object e)
        {

            //2014年6月7号下午到这里停止。。。。。。。。随后再写这下面的代码；；；；；；；；；；；；；；；；；；；
            //先判断小人有没过关吧？？？
            Block block = new Block();
            foreach (Block item in smallMans)
            {
                if (item.IsVisSmallMan == true)
                {
                    block = item;
                }
            }
            if (block.X == visBlocks[0].X)
            {
                //把前一关没有上去的格子取到。。。上去的是
                foreach (Block item in BlockUplists)
                {
                    FuBlockUplists = allBlocks;
                    FuBlockUplists.Remove(item);
                }
                //没有上去的格子从0下到1500
                foreach (Block item in FuBlockUplists)
                {
                    //20140608上午。从下午继续。这里写0到1500下去方块。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
                    BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                    b1.Start0To1500();
                }

                foreach (Block item in BlockUplists)
                {
                    BlockRectangle b1 = allBlockRectangles.First(retangle => retangle.Tag == item.Tag);
                    b1.Start0ToFu1500();
                }

                //把Uplists加回到allBlocks;
                foreach (Block item in BlockUplists)
                {
                    allBlocks.Add(item);
                }

                //SmallMan s1 = allSmallMans.First(smallman => smallman.Tag == block.Tag);
                //s1.StartUP();
                //Task.Delay(300);
                //s1.StartUpBack();

                sb1.Begin();
                From0To1500.Begin();
                //说明过关了。执行sb1.begin()开始下一关。在sb1完成后生成下一关的块。。。
            }
            else
            {
                GameOverScore.Text = level.ToString() + "步";
                GameOverGrid.Visibility = Visibility.Visible;
                mediaElement.Play();
                //string mm = sw.Elapsed.Minutes.ToString();
                //string ss = sw.Elapsed.Seconds.ToString();
                //await new MessageDialog("您坚持了：" + mm + "分" + ss + "秒").ShowAsync();
                #region 1-33关的语句描述
                if (level <= 3)
                {
                    switch (level)
                    {
                        case 0:
                            GameOverText.Text = "我还能说什么";
                            break;
                        case 1:
                            GameOverText.Text = "一步一风云";
                            break;
                        case 2:
                            GameOverText.Text = "2 2 2 2 2 2 2";
                            break;
                        case 3:
                            GameOverText.Text = "三步小屁孩";
                            break;
                    }
                }
                else if (level < 8)
                {
                    GameOverText.Text = "八个方位都走不全";
                }
                else if (level < 12)
                {
                    GameOverText.Text = "诶哟~不错哟";
                }

                else if (level <= 33)
                {
                    switch (level)
                    {
                        case 12:
                            GameOverText.Text = "初出茅庐";
                            break;
                        case 13:
                            GameOverText.Text = "初学乍练";
                            break;
                        case 14:
                            GameOverText.Text = "初窥门径";
                            break;
                        case 15:
                            GameOverText.Text = "小试牛刀";
                            break;
                        case 16:
                            GameOverText.Text = "略有小成";
                            break;
                        case 17:
                            GameOverText.Text = "小有成就";
                            break;
                        case 18:
                            GameOverText.Text = "驾轻就熟";
                            break;
                        case 19:
                            GameOverText.Text = "移形换位";
                            break;
                        case 20:
                            GameOverText.Text = "融会贯通";
                            break;
                        case 21:
                            GameOverText.Text = "炉火纯青";
                            break;
                        case 22:
                            GameOverText.Text = "出类拔萃";
                            break;
                        case 23:
                            GameOverText.Text = "技冠群雄";
                            break;
                        case 24:
                            GameOverText.Text = "出神入化";
                            break;
                        case 25:
                            GameOverText.Text = "傲视群雄";
                            break;
                        case 26:
                            GameOverText.Text = "登峰造极";
                            break;
                        case 27:
                            GameOverText.Text = "惊世骇俗";
                            break;
                        case 28:
                            GameOverText.Text = "震古铄今";
                            break;
                        case 29:
                            GameOverText.Text = "威镇寰宇";
                            break;
                        case 30:
                            GameOverText.Text = "空前绝后";
                            break;
                        case 31:
                            GameOverText.Text = "天人合一";
                            break;
                        case 32:
                            GameOverText.Text = "返璞归真";
                            break;
                        case 33:
                            GameOverText.Text = "牛B的人物好牛B";
                            break;
                    }

                }
                else
                {
                    GameOverText.Text = "截图````要快！";
                }
                #endregion
                sw.Stop();
                sw.Reset();

                foreach (Block item in smallMans)
                {
                    item.IsVisSmallMan = false;
                }
                //将活下来的小人干掉换个新的。
                //GameOverGrid.Canvas.ZIndex=0;//这个值取不到。明天联网再研究；--------已解决通过visibility；
                //实在不行只能单独建个GameOverPage了。
            }
        }
    }
}
//20140607下午：小人跳起来。跳到中间位置。
//上往上走下往下走 走出屏幕再回来。。回到中间准备合的时候再抖几下。。。

//至少作小人上升和下落的动画吧。先。



//小人控件和图片控件一样显示在每个格子里。
//写一个UP方法一个Down方法
//写一个左走和右走的方法。让visblock[0].X,Y+1的Tag的小人显示出来。。。的时候就成功到下一关。否则就GameOver.convas.zindex=0;


#region 生成延时难度速度控制
////生成延时 难度速度控制
//if (level <= 4)
//{
//    await Task.Delay(300 + (100 / level));
//}
//else if (level <= 10)
//{
//    await Task.Delay(250 + (100 / level - 4));
//}
//else if (level <= 18)
//{
//    await Task.Delay(150 + (100 / level - 10));
//}
//else if (level < 168)
//{
//    await Task.Delay(150 - (level - 18));
//}

#endregion

//20140608下午UI出现问题Bug有可能是FuUplists=allBlocks的问题也可能是1500到0以前没有弄好。
//解答：结果的确是fuuplists=allblocks的问题。。。。。

//小人跳起和落下的动画好乱好晕。。。
//如果没过关。gameover的时候上半半就变成平整的了。郁闷ing.
