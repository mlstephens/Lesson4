using Shape.Circle;
using Shape.Parallelogram;
using Shape.Square;
using Shape.Triangle;
using System.Collections.Generic;

namespace Shape.AllShapes
{
    public class AllShapes<T>
    {
        public AllShapes()
        {
            Circles = new List<Circle<T>>();
            Parallelograms = new List<Parallelogram<T>>();
            Squares = new List<Square<T>>();
            Triangles = new List<Triangle<T>>();
        }

        public T Id { get; set; }

        public List<Circle<T>> Circles { get; set; }

        public List<Parallelogram<T>> Parallelograms { get; set; }

        public List<Square<T>> Squares { get; set; }

        public List<Triangle<T>> Triangles { get; set; }
    }
}
