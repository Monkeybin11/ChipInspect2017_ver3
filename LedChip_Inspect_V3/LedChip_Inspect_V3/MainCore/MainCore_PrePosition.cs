using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Util;
using WaferandChipProcessing.Data;
using WaferandChipProcessing.Func;
using System.Windows.Media;
using System.Windows.Controls;
using System.IO;
using static ModelLib.AmplifiedType.Handler;
using static EmguCV_Extension.Vision_Tool;
using static EmguCV_Extension.Preprocessing;
using static Util_Tool.UI.Corrdinate;
using static System.Console;
using static System.Linq.Enumerable;
using System.Diagnostics;
using System.Drawing;
using EmguCV_Extension;
using ModelLib.AmplifiedType;
using Unit = System.ValueTuple;
using static ModelLib.AmplifiedType.PartialApplication;
using SpeedyCoding;


namespace WaferandChipProcessing
{
    using static WaferandChipProcessing.Handler;
    using static SpeedyCoding.Handler;
    using WaferandChipProcessing;
    using static Helper;

    public partial class MainCore
    {
        string posPath = @"C:\Data\20180122_서울바이오시스\test.csv";

        public Action<Image<Gray, byte>, Image<Bgr, byte>> ProcessingStep1_PrePos(
            int threshold,
            SampleType sampletype,
            int cHnum,
            int cWnum,
            bool whiteGrid,
            SVCConstrain Constrain,
            bool debugmode = false)
        {
            return (originalimg, colorimg) =>
            {
                try
                {
                    Stopwatch stw = new Stopwatch();
                    stw.Start();

                    var color_visual_img = colorimg.Clone();
                    var color_visual_img2 = colorimg.Clone();
                    var baseimg = originalimg.Clone();
                    PResult = new ImgPResult(
                        PData.UPAreaLimit,
                        PData.DWAreaLimit,
                        PData.IntenSumUPLimit,
                        PData.IntenSumDWLimit);
                   
                    VectorOfVectorOfPoint contours;
                    //color_visual_img.Save( @"E\:001_Job\016_Samsung_Display_second\Test" +\\beforcntr.bmp" );

                    var proceced = Proc_Method_List[sampletype](baseimg);
                    //proceced.Save( @"C:\Data\180102_플레이나이트라이드\line\B-CC7B00126_RE\test\before.png" );

                    if (debugmode)
                    {
                        contours = baseimg
                                     .Map(Proc_Method_List[sampletype])
                                     //.Act(img => img.Save(TestFileSavePath.BasePath + "\\beforcntr.bmp"))
                                     .Map(FindContour)
                                     .Map(Sortcontours);
                    }
                    else
                    {
                        contours = baseimg
                                     .Map(Proc_Method_List[sampletype])
                                     //.Act(img => img.Save(@"C:\Data\20180122_서울바이오시스\Thres.png"))
                                     .Map(FindContour)
                                     .Map(Sortcontours);
                    }

                    var areaChecker = AreaConstrain.Apply(Constrain);

                    var centerMoment = contours.Map(CalcCenter);
                    var boxlist = contours.Map(ApplyBox)
                                            .Where(areaChecker)
                                            .Select(x=>x)
                                            .ToList(); ;//.OrderBy(x => x.Location.Y).ThenBy(x => x.Location.X).ToList();

                    var color_visual_img3 = colorimg.Clone();
                  
                    #region Changed
                    List<Pos> PosList = ReadCsv(posPath).SVCPosToPixel(); // insert Pos
                    List<PosResult> result = ResultNgInitializer(PosList); // init with NG 

                    for (int k = 0; k < PosList.Count; k++)
                    {
                        var pos = PosList[k];
                        for (int i = 0; i < boxlist.Count ; i++)
                        {
                            if ( CheckInBox( SumInsideBox , pos, boxlist[i], Constrain) )
                            {
                                result[k] = ToResult(SumInsideBox, boxlist[i] , pos , Constrain );
                            }
                        }
                    }

                    DrawBox(color_visual_img3, boxlist);
                    DrawDot(PosList, color_visual_img3);
                    color_visual_img3.Save(@"C:\Data\20180122_서울바이오시스\Processed.png");

                    var indexingimgpath = @"C:\Data\20180122_서울바이오시스\Indexing.png";
                    CreateIndexingMap(result).Save(@"C:\Data\20180122_서울바이오시스\Indexing05.png");


                    var savepath = @"C:\Data\20180122_서울바이오시스\Result.csv";
                    ExportResult(savepath, result);

                    

                    //여기서 결과가 나온다. 이것을 이제 저장하는 기능을 해야한다. 
                    // 1. 씨에스브이 파일로 , 2. 이미지 자체 , 3. 인덱싱 이미지. 
                    // 3 인덱싱 이미지 작업하는데는 다시 함수로 해줘야한다. 

                   
                    #endregion

                    //evtProcessingResult();
                    stw.Stop();
                    Console.WriteLine("Process Time : " + stw.ElapsedMilliseconds);
                    stw.Reset();
                    evtProcessingDone(true);
                }
                catch (Exception er)
                {
                    System.Windows.Forms.MessageBox.Show(er.ToString());
                    evtProcessingDone(true); 
                }
            };
        }
    }

    public struct Pos
    {
        public double X;
        public double Y;

        public static implicit operator Pos(double[] pos) => new Pos() { X = pos[0] , Y = pos[1]};
        public static implicit operator double[](Pos pos) => new double[] { pos.X , pos.Y};
    }

    public class SVCConstrain
    {
        public double Upinten;
        public double Dwinten;
        public double UpArea;
        public double DwArea;

        public SVCConstrain(double upinten, double dwinten, double uparea, double dwarea)
        {
            Upinten = upinten;
            Dwinten = dwinten;
            UpArea = uparea;
            DwArea = dwarea;
        }
    }

    public class PosResult
    {
        public double X;
        public double Y;
        public double Intensity;
        public string Status;

        public PosResult(double x, double y )
        {
            X = x;
            Y = y;
            Status = "NG";
        }

        public PosResult(double x, double y, double inten, string res)
        {
            X = x;
            Y = y;
            Intensity = inten;
            Status = res;
        }
    }

}





