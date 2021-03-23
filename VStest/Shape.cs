using System;

namespace VStest
{
    public abstract class Shape:IComparable
    {
        protected Exception InvalidInputException = new Exception("Value cannot be non-positive");
        protected double area { get; set; }

        public int CompareTo(object obj)
        {
            var other = (Shape)obj;
            if (this.area > other.area)
                return 1;
            else if (this.area == other.area)
                return 0;
            else
                return -1;
        }
        public virtual void Info()
        {
            Console.Write("Area of the figure : " + area);
            Console.WriteLine("\t This is" + this.GetType());
        }
    }
    public class Square : Shape
    {
        public double Side { get; private set; }
        public Square(double side)
        {
            if (side < 0)
                throw InvalidInputException;
            this.Side = side;
            area = side * side;
        }
    }
    public class Rectangle : Shape
    {
        public double Width { get; private set; }
        public double Height { get; private set; }
        public Rectangle(double width, double height)
        {
            if (width < 0 || height < 0)
                throw InvalidInputException;
            this.Width = Width;
            this.Height = height;
            area = Width * Height;
        }
    }
    public class Triangle : Shape
    {
        public double Base { get; private set; }
        public double Height { get; private set; }

        public Triangle(double _base, double height)
        {
            if (_base < 0 || height < 0)
                throw InvalidInputException;
            Base = _base;
            Height = height;
            area = (Base * Height) / 2;
        }

    }
    public class Circle : Shape
    {
        public double Radius { get; private set; }

        public Circle(double radius)
        {
            if (radius < 0)
                throw InvalidInputException;
            Radius = radius;
            area = Math.PI * Math.Pow(radius, 2);
        }
    }
}
