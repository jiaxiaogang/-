using System;
using System.Collections.Generic;
using System.Text;

namespace 是男人就坚持12秒
{
    public class Map
    {
        Block[,] data = null;

        public int[] smallManY = new int[9];//标识每个小人应该出现的Y轴；

        public Map()
        {
            data = new Block[9, 4];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    data[i, j] = new Block() { X = i, Y = j, IsVis = true };
                }
            }
        }



        //索引器先写上吧用不用再说
        public Block this[int x, int y]
        {
            get
            {
                return data[x, y];
            }
        }


        //随机产生一个不显示的困难度块;
        public Block[] HardBlock()
        {
            Block[] blocks = new Block[1];
            Random r = new Random();
            int i = r.Next(0, 9);
            int j = r.Next(0, 4);
            blocks[0] = data[i, j];
            return blocks;


        }


        //随机产生一个不显示的中等难度块x,y和x,y+1;
        public Block[] MediumBlock()
        {
            Random r = new Random();
            int i = r.Next(0, 9);
            int j = r.Next(1, 4);
            Block[] blocks = new Block[2];
            blocks[0] = data[i, j];
            blocks[1] = data[i, j - 1 ];
            return blocks;
        }

        //随机产生一个不显示的简单难度块x,y和x,y+1和x,y+2;

        public Block[] EasyBlock()
        {
            Random r = new Random();
            int i = r.Next(0, 9);
            int j = r.Next(2, 4);
            Block[] blocks = new Block[3];
            blocks[0] = data[i, j];
            blocks[1] = data[i, j - 1];
            blocks[2] = data[i, j - 2];
            return blocks;
        }


        //随机生成往上升的组;
        public List<Block> UpBlocks(Block[] b)
        {
            List<Block> blocks = new List<Block>();
            Random r = new Random();
            for (int i = 0; i < 9; i++)
			{
			 if(i==b[0].X)
             {
                 for (int j = b[0].Y; j >= 0; j--)
			    {
	                blocks.Add(data[i,j]);		 
                    
			    }
                  smallManY[i]=b[0].Y;
             }
             else
             {
                 int num = r.Next(0, 4);
                 for (int j = num; j >=0 ; j--)
                 {
                     blocks.Add(data[i, j]);
                 }
                 smallManY[i] = num;
             }
			}

            return blocks;
        }

    }
}
