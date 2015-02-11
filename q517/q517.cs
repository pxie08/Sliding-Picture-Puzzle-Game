/***********************************
** q517.cs
** Raiders of the Lost Puzzle Piece
** Patrick Xie, 5/17/2011
***********************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace q517
{
    public partial class q517 : Form
    {
        private PictureBox mainPic;
        private object[] pieces = new object[9];
        private System.Collections.Hashtable pieceLoc = new System.Collections.Hashtable(9);

        public q517()
        {
            InitializeComponent();
        }
        /*creates peices of the picture*/
        private void createPieces()
        {
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(100, 100);
                    if (index == 8)
                    {
                        pic.BorderStyle = BorderStyle.None;
                        pic.BackColor = Color.Transparent;
                    }
                    else
                    {
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.BackColor = Color.Transparent;
                    }
                    pic.Name = string.Format("piece{0}", index);
                    pic.Click += new EventHandler(pic_Click);
                    pic.Tag = index;
                    pieces[index] = pic;
                    index++;
                }
            }
        }
        /*sets each piece ot have a part of the picture*/
        private void getPieces()
        {
            mainPic = new PictureBox();
            mainPic.Size = new Size(300, 300);
            mainPic.Location = new Point(0, 0);
            mainPic.Image = Properties.Resources.simpsons;
            mainPic.SizeMode = PictureBoxSizeMode.StretchImage;
            int top = 0;
            int left = 0;
            int k = 0;
            Bitmap myCanvas = new Bitmap(mainPic.Image);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    PictureBox pieceOfPic = (PictureBox)pieces[k];
                    if (k == 8)
                    {
                        pieceOfPic.Image = Properties.Resources.blank;
                    }
                    else
                    {
                        pieceOfPic.Image = myCanvas.Clone(new Rectangle(left, top, 100, 100), System.Drawing.Imaging.PixelFormat.DontCare);
                    }
                    left += 100;
                    k++;
                }
                left = 0;
                top += 100;
            }
        }
        /*shuffles the board*/
        private void shufflePic()
        {
            pieceLoc.Clear();
            this.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rand = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable(9);
            for (int i = 0; i < 9; i++)
            {
                int newPiece = rand.Next(0, 9);
                while (gen.Contains(newPiece))
                {
                    newPiece = rand.Next(0,9);
                }
                PictureBox pic = (PictureBox)pieces[newPiece];
                pic.Location = new Point(left, top);
                this.Controls.Add(pic);
                pieceLoc.Add(pic.Tag, i);
                gen.Add(newPiece, newPiece);
                left += 100;
                if ((i + 1) % 3 == 0)
                {
                    left = 0;
                    top += 100;
                }
            }
        }

        /*swap locations of the picture clicked on and empty space*/
        private void pic_Click(object sender, EventArgs e)
        {
            PictureBox piecePic = (PictureBox)pieces[8];
            PictureBox currentpiece = (PictureBox)sender;
            if ((piecePic.Left == currentpiece.Left - 100) && (piecePic.Top == currentpiece.Top))
            {
                for (int i = 1; i <= 100; i++)
                {
                    piecePic.Left++;
                    currentpiece.Left--;
                }
            }
            else if ((piecePic.Top == currentpiece.Top - 100) && (piecePic.Left == currentpiece.Left))
            {
                for (int i = 1; i <= 100; i++)
                {
                    piecePic.Top++;
                    currentpiece.Top--;
                }
            }
            else if ((piecePic.Top == currentpiece.Top + 100) && (piecePic.Left == currentpiece.Left))
            {
                for (int i = 1; i <= 100; i++)
                {
                    piecePic.Top--;
                    currentpiece.Top++;
                }
            }
            else if ((piecePic.Left == currentpiece.Left + 100) && (piecePic.Top == currentpiece.Top))
            {
                for (int i = 1; i <= 100; i++)
                {
                    piecePic.Left--;
                    currentpiece.Left++;
                }
            }
            else
            {
               
            }
            swaptag(ref piecePic, ref currentpiece);
        }
        /*swaps tag of picture piece between two pieces*/
        private void swaptag(ref PictureBox A, ref PictureBox B)
        {
            object keeptag = null;
            keeptag = pieceLoc[A.Tag];
            pieceLoc[A.Tag] = pieceLoc[B.Tag];
            pieceLoc[B.Tag] = keeptag;
        }
        /*Add Pictures to the form*/
        private void addpictures()
        {
            pieceLoc.Clear();
            this.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rand = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable(25);
            for (int i = 0; i < 9; i++)
            {
                PictureBox pic = (PictureBox)pieces[i];
                pic.Location = new Point(left, top);
                this.Controls.Add(pic);
                pieceLoc.Add(pic.Tag, i);
                gen.Add(i, i);
                left += 100;
                if ((i + 1) % 3 == 0)
                {
                    left = 0;
                    top += 100;
                }
            }
        }

        private void q517_Load(object sender, EventArgs e)
        {
            createPieces();
            getPieces();
            addpictures();
            timer1.Start();
        }
        /*Timer to shuffle the board in 5 seconds*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            shufflePic();
            timer1.Stop();
        }

    }
}