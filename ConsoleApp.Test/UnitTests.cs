using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Circle_String()
        {
            //Circle<string> circle = new Circle<string>() { ID = "99", Angles = 0, Sides = 0 };
            //IGenericClass<string> igc = circle;

            //Assert.AreEqual(igc.Id, circle.ID);
            //Assert.AreEqual(igc.Name, "Circle");
            //Assert.AreEqual(igc.Formula, "PI * Radius2");
            //Assert.AreEqual(igc.Sides, circle.Sides);
            //Assert.AreEqual(igc.Angles, circle.Angles);
        }

        [TestMethod]
        public void Circle_Integer()
        {
            //Circle<int> circle = new Circle<int>() { ID = 102, Angles = 4, Sides = 4 };
            //IGenericClass<int> igc = circle;

            //Assert.AreEqual(igc.Id, circle.ID);
            //Assert.AreEqual(igc.Name, "Circle");
            //Assert.AreEqual(igc.Formula, "PI * Radius2");
            //Assert.AreEqual(igc.Sides, circle.Sides);
            //Assert.AreEqual(igc.Angles, circle.Angles);
        }
    }
}
