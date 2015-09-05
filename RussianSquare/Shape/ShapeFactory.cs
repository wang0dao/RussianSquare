using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RussianSquare.Shape
{
    public class ShapeFactory
    {
        public static SuperShape CreateShape(string shapeName, Graphics graSrc, Color shapeCol, Color backCol, Rectangle framRec, int squareSize)
        {
            SuperShape shape = null;

            switch (shapeName)
            {
                case "LLeft":
                    shape = new LLeftShape(graSrc, shapeCol, backCol, framRec, squareSize);
                    break;
                //case "LRight":
                //    shape = new LRightShpe();
                //    break;
                //case "I":
                //    shape = new IShape();
                //    break;
                //case "M":
                //    shape = new MShape();
                //    break;
                //case "O":
                //    shape = new OShape();
                //    break;
                //case "ZLeft":
                //    shape = new ZLeftShape();
                //    break;
                //case "ZRight":
                //    shape = new ZRightShape();
                //    break;
                default: 
                    break;  
            }

            return shape;
        }

        public enum ShapeEnum
        {
            IShape,
            MShape,
            OShape,
            LLShape,
            LRShape,
            ZLShape,
            ZRShape,
        }
    }
}
