using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SpeedyCoding;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace WaferandChipProcessing
{
    internal static class Helper
    {
        public static List<Pos> ReadCsv(string path)
            =>  File.ReadAllLines(path).Select( x => (Pos)x.Split(',').Select( y => double.Parse(y) ).ToArray()).ToList();



        public static bool CheckInBox(Func<Rectangle, double> intensum , Pos pos , Rectangle rect , SVCConstrain constrain)
        {
            var x = pos.X;
            var y = pos.Y;
            double area = rect.Width * rect.Height;

            if (rect.X < x
                && x < rect.X + rect.Width
                && y > rect.Y
                && y < rect.Y + rect.Height
                && constrain.DwArea < area
                && constrain.UpArea > area)
                {
                  
                        return true;
                }
            return false;
        }

        public static Func<Func<Rectangle, double> , Rectangle, Pos, SVCConstrain , PosResult> ToResult
            => (intensum , rect, pos , constrain )
            => {
                double inten = intensum(rect);
                if (inten < constrain.Upinten
                    && inten > constrain.Dwinten)
                    return new PosResult(pos.X, pos.Y, intensum(rect), "OK");
                return new PosResult(pos.X, pos.Y, intensum(rect), "NG"); };

        public static List<PosResult> ResultNgInitializer( List<Pos> src )
            => src.Select(x => new PosResult(x.X, x.Y)).ToList();

        public static List<Pos> SVCPosToPixel(this List<Pos> src)
            => src.Select(x => (Pos)new double[] { x.X / 5.0 + 850, x.Y / 5.0 +10610}).ToList();

        public static List<Pos> PixelToSVCPos(this List<Pos> src)
          => src.Select(x => (Pos)new double[] { (x.X - 850) * 5.0, (x.Y - 10610) * 5.0 }).ToList();

        public static void DrawDot(List<Pos> poss, Image<Bgr, byte> img)
        {
            foreach (var pos in poss)
            {
                var dot = new CircleF(new PointF((float)pos.X , (float)pos.Y) , 3);

                img.Draw(dot, new Bgr(40, 80, 190), 1);
            }
        }

        public static void ExportResult(string path, List<PosResult> reslist)
        {
            reslist = reslist.OrderBy(x => x.X).ThenBy(y => y.Y).ToList();

            StringBuilder stb = new StringBuilder();
            stb.Append("X,Y,Intensity,Class" + Environment.NewLine);
            foreach (var item in reslist)
            {
                var x = (item.X - 850) * 5.0;
                var y = (item.Y - 10610) * 5.0;

                stb.AppendLine(x.ToString() + "," + y.ToString() + "," + item.Intensity.ToString() + "," + item.Status);
            }
            File.WriteAllText(path , stb.ToString());
        }

        public static Func<SVCConstrain, Rectangle, bool> AreaConstrain
            => (constrain, rect)
            =>
        {
            if (rect.Width * rect.Height > constrain.UpArea) return false;
            if (rect.Width * rect.Height < constrain.DwArea) return false;
            return true;
        };

        public static Image<Bgr,byte> CreateIndexingMap(List<PosResult> result)
        {

            var xMax = (int)result.Select(x => x.X).Max()+107/2;
            var yMax = (int)result.Select(x => x.Y).Max()+167/2;
            Bitmap imgtemp = new Bitmap(xMax, yMax);
            var map = new byte[yMax/2, xMax/2, 3];

            var rectlist_full = result.Select(x => new Rectangle((int)x.X/2, (int)x.Y/2, 106/2, 166/2))
                                 .ToList();
            var rectlist_NG = result.Where(x => x.Status == "NG")
                                 .Select(x => new Rectangle ((int)x.X/2,(int)x.Y/2,106/2,166/2 ))
                                 .ToList();



            //map = map.MapLoop(x => x = 255);


            map = DrawRect(map, rectlist_full, false);
            map = DrawRect(map, rectlist_NG,true);

            Image<Bgr, byte> mpaimg = new Image<Bgr, byte>(map);

            //Rectangle lockbox = new Rectangle(0, 0, xMax, yMax);
            //System.Drawing.Imaging.BitmapData bmpData
            //    = imgtemp.LockBits(lockbox, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //
            //IntPtr ptr = bmpData.Scan0;
            //var data = map.Flatten();
            //
            //Marshal.Copy(data, 0, ptr, data.Length);
            //
            //imgtemp.UnlockBits(bmpData);

            return mpaimg;
        }

        public static byte[,,] DrawRect(byte[,,] src, List<Rectangle> rects , bool isNG)
        {
            foreach (var rect in rects)
            {
                var whalf = rect.Width / 2;
                var hhalf = rect.Height / 2;

                for (int i = rect.X - whalf; i < rect.X+ whalf-1; i++)
                {
                    for (int j = rect.Y- hhalf; j < rect.Y+ hhalf-1; j++)
                    {
                        if (isNG)
                        {
                            src[j, i, 0] = 10;
                            src[j, i, 1] = 40;
                            src[j, i, 2] = 210;
                        }
                        else
                        {
                            src[j, i, 0] = 255;
                            src[j, i, 1] = 255;
                            src[j, i, 2] = 255;
                        }
                      
                    }
                }
            }
            return src;
        }
    }
}
