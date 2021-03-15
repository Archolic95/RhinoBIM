using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Elements.utils
{
    public class defaultRectangle
    {
        private Curve _rectCurve;
        public Curve Curve 
        {
            get {
                
                return _rectCurve;
            }
        }

        public defaultRectangle(double a, double b) 
        {
            // Default Curve, a 200 * 200 square
            var ptA = new Point3d(-a/2, -b/2, 0);
            var ptB = new Point3d(a/2, -b/2, 0);
            var ptC = new Point3d(-a/2, b/2, 0);
            var ptD = new Point3d(a/2, b/2, 0);
            Curve _rectCurve = Curve.JoinCurves(
                new List<Curve> {
                new LineCurve(ptA, ptB),
                new LineCurve(ptB, ptC),
                new LineCurve(ptC, ptD),
                new LineCurve(ptD, ptA)
                })[0];
        }

    }
}
