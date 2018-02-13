using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processing_Test
{
    using SpeedyCoding;
    using Img = Image<Gray, byte>;
    public partial class CropWin : Form
    {

        List<string> pathList = new List<string>();
        public CropWin()
        {
            InitializeComponent();
        }

        private void label2_Click( object sender, EventArgs e )
        {

        }

        private void btnLoad_Click( object sender, EventArgs e )
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if ( ofd.ShowDialog() == DialogResult.OK )
            {
                pathList.Add( ofd.FileName );
                richTextBox1.Text = pathList.Aggregate( ( f, s ) => f + Environment.NewLine + s );
            }
        }

        private void btnstart_Click( object sender, EventArgs e )
        {
            var basepath = Path.GetDirectoryName(pathList[0]);
            List<Img> imglist = new List<Image<Gray, byte>>();
            SaveFileDialog ofd  = new SaveFileDialog();
            ofd.InitialDirectory = basepath;
            if ( ofd.ShowDialog() == DialogResult.OK )
            {
                Rectangle roi = new Rectangle(
                    (int)nudx0.Value,
                    (int)nudy0.Value,
                    (int)nudW.Value,
                    (int)nudH.Value
                    );

                foreach ( var item in pathList )
                {
                    var img = new Img(item);
                    img.ROI = roi;

                    img = img.Copy();

                    imglist.Add( img );
                }
                imglist.ActLoop( ( x, i ) => x.Save( Path.GetDirectoryName( ofd.FileName ).Print( "Base" ) + "\\" + Path.GetFileName( pathList[i] ).Print( "Name" ) ) );
            }
        }

        private void btnLoadAll_Click( object sender, EventArgs e )
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var basepath =  Path.GetDirectoryName( ofd.FileName );

                var filelist = Directory.GetFiles(basepath);

                var imglist = filelist.Select( x => new Img(x) ).ToList();
                var imglistoutput = new List<Img>();

                Rectangle roi = new Rectangle(
                    (int)nudx0.Value,
                    (int)nudy0.Value,
                    (int)nudW.Value,
                    (int)nudH.Value
                    );

                foreach ( var item in imglist )
                {

                    item.ROI = roi;

                    var temp = item.Copy();

                    imglistoutput.Add( temp );
                }

                int i = 0;

                StringBuilder stb = new StringBuilder();

                foreach ( var item in imglistoutput )
                {
                    item.Save( basepath + "\\Crop_" + Path.GetFileName( filelist[i++] ).Print( "Name" ) );

                    int sum = 0;
                    for ( int k = 0 ; k < item.Data.GetLength(0) ; k++ )
                    {
                        for ( int j = 0 ; j < item.Data.GetLength( 1 ) ; j++ )
                        {
                            sum += item.Data[k, j, 0];
                        }
                    }
                    stb.AppendLine( sum.ToString() );
                }
                File.WriteAllText( basepath + "\\Res.csv" ,  stb.ToString() );
            }
        }
    }
}
