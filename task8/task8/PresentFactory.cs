using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task8.Abstractions;

namespace task8.Entities
{
    class PresentFactory : IToyFactory
    {
        public Color PresentColor { get; set; }
        public Toy CreateNew()
        {
            return new Ball(PresentColor);
        }
    }
}
