using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{

	/// <summary>
	/// Class for creating a linear gradient.
	/// </summary>
	public class LinearGradient : IGradient
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="minColor">The color corresponding to 0.0</param>
		/// <param name="maxColor">The color corresponding to 1.0</param>
		public LinearGradient( Color minColor, Color maxColor )
		{
			this.minColor_ = minColor;
			this.maxColor_ = maxColor;
		}


		/// <summary>
		/// The color corresponding to 0.0
		/// </summary>
		public Color MaxColor
		{
			get
			{
				return this.maxColor_;
			}
			set
			{
				this.maxColor_ = value;
			}
		}
		private Color maxColor_;


		/// <summary>
		/// The color corresponding to 1.0
		/// </summary>
		public Color MinColor
		{
			get
			{
				return this.minColor_;
			}
			set
			{
				this.minColor_ = value;
			}
		}
		private Color minColor_;


		/// <summary>
		/// The color corresponding to NaN
		/// </summary>
		public Color VoidColor			         
		{
			get 
			{ 
				return voidColor_; 
			}
			set 
			{ 
				voidColor_ = value; 
			}
		}
        private Color voidColor_ = Color.Black;


		/// <summary>
		/// Gets a color corresponding to a number between 0.0 and 1.0 inclusive. The color will
		/// be a linear interpolation of the min and max colors.
		/// </summary>
		/// <param name="prop">the number to get corresponding color for (between 0.0 and 1.0)</param>
		/// <returns>The color corresponding to the supplied number.</returns>
		public Color GetColor( double prop )
		{
            if (Double.IsNaN(prop))
            {
                return voidColor_;
            }

			if ( prop <= 0.0 )
			{
				return this.MinColor;
			}

			if ( prop >= 1.0 )
			{
				return this.MaxColor;
			}

			byte r = (byte)((int)(this.MinColor.R) + (int)(((double)this.MaxColor.R - (double)this.MinColor.R)*prop));
			byte g = (byte)((int)(this.MinColor.G) + (int)(((double)this.MaxColor.G - (double)this.MinColor.G)*prop));
			byte b = (byte)((int)(this.MinColor.B) + (int)(((double)this.MaxColor.B - (double)this.MinColor.B)*prop));

			return Color.FromArgb(r,g,b);
		}


	}
}
