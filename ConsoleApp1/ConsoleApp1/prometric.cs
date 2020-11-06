using System;
using Shapes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;

namespace Shapes
{
	
	public class Shape
	{
		static int count = 0;
		public const double pi = 3.14;
		public Shape()
		{
			Interlocked.Increment(ref count);
		}

		~Shape()
		{
			Interlocked.Decrement(ref count);
		}

		
	   public string getName()
		{
			return name;
		}

		public double getPerimeter()
		{
			return perimeter;
		}

		public double perimeter
		{
			get;
			set;
		}
		
		public string name
		{
			get;
			set;
		}

		public double surfaceArea
		{
			get;
			set;
		}

	
		public double getSurfaceArea()
		{
			return surfaceArea;
		}

		
		public static int GetActiveInstances()
		{
			return count;
		}
	}
	
	
	public class Circle : Shape
	{
		private double radius
		{
			get;
			set;
		}

		public Circle(double radius)
		{
			name = "Circle";
			this.radius = radius;
			perimeter = 2 * pi * radius;
			surfaceArea = pi * radius * radius;
		}
	}

   
	public class Triangle : Shape
	{
		private double sideA
		{
			get;
			set;
		}

		private double sideB
		{
			get;
			set;
		}

		private double baseT
		{
			get;
			set;
		}

		public Triangle(double sideA, double sideB, double baseT, double heightB)
		{
			this.sideA = sideA;
			this.sideB = sideB;
			this.baseT = baseT;
			name = checkTriangleType();
			perimeter = sideA + sideB + baseT;
			surfaceArea = (heightB * baseT) / 2;
		}

		private string checkTriangleType()
		{
			if (sideA == sideB && sideB == baseT)
				return "Equilatoral Triangle";
			else if (sideA == sideB || sideB == baseT || baseT == sideA)
				return "Isosceles Triangle";
			else
				return "Scalene Triangle";
		}
	}
	
	
	public class Quadrilateral : Shape
	{
		private double width
		{
			get;
			set;
		}

		private double length
		{
			get;
			set;
		}

		public Quadrilateral(double width, double length)
		{
			this.width = width;
			this.length = length;
			name = checkQuadrilateralType();
			perimeter = (width + length) * 2;
			surfaceArea = width * length;
		}

		private string checkQuadrilateralType()
		{
			if (width == length)
				return "Square";
			else
				return "Rectangle";
		}
	}
}

public static class ShapeOperations
{
	
	public static List<Shape> sortByField(List<Shape> shapeList, string property)
	{
		if (property.Equals("surfaceArea"))
			shapeList.Sort(new Comparison<Shape>((x, y) => x.surfaceArea.CompareTo(y.surfaceArea)));
		else if (property.Equals("perimeter"))
			shapeList.Sort(new Comparison<Shape>((x, y) => x.perimeter.CompareTo(y.perimeter)));
		else
			throw new Exception("property must be either surfaceArea or perimeter");
        return shapeList;
	}

	
	
	public static string serializeShapes(List<Shape> shapeList)
	{
		return JsonConvert.SerializeObject(shapeList, Formatting.Indented);
	}
}

public class Program
{
	public static void Main()
	{
		
		Shape circle1 = new Circle(5);
		
		Shape triangle1 = new Triangle(10, 14, 20, 6.49);
		Shape triangle2 = new Triangle(15, 15, 15, 12.9);
		Shape triangle3 = new Triangle(12, 12, 15, 9.36);
		
		Shape quadrilateral1 = new Quadrilateral(30, 30);
		Shape quadrilateral2 = new Quadrilateral(15, 40);
		
		List<Shape> shapeList = new List<Shape>()
		{circle1, triangle1, triangle2, triangle3, quadrilateral1, quadrilateral2};
		foreach (Shape s in shapeList)
		{
			Console.WriteLine("Name of Shape: " + s.getName());
			Console.WriteLine("Perimeter of " + s.getName() + ": " + s.getPerimeter());
			Console.WriteLine("Surface Area of " + s.getName() + ": " + s.getSurfaceArea());
			Console.WriteLine("\n");
		}
		
		Console.WriteLine("\nSorted by Surface Area:");
		var sortedByArea = ShapeOperations.sortByField(shapeList, "surfaceArea");
		foreach (Shape s in sortedByArea)
		{
			Console.WriteLine("Name of object Shape: " + s.getName());
		}
		
		var sortedByPerimeter = ShapeOperations.sortByField(shapeList, "perimeter");
		Console.WriteLine("\nSorted by Perimeter:");
		foreach (Shape s in sortedByPerimeter)
		{
			Console.WriteLine("Name of object Shape: " + s.getName());
		}
			
		
		Console.WriteLine(ShapeOperations.serializeShapes(shapeList));
		
		Console.WriteLine("Total number of Shape Objects in  the memory: " + Shape.GetActiveInstances());

        Console.ReadLine();
    }
}