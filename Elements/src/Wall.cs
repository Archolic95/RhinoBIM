using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Elements.utils;

namespace Elements.src
{
    public class Wall : GH_Component
    {
        protected override System.Drawing.Bitmap Icon => Properties.Resources.brickwall;

        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Wall() : base("Elements", "Nickname",
              "Description",
              "RhinoBIM", "Elements")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "C", "Wall Profile", GH_ParamAccess.item);
            pManager.AddNumberParameter("Thickness", "T", "Wall Tickness", GH_ParamAccess.item);
            pManager.AddNumberParameter("Level", "L", "Wall Level", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "Wall Height", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Wall", "W", "Wall Geometry", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var defaultRect = new defaultRectangle(100.0, 100.0);
            var wallCurve = defaultRect.Curve as Curve;

            DA.GetData(0, ref wallCurve);

            wallCurve = wallCurve.ToNurbsCurve();

            var thickness = 150.0;
            DA.GetData(1, ref thickness);

            var level = 0.0;
            DA.GetData(2, ref level);

            var height = 4500.0;
            DA.GetData(3, ref height);

            var tolerance = 0.000001;
            var inProfile = wallCurve.Offset(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1)), thickness / 2.0, tolerance, CurveOffsetCornerStyle.Sharp)[0];
            var outProfile = wallCurve.Offset(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1)), thickness / (-2.0), tolerance, CurveOffsetCornerStyle.Sharp)[0];

            var inStart = inProfile.PointAtStart;
            var inEnd = inProfile.PointAtEnd;

            var outStart = outProfile.PointAtStart;
            var outEnd = outProfile.PointAtEnd;

            var SS = new Line(inStart, outStart).ToNurbsCurve();
            var EE = new Line(inEnd, outEnd).ToNurbsCurve();

            var closed = Curve.JoinCurves(new List<Curve> { inProfile, SS, outProfile, EE })[0];

            //var baseSrf = Brep.CreatePlanarBreps(closed)[0];
            //var faceBrep = baseSrf.Faces[0];
            //var brepSolid = Brep.CreateFromOffsetFace(faceBrep, dis, 0.01, false, true);
            //var solid = Extrusion.Create(closed, dis, true).ToBrep();
            var surfaceSolid = Surface.CreateExtrusion(closed, new Vector3d(0, 0, 1) * height).ToBrep();
            var brepSolid = surfaceSolid.CapPlanarHoles(tolerance);

            brepSolid.Translate(new Vector3d(0.0, 0.0, level - 0.0));
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4413a467-1be1-4d64-a83c-e099b437b226"); }
        }
    }
}