using Shape.Circle;
using Shape.Parallelogram;
using Shape.Square;
using Shape.Triangle;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class AllShapes<T>
    {
        public List<Circle<T>> Circles { get; set; } = new List<Circle<T>>();

        public List<Parallelogram<T>> Parallelograms { get; set; } = new List<Parallelogram<T>>();

        public List<Square<T>> Squares { get; set; } = new List<Square<T>>();

        public List<Triangle<T>> Triangles { get; set; } = new List<Triangle<T>>();
    }
}
