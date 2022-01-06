﻿using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;
using System.Text;

namespace Quantum.Drawing
{
    public class Graphics
    {
        public Canvas canvas;

        public PCScreenFont font;

        private static uint[] Pallete = new uint[16];

        public Graphics()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();

            font = PCScreenFont.LoadFont(Convert.FromBase64String("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AAwwAAB+AgQIECBAfgAAAAAQEAAAfgIECBAgQH4AAAAAQjwAAAAAAAAAAAAAAAAAABAQAAAAAAAAAAAAAAAAAAAkGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEBAM/f///8AD//9gIv//ZCL//2Ui//+gJawlriX8Jf4lGysOIv//xiVmJiUrJyv//7wA//+9AP//vgD//6YA//+oAP//uAD//5IB//8gIP//ISD//zAg//8iIf//JiD//zkg//86IP//HCAfIP//HSDuAv//HiD//0Iu//9BLs4C//8eAf//HwH//zAB//8xAf//XgH//18B//8gAKAAACABIAIgAyAEIAUgBiAHIAggCSAKIC8gXyD//yEA//8iAP//IwD//yQA//8lAP//JgD//ycAvAL//ygA//8pAP//KgBOIBci//8rAP//LADPAnUDGiD//y0ArQAQIBEgEiATIEMgEiL//y4AJCD//y8ARCAVIv//MAD//zEA//8yAP//MwD//zQA//81AP//NgD//zcA//84AP//OQD//zoANiL//zsA//88AP//PQBALv//PgD//z8A//9AAP//QQAQBJED//9CABIEkgP//0MAIQT5A///RAD//0UAFQSVA///RgD//0cA//9IAB0ElwP//0kABgTABM8EmQP//0oACAR/A///SwAaBJoDKiH//0wA//9NABwEnAP6A///TgCdA///TwAeBJ8D//9QACAEoQP//1EAGgX//1IA//9TAAUE//9UACIEpAP//1UA//9WAP//VwAcBf//WAAlBKcD//9ZAK4EpQP//1oAlgP//1sA//9cAPUp//9dAP//XgDEAsYCAyP//18A//9gAMsC7x81IP//YQAwBP//YgD//2MAQQTyA///ZAD//2UANQT//2YA//9nAP//aAD//2kAVgT//2oAWATzA///awD//2wA//9tAP//bgD//28APgS/A///cABABP//cQAbBf//cgD//3MAVQT//3QA//91AP//dgD//3cAHQX//3gARQT//3kAQwT//3oA//97AP//fAAjIv//fQD//34A3ALAH///IiAZIs8l///AAP//wQD//8IA///DAP//xADSBP//xQArIf//xgDUBP//xwD//8gAAAT//8kA///KAP//ywABBP//zAD//80A///OAP//zwAHBKoD///QABAB///RAP//0gD//9MA///UAP//1QD//9YA5gT//9cA///YAP//2QD//9oA///bAP//3AD//90A///eAPcD///fAP//kiX//6EA//+iAP//owD//6wg//+lAP//YAH//6cA//9hAf//qQD//6oA//+rAP//rAD//6QA//+uAP//rwDJAv//sADaAv//sQD//7IA//+zAP//fQH//7UAvAP//7YA//+3AIcDJyDFIjEu//9+Af//uQD//7oA//+7AP//UgH//1MB//94AasD//+/AP//ACUUIBUgryP//wIl//8MJW0l//8QJW4l//8UJXAl//8YJW8l//8cJf//JCX//ywl//80Jf//PCX//5El//+6Iz4g//+7I///vCP//70j//9QJQEl//9RJQMl//9UJQ8l//9XJRMl//9aJRcl//9dJRsl//9gJSMl//9jJSsl//9mJTMl//9pJTsl//9sJUsl//+IJf//kSH//5Mh//+QIf//kiH//+AA///hAP//4gD//+MA///kANME///lAP//5gDVBP//5wD//+gAUAT//+kA///qAP//6wBRBP//7AD//+0A///uAP//7wBXBP//8AD///EA///yAP//8wD///QA///1AP//9gDnBP//9wD///gA///5AP//+gD///sA///8AP///QD///4A+AP///8A//+0ALkCygJ0A/0fMiD//7sCvQL+HxggGyD//xkgvR+/H///NiD//zMgugLdAv//FiH//7Qg//+9IP//EQSCAf//EwSTA///FAT//xYE//8XBP//GAR2A///GQT//xsE//8fBKADDyL//yME//8kBKYD//8mBP//JwT//ygE//8pBP//KgT//ysE//8sBP//LQT//y4E//8vBP//BAT//w4E//8CBP//AwT//wkE//8KBP//CwT//wwEMB7//w8E//+QBP//DQT//zEE//8yBP//MwT//zQE//82BP//NwT//zgEdwP//zkE//86BP//OwT//zwE//89BP//PwT//0IE//9EBNUDeAL//0YE//9HBP//SAT//0kE//9KBP//SwT//0wE//9NBPYD//9OBP//TwT//1QE9QP//14E//9SBP//UwT//1kE//9aBP//WwQnAf//XAT//18E//+RBP//XQT//3oDvh///4QD//+FA///hgP//4gD//+JA///igP//4wD//+OA///jwP//5AD//+UAwYi//+YA58B9ANyBOgE//+bA0UC//+eA///owOpAREi//+oA///qQMmIf//rAP//60D//+uA///rwP//7AD//+xA///sgP//7MD//+0A58e//+1A1sC//+2A///twOeAf//uAP//7kDaQL//7oDOAH//7sD//+9A///vgP//8ED///CA///wwP//8QD///FA///xgP//8cD///IA///yQP//8oD///LA///zAP//80D///OA///AAH//wIB0AT//wQB//8CHv//BgH//wgB//8KAf//DAH//woe//8OAf//EgH//xYB//8aAf//GAH//x4e//8cAf//IAH//yIB//8kAf//JgH//ygB//8qAf//LgH//zQB//82Af//OQH//z0B//87Af//QQH//0Ae//9DAf//RwH//0UB//9KAf//TAH//1AB//9WHv//VAH//1gB//9WAf//WgH//1wB//9gHv//GAL//2oe//9kAf//GgL//2IB//9mAf//aAH//2oB//9sAf//bgH//3AB//9yAf//gB7//4Ie//90Af//hB7///Ie//92Af//eQH//3sB//8BAf//AwHRBP//BQH//wMe//8HAf//CQH//wsB//8NAf//Cx7//w8B//8RAf//EwH//xcB//8bAf//GQH//x8e//8dAf//IQH//yMB//8lAf//KQH//ysB//8vAf//NQH//zcB//86Af//PgH//zwB//9CAf//QR7//0QB//9IAf//RgH//0sB//9NAf//UQH//1ce//9VAf//WQH//1cB//9bAf//XQH//2Ee//8ZAv//ax7//2UB//8bAv//YwH//2cB//9pAf//awH//20B//9vAf//cQH//3MB//+BHv//gx7//3UB//+FHv//8x7//3cB//96Af//fAH//9gC///ZAv//xwL//9sC//8="));

            Pallete[0] = 0xFF000000; // Black
            Pallete[1] = 0xFF0000AB; // Darkblue
            Pallete[2] = 0xFF008000; // DarkGreen
            Pallete[3] = 0xFF008080; // DarkCyan
            Pallete[4] = 0xFF800000; // DarkRed
            Pallete[5] = 0xFF800080; // DarkMagenta
            Pallete[6] = 0xFF808000; // DarkYellow
            Pallete[7] = 0xFFC0C0C0; // Gray
            Pallete[8] = 0xFF808080; // DarkGray
            Pallete[9] = 0xFF5353FF; // Blue
            Pallete[10] = 0xFF55FF55; // Green
            Pallete[11] = 0xFF00FFFF; // Cyan
            Pallete[12] = 0xFFAA0000; // Red
            Pallete[13] = 0xFFFF00FF; // Magenta
            Pallete[14] = 0xFFFFFF55; // Yellow
            Pallete[15] = 0xFFFFFFFF; //White

            frontpen = new Pen(Color.FromArgb((int)Pallete[GraphicalConsole.foreground]));
            backpen = new Pen(Color.FromArgb((int)Pallete[GraphicalConsole.background]));

            lastpen = backpen;
            lastx = 0;
            lasty = font.Height;
        }

        public Pen frontpen;
        public Pen backpen;

        public void DrawFilledRectangle(Pen pen, int x_start, int y_start, int width, int height)
        {
            if (height == -1)
            {
                height = width;
            }

            for (int y = y_start; y < y_start + height; y++)
            {
                canvas.DrawLine(pen, x_start, y, x_start + width + 1, y);
            }
        }

        public void WriteByte(char ch)
        {
            DrawFilledRectangle(backpen, Kernel.AConsole.X * font.Width + 1, Kernel.AConsole.Y * font.Height, font.Width - 1, font.Height);
            canvas.DrawChar((char)ch, font, frontpen, Kernel.AConsole.X * font.Width, Kernel.AConsole.Y * font.Height);
        }

        public void ChangeForegroundPen(uint foreground)
        {
            frontpen = new Pen(Color.FromArgb((int)Pallete[foreground]));
        }

        public void ChangeBackgroundPen(uint background)
        {
            backpen = new Pen(Color.FromArgb((int)Pallete[background]));
        }

        public void ScrollUp()
        {
            // ???
        }

        public Pen lastpen;
        public int lastx;
        public int lasty;

        public void SetCursorPos(int mX, int mY)
        {
            if (Kernel.AConsole.CursorVisible)
            {
                DrawFilledRectangle(lastpen, lastx, lasty, 8, 4);
                DrawFilledRectangle(frontpen, mX * font.Width, mY * font.Height + font.Height, 8, 4);

                lastx = mX * font.Width;
                lasty = mY * font.Height + font.Height;
                lastpen = backpen;
            }
        }
    }
}
