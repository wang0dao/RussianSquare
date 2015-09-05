using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RussianSquare.Shape
{
    public abstract class SuperShape
    {
        /// <summary>
        /// 横向位置（单位px）
        /// </summary>
        public int xPosition;
        /// <summary>
        /// 纵向位置
        /// </summary>
        public int yPosition;
        /// <summary>
        /// 方块尺寸（单位px）
        /// </summary>
        public int squareSize;
        /// <summary>
        /// 方块状态
        /// </summary>
        public ShapeState shapeState;
        /// <summary>
        /// 界面x轴长度
        /// </summary>
        protected int xLength;
        /// <summary>
        /// 界面y轴长度
        /// </summary>
        protected int yLength;
        /// <summary>
        /// 横向总步数
        /// </summary>
        protected int xCount;
        /// <summary>
        /// 纵向总步数
        /// </summary>
        protected int yCount;
        /// <summary>
        /// 起始横向步数
        /// </summary>
        protected int xStep;
        /// <summary>
        /// 起始纵向步数
        /// </summary>
        protected int yStep;
        /// <summary>
        /// 方块颜色
        /// </summary>
        public Color shapeColor;
        /// <summary>
        /// 初始化用，设置方块背景颜色
        /// </summary>
        public Color backColor;
        /// <summary>
        /// 初始化用，设置方块画布
        /// </summary>
        public Graphics graSrc;
        /// <summary>
        /// 方块状态枚举
        /// </summary>
        public enum ShapeState
        {
            Up,
            Down,
            Left,
            Right
        }
        public int[,] heapArr;
        public bool overFlg;

        /// <summary>
        /// 初始化
        /// </summary>1
        /// <param name="画布源"></param>
        /// <param name="颜色"></param>
        /// <param name="背景色"></param>
        /// <param name="框架尺寸"></param>
        /// <param name="方块尺寸"></param>
        public void InitRussian(Graphics graSrc, Color shapeCol, Color backCol,Rectangle framRec, int squareSize)
        {
            // 画步
            this.graSrc = graSrc;
            // 颜色
            this.shapeColor = shapeCol;
            this.backColor = backCol;
            // 方块尺寸
            this.squareSize = squareSize;
            // 框架尺寸
            this.xLength = framRec.Width;
            this.yLength = framRec.Height;    
            // 两轴方块个数
            this.xCount = framRec.Width / this.squareSize;
            this.yCount = framRec.Height / this.squareSize;     
            // 初始步数
            this.xStep = this.xCount / 2;
            this.yStep = 0; 
            // 初始位置
            this.xPosition = squareSize * this.xStep;
            this.yPosition = squareSize * (yStep - 1);          
            // 随机获取方块状态
            this.shapeState = GetShpaeState();
            heapArr = new int[xCount + 1, yCount + 1];
        }
        /// <summary>
        /// 建造方块
        /// </summary>
        public abstract void CreateShape();
        /// <summary>
        /// 清除方块
        /// </summary>
        public abstract void ClearShape();
        /// <summary>
        /// 方块变形
        /// </summary>
        public abstract void ChangeShapeState();
        /// <summary>
        /// 向下移动
        /// </summary>
        public abstract int MoveDown();
        /// <summary>
        /// 向左移动
        /// </summary>
        public abstract void MoveLeft();
        /// <summary>
        /// 向右移动
        /// </summary>
        public abstract void MoveRight();
        /// <summary>
        /// 方块碰撞
        /// </summary>
        public abstract void DoCollide();
        public abstract void HeapUp();

        public ShapeState GetShpaeState()
        {
            ShapeState shapeState;
            Random rd = new Random();
            int randIndex = rd.Next(1, 3);

            if (randIndex == 1)
            {
                shapeState = ShapeState.Up;
            }
            else
            {
                shapeState = ShapeState.Down;
            }

            return shapeState;
        } 
        
        
    }
}
