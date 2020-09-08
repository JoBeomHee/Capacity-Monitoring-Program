using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace WinforsysGlassMap
{
    public class PinPointLabel : Label
    {
        private int Shape = -1;
        private Color Color = Color.Red;

        public PinPointLabel()
        {

        }

        public void SetImage(int shape, Color color)
        {
            this.Shape = shape;
            this.Color = color;

            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Shape > -1)
            {
                Graphics g = e.Graphics;
                SolidBrush br = new SolidBrush(this.Color);
                Rectangle rect = new Rectangle(0, 0, this.Height, this.Height);

                if (this.Shape == 0)
                {
                    g.FillEllipse(br, rect);
                }
                else if (this.Shape == 1)
                {
                    g.FillRectangle(br, rect);
                }
                else if (this.Shape == 2)
                {
                    Point[] points = new Point[] { new Point(0, this.Height), new Point(this.Height / 2, 0), new Point(this.Height, this.Height) };
                    g.FillPolygon(br, points);
                }
            }
        }
    }
}
