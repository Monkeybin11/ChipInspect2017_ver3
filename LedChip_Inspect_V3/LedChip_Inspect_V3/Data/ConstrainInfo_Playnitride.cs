using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaferandChipProcessing
{
	public class ConstrainInfo_Playnitride
	{
		//public readonly double BoundaryLen = 8127; // B1
		//public readonly double BoundaryLen = 8141; // B2
		public readonly double BoundaryLen = 8500;  // G1
	
		public readonly Tuple<double,double> Center;

		public ConstrainInfo_Playnitride( double xoff , double yoff )
		{
			Center = Tuple.Create( xoff , yoff );
		}
	}

	public static class Playnitride_Ext
	{
		// B1
		//public static int XIDxOffset = 403;
		//public static int YIDxOffset = 700;

		// B2
		//public static int XIDxOffset = 402;
		//public static int YIDxOffset = 736;

		//G1
		public static int XIDxOffset = 495;
		public static int YIDxOffset = 700;

		public static int ToPNPosX( this int x )
			=> x - XIDxOffset;


		public static int TOPNPosY( this int y  )
			=> YIDxOffset - y;


        public static int X_LGIDxOffset = 495;
        public static int Y_LGIDxOffset = 700;

        public static int ToLGPosX(this int x)
            => x - X_LGIDxOffset;


        public static int TOLGPosY(this int y)
            => Y_LGIDxOffset - y;



    }
}
