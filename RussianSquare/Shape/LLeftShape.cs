using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RussianSquare.Shape
{
    public class LLeftShape:SuperShape
    {
        public LLeftShape(Graphics graSrc, Color shapeCol, Color backCol, Rectangle framRec, int squareSize)
        {
            InitRussian(graSrc, shapeCol, backCol, framRec, squareSize);
        }

        /// <summary>
        /// 建造方块
        /// </summary>
        public override void CreateShape()
        {
            Pen pen = new Pen(backColor);
            SolidBrush sob = new SolidBrush(shapeColor);

            try
            {
                if (shapeState == ShapeState.Down)
                {
                    graSrc.FillRectangle(sob, xPosition, yPosition, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition + squareSize, yPosition, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition, yPosition - squareSize, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition - squareSize, yPosition - squareSize, squareSize, squareSize);

                    graSrc.DrawRectangle(pen, xPosition, yPosition, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition + squareSize, yPosition, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition, yPosition - squareSize, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition - squareSize, yPosition - squareSize, squareSize, squareSize);
                }

                if (shapeState == ShapeState.Up)
                {
                    graSrc.FillRectangle(sob, xPosition, yPosition, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition, yPosition - squareSize, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition + squareSize, yPosition - squareSize, squareSize, squareSize);
                    graSrc.FillRectangle(sob, xPosition + squareSize, yPosition - squareSize * 2, squareSize, squareSize);

                    graSrc.DrawRectangle(pen, xPosition, yPosition, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition, yPosition - squareSize, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition + squareSize, yPosition - squareSize, squareSize, squareSize);
                    graSrc.DrawRectangle(pen, xPosition + squareSize, yPosition - squareSize * 2, squareSize, squareSize);
                }

                pen.Dispose();
                sob.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 清除方块
        /// </summary>
        public override void ClearShape()
        {
            Color tmpColor = shapeColor;
            shapeColor = backColor;

            CreateShape();
            shapeColor = tmpColor;
        }

        /// <summary>
        /// 变形
        /// </summary>
        public override void ChangeShapeState()
        {
            ClearShape();
            
            if (shapeState == ShapeState.Up)
            {
                shapeState = ShapeState.Down;
            }
            else
            {
                shapeState = ShapeState.Up;
            }

            CreateShape();
        }

        /// <summary>
        /// 向下移动
        /// </summary>
        public override int MoveDown()
        {
            if (shapeState == ShapeState.Down)
            {
                if (yStep >= yCount)
                {
                    // 碰到边框
                    heapArr[xStep, yStep] = 1;
                    heapArr[xStep + 1, yStep] = 1;
                    heapArr[xStep, yStep - 1] = 1;
                    heapArr[xStep - 1, yStep - 1] = 1;

                    return 1;
                }

                if (yStep == 0 || yStep ==1)
                {
                    if (heapArr[xStep, yStep + 1] == 1 || heapArr[xStep + 1, yStep + 1] == 1 || heapArr[xStep-1, yStep ]==1)
                    {
                        // 方块溢出
                        return -1;
                    }
                }
                else
                {
                    if (heapArr[xStep, yStep + 1] == 1 || heapArr[xStep + 1, yStep + 1] == 1 || heapArr[xStep - 1, yStep] == 1)
                    {
                        // 方块碰撞
                        heapArr[xStep, yStep] = 1;
                        heapArr[xStep + 1, yStep] = 1;
                        heapArr[xStep, yStep - 1] = 1;
                        heapArr[xStep - 1, yStep - 1] = 1;

                        return 1;
                    }
                }           
            }

            if ( shapeState == ShapeState.Up)
            {
                if (yStep >= yCount)
                {
                    heapArr[xStep, yStep] = 1;
                    heapArr[xStep, yStep - 1] = 1;
                    heapArr[xStep + 1, yStep - 1] = 1;
                    heapArr[xStep + 1, yStep - 2] = 1;
                    return 1;
                }

                if (yStep == 0)
                {
                    if (heapArr[xStep, yStep + 1] == 1)
                    {
                        // 方块溢出
                        return -1;
                    }
                }
                else if (yStep == 1 || yStep == 2)
                {
                    if (heapArr[xStep, yStep + 1] == 1 || heapArr[xStep + 1, yStep] == 1)
                    {
                        // 方块溢出
                        return -1;
                    }
                }
                else
                {
                    if (heapArr[xStep, yStep + 1] == 1 || heapArr[xStep + 1, yStep] == 1)
                    {
                        heapArr[xStep, yStep] = 1;
                        heapArr[xStep, yStep - 1] = 1;
                        heapArr[xStep+1, yStep - 1] = 1;
                        heapArr[xStep+1, yStep - 2] = 1;

                        return 1;
                    }
                }
            }

            ClearShape();
            yStep++;
            yPosition += squareSize;
            CreateShape();

            return 0;
        }

        /// <summary>
        /// 向左移动
        /// </summary>
        public override void MoveLeft()
        {
            if (yStep == 0)
            {
                return;
            }

            if (shapeState == ShapeState.Down)
            {
                if (xStep <= 1 || heapArr[xStep - 2,yStep-1]==1||heapArr[xStep-1 ,yStep]==1)
                {
                    return;
                }
            }

            if (shapeState == ShapeState.Up)
            {
                if (xStep <= 0 || heapArr[xStep-1, yStep] == 1 || heapArr[xStep-1, yStep-1] == 1)
                {
                    return;
                }
            }

            xStep--;
            ClearShape();
            xPosition -= squareSize;
            CreateShape();
        }

        /// <summary>
        /// 向右移动
        /// </summary>
        public override void MoveRight()
        {
            if (yStep == 0)
            {
                return;
            }

            if (shapeState == ShapeState.Down)
            {
                //if(xStep >= yCount || heapArr[x])
            }

            if (shapeState == ShapeState.Up || shapeState == ShapeState.Down)
            {
                if (xStep < xCount-2)
                {
                    xStep++;
                    ClearShape();
                    xPosition += squareSize;
                    CreateShape();
                }
            }
        }

        public override void HeapUp()
        {
            throw new NotImplementedException();
        }

        public override void DoCollide()
        {
            throw new NotImplementedException();
        }

        public enum MoveDirection
        {
            Down,
            Left,
            Right
        }


    }
}
