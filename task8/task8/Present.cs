using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task8.Abstractions;

namespace task8.Entities
{
    class Present : Toy
    {
        public SolidBrush PresentColor { get; private set; }
        public Present(Color color)
        {
            AutoSize = false;
            Height = 50;
            Width = 50;
            Paint += Present_Paint1;
            PresentColor = new SolidBrush(color);

        }

        private void Present_Paint1(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(PresentColor, 0, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left += 1;
        }
    }
}
