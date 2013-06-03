using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{
	/// <summary>
	/// Class for creating a rainbow legend.
	/// </summary>
	public class StepGradient : IGradient
	{

		/// <summary>
		/// Types of step gradient defined.
		/// </summary>
		public enum Type
		{
			/// <summary>
			/// Rainbow gradient type (colors of the rainbow)
			/// </summary>
			Rainbow,

			/// <summary>
			/// RGB gradient type (red, green blud).
			/// </summary>
			RGB
		}

		/// <summary>
		/// Sets the type of step gradient.
		/// </summary>
		public Type StepType
		{
			get
			{
				return stepType_;
			}
			set
			{
				stepType_ = value;
			}
		}
		Type stepType_ = Type.RGB;


		/// <summary>
		/// Default Constructor
		/// </summary>
		public StepGradient()
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="stepType">type of gradient</param>
		public StepGradient( Type stepType )
		{
			stepType_  = stepType;
		}

		/// <summary>
		/// Gets a color corresponding to a number between 0.0 and 1.0 inclusive. The color will
		/// be a linear interpolation of the min and max colors.
		/// </summary>
		/// <param name="prop">the number to get corresponding color for (between 0.0 and 1.0)</param>
		/// <returns>The color corresponding to the supplied number.</returns>
		public Color GetColor( double prop )
		{
			switch (stepType_)
			{
				case Type.RGB:
				{
					if (prop < 1.0/3.0) return Color.Red;
					if (prop < 2.0/3.0) return Color.Green;
					return Color.Blue;
				}
				case Type.Rainbow:
				{
					if (prop < 0.125) return Color.Red;
					if (prop < 0.25) return Color.Orange;
					if (prop < 0.375) return Color.Yellow;
					if (prop < 0.5) return Color.Green;
					if (prop < 0.625) return Color.Cyan;
					if (prop < 0.75) return Color.Blue;
					if (prop < 0.825) return Color.Purple;
					return Color.Pink;
				}
				default:
				{
					return Color.Black;
				}
			}

		}

	} 
}
