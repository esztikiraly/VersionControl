using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task8.Abstractions;

namespace task8.Entities
{
    class Car :Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFile = Image.FromFile("car.PNG");
            g.DrawImage(imageFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
