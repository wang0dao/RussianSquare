using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RussianSquare.Shape;

namespace RussianSquare
{
    public partial class Form1 : Form
    {
        private const int xCount = 12;
        private const int yCount = 21;
        private Color shapeColor = Color.White;
        private Color backColor = Color.Black;
        //private Rectangle shapeRec = new Rectangle(99,-33,33,33);
        //private Rectangle framRec = new Rectangle(0, 0, 396, 693);
        private Rectangle framRec;
        private Rectangle privewRec;
        private int squareSize;
        private Graphics graSrc;
        private Graphics previewSrc;

        private SuperShape shape;
        private SuperShape shapePreview;
        private int xStep = 0;
        private int yStep = 0;

        private bool overFlg = false;
        private bool startFlg = false;
        private bool pauseFlg;
        private int[,] heapArr;
        

        private int[,] positionArr = new int[xCount, yCount];

        public Form1()
        {
            InitializeComponent();
            int xLength = panel1.Width;
            int yLength = panel1.Height;
            framRec = new Rectangle(0, 0, xLength, yLength);
            squareSize = xLength / xCount;
            previewSrc = panel2.CreateGraphics();
            privewRec = new Rectangle(0, 0, squareSize * 2, squareSize * 2);
            heapArr = new int[xCount+1,yCount+1];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!startFlg)
            {
                startFlg = true;

                if (graSrc == null)
                {
                    graSrc = panel1.CreateGraphics();
                }

                timer1.Enabled = true;

                shape = ShapeFactory.CreateShape("LLeft", graSrc, shapeColor, backColor, framRec, squareSize);
                shape.heapArr = heapArr;
                shape.CreateShape();
            }
            else
            {
                if (pauseFlg)
                {
                    pauseFlg = false;
                    timer1.Enabled = true;
                }
                else
                {
                    pauseFlg = true;
                    timer1.Enabled = false;
                }
            }
            
            
            
            //shapePreview = ShapeFactory.CreateShape("LLeft", previewSrc, shapeColor, backColor, privewRec, squareSize);
            //shapePreview.shapeState = shape.shapeState;
            //shapePreview.CreateShape();
            
            //shape

            //graSrc.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                shape.ChangeShapeState();
            }

            if (e.KeyCode == Keys.Down)
            {
                int downFlg = shape.MoveDown();

                if (downFlg == 1)
                {
                    heapArr = shape.heapArr;
                    shape = null;
                    shape = ShapeFactory.CreateShape("LLeft", graSrc, shapeColor, backColor, framRec, squareSize);
                    shape.CreateShape();
                    shape.heapArr = heapArr;

                }

                if (downFlg == -1)
                {
                    MessageBox.Show("GAME OVER");
                }
                
                
            }

            if (e.KeyCode == Keys.Left)
            {
                shape.MoveLeft();
            }
            if (e.KeyCode == Keys.Right)
            {
                shape.MoveRight();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }
    }
}
