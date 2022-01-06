﻿
using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace Quantum.Drawing
{
    public class GraphicalConsole : Console
    {

        public Graphics graphics;


        public GraphicalConsole()
        {
            graphics = new Graphics();

            Name = graphics.canvas.Name();
            Type = ConsoleType.Graphical;

            mWidth = graphics.canvas.Mode.Columns;
            mHeight = graphics.canvas.Mode.Rows;

            mCols = graphics.canvas.Mode.Columns / graphics.font.Width;
            mRows = graphics.canvas.Mode.Rows / graphics.font.Height;
        }

        protected int mX = 0;
        public override int X
        {
            get { return mX; }
            set
            {
                mX = value;
                UpdateCursor();
            }
        }


        protected int mY = 0;
        public override int Y
        {
            get { return mY; }
            set
            {
                mY = value;
                UpdateCursor();
            }
        }

        public static int mWidth;
        public override int Width
        {
            get { return mWidth; }
        }

        public static int mHeight;
        public override int Height
        {
            get { return mHeight; }
        }

        public static int mCols;
        public override int Cols
        {
            get { return mCols; }
        }

        public static int mRows;
        public override int Rows
        {
            get { return mRows; }
        }

        public static uint foreground = (byte)ConsoleColor.White;
        public override ConsoleColor Foreground
        {
            get { return (ConsoleColor)foreground; }
            set
            {
                foreground = (byte)global::System.Console.ForegroundColor;
                graphics.ChangeForegroundPen(foreground);
            }
        }

        public static uint background = (byte)ConsoleColor.Black;

        public override ConsoleColor Background
        {
            get { return (ConsoleColor)background; }
            set
            {
                background = (byte)global::System.Console.BackgroundColor;
                graphics.ChangeBackgroundPen(background);
            }
        }

        public override int CursorSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static bool cursorvisible = false;
        public override bool CursorVisible { get => cursorvisible; set => cursorvisible = value; }

        public override void Clear()
        {
            graphics.canvas.Clear();
            mX = 0;
            mY = 0;
            UpdateCursor();
        }

        public override void Clear(uint color)
        {
            graphics.canvas.Clear(Color.FromArgb((int)color));
            mX = 0;
            mY = 0;
            UpdateCursor();
        }

        public override void UpdateCursor()
        {
            graphics.SetCursorPos(mX, mY);
        }

        /// <summary>
        /// Scroll the console up and move crusor to the start of the line.
        /// </summary>
        private void DoLineFeed()
        {
            mY++;
            mX = 0;
            if (mY == mRows)
            {
                graphics.canvas.Clear();
                mY = 0;
            }
            UpdateCursor();
        }

        private void DoCarriageReturn()
        {
            mX = 0;
            UpdateCursor();
        }

        /// <summary>
        /// Write char to the console.
        /// </summary>
        /// <param name="aChar">A char to write</param>
        public void Write(char aChar)
        {
            if (aChar == 0)
                return;

            graphics.WriteByte(aChar);
            mX++;
            if (mX == mCols)
            {
                DoLineFeed();
            }
            UpdateCursor();
        }

        public override void Write(char[] aText)
        {
            for (int i = 0; i < aText.Length; i++)
            {
                switch (aText[i])
                {
                    case LineFeed:
                        DoLineFeed();
                        break;

                    case CarriageReturn:
                        DoCarriageReturn();
                        break;

                    case Tab:
                        DoTab();
                        break;

                    /* Normal characters, simply write them */
                    default:
                        Write(aText[i]);
                        break;
                }
            }
        }

        private void DoTab()
        {
            Write(' ');
            Write(' ');
            Write(' ');
            Write(' ');
        }

        public override void DrawImage(ushort X, ushort Y, Bitmap image)
        {
            graphics.canvas.DrawImage(image, X, Y);
        }

        public override void Write(byte[] aText)
        {
            throw new NotImplementedException();
        }
    }
}