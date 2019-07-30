using System;
using System.Drawing;

namespace FlyweightPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var frodo = new FrodoGlyph();
            var gandalf = new GandalfGlyph();

            using (var bmp = new Bitmap(800, 600))
            using(var graphics = Graphics.FromImage(bmp))
            {
                frodo.DrawOnto(graphics);
                gandalf.CastOnto(graphics);
            }
        }
    }


    public class FrodoGlyph
    {
        public readonly Guid Id = Guid.Parse("82D701F2-FFDF-4CFC-9005-823555AC0628");
        public string FontName => LotRFontInfo.Name; /* Flyweigth Pointer*/
        public string License => LotRFontInfo.License; /* Flyweigth Pointer*/
        public string Path => "M12,2A9,9 0 0,0 3,11C3,14.03 4.53,16.82 7,18.47V22H9V19H11V22H13V19H15V22H17V18.46C19.47,16.81 21,14 21,11A9,9 0 0,0 12,2M8,11A2,2 0 0,1 10,13A2,2 0 0,1 8,15A2,2 0 0,1 6,13A2,2 0 0,1 8,11M16,11A2,2 0 0,1 18,13A2,2 0 0,1 16,15A2,2 0 0,1 14,13A2,2 0 0,1 16,11M12,14L13.5,17H10.5L12,14Z";

        public void DrawOnto(Graphics graphics)
        {
            Brush b = new SolidBrush(Color.Maroon);
            graphics.FillPolygon(b,  new PointF[] { /* Path */ });
        }
    }

    public class GandalfGlyph
    {
        public readonly Guid Id = Guid.Parse("6640A708-F177-42EA-9133-D0005C688C43");
        public string FontName => LotRFontInfo.Name; /* Flyweigth Pointer*/
        public string License => LotRFontInfo.License; /* Flyweigth Pointer*/
        public string Path => "M10 8.75V3.5H8V17.5L14 15.25V20.5H16V6.5L10 8.75M14 13.25L10 14.75V10.75L14 9.25V13.25Z";

        public void CastOnto(Graphics graphics)
        {
            Brush b = new SolidBrush(Color.White);
            graphics.FillPolygon(b, new PointF[] { /* Path */ });
        }
    }

    public static class LotRFontInfo /* Flyweight */
    {
        public static string Name = "LotR Font (slim)";
        public static string License = @"
           Copyright (c) 2019, Wit wit@wit.com, with Reserved Font Name: 'LotR Font(slim)'
           This      Font Software is licensed under the SIL Open Font License, Version 1.1.
           This license is copied below, and is also available with a FAQ at:
           http://scripts.sil.org/OFL

           Version 1.1 - 26 February 2007";
    }
}
