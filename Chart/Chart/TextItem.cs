using System;
using System.Drawing;

namespace xuzhenzhen.com.chart
{
	/// <summary>
	/// This class implements drawing text against two physical axes.
	/// </summary>
	public class TextItem : IDrawable
	{
		private void Init()
		{
			font_ = new Font(FontFamily.GenericSerif, 10, FontStyle.Regular);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position">The position the text starts.</param>
		/// <param name="text">The text.</param>
		public TextItem( PointD position, string text )
		{
			start_ = position;
			text_ = text;
			Init();
		}


		/// <summary>
		/// Text associated.
		/// </summary>
		public string Text 
		{
			get
			{
				return text_;
			}
			set
			{
				text_ = value;
			}
		}
		private string text_ = "";


		/// <summary>
		/// The starting point for the text.
		/// </summary>
		public PointD Start
		{
			get
			{
				return start_;
			}
			set
			{
				start_ = value;
			}
		}
		private PointD start_;


		/// <summary>
		/// Draws the text on a plot surface.
		/// </summary>
		/// <param name="g">graphics surface on which to draw</param>
		/// <param name="xAxis">The X-Axis to draw against.</param>
		/// <param name="yAxis">The Y-Axis to draw against.</param>
		public void Draw( System.Drawing.Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis )
		{
			Point startPoint = new Point( 
				(int)xAxis.WorldToPhysical( start_.X, true ).X,
				(int)yAxis.WorldToPhysical( start_.Y, true ).Y );

			g.DrawString(text_, font_, textBrush_,(int)startPoint.X,(int)startPoint.Y);
		}


		/// <summary>
		/// The brush used to draw the text.
		/// </summary>
		public Brush TextBrush
		{
			get
			{
				return textBrush_;
			}
			set
			{
				textBrush_ = value;
			}
		}

	
		/// <summary>
		/// Set the text to be drawn with a solid brush of this color.
		/// </summary>
		public Color TextColor
		{
			set
			{
				textBrush_ = new SolidBrush( value );
			}
		}
	
		/// <summary>
		/// The font used to draw the text associated with the arrow.
		/// </summary>
		public Font TextFont
		{
			get
			{
				return this.font_;
			}
			set
			{
				this.font_ = value;
			}
		}

		private Brush textBrush_ = new SolidBrush( Color.Black );
		private Pen pen_ = new Pen( Color.Black );
		private Font font_;
	}
}
