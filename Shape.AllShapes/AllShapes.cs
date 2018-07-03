using Shape.Circle;
using Shape.Parallelogram;
using Shape.Square;
using Shape.Triangle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape.AllShapes
{
    public class AllShapes<T>
    {
        public T ID { get; set; }

        public List<Circle<T>> Circles { get; set; }

        public List<Parallelogram<T>> Parallelograms { get; set; }

        public List<Square<T>> Squares { get; set; }

        public List<Triangle<T>> Triangles { get; set; }
    }
}
