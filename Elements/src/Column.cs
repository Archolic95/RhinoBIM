using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Elements.utils;

namespace Elements.src
{
    public class Column : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Column() : base("Column", "Nickname",
              "Description",
              "Category", "Subcategory")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Section", "S", "Section Curve for Column", GH_ParamAccess.item);
            pManager.AddCurveParameter("Path", "P", "Extrusion Trajectory", GH_ParamAccess.item);
            pManager.AddGenericParameter("Start Level", "SL", "Level for the Column", GH_ParamAccess.item);
            pManager.AddGenericParameter("End Level", "EL", "Level for the Column", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Column", "C", "New Column", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var defaultRect = new defaultRectangle(100.0,100.0);
            var columnSection = defaultRect.Curve;
            
            DA.GetData(0, ref columnSection);

            if (!columnSection.IsPlanar() || !columnSection.IsClosed)
            {
                // Log Error
            }

            Curve path = new LineCurve(new Point3d(0, 0, 0), new Point3d(0, 0, 3000));

            DA.GetData(1, ref path);

            var massProperty = AreaMassProperties.Compute(columnSection);
            var centroid = massProperty.Centroid;

            double startLevel = 0.0;
            DA.GetData(2, ref startLevel);

            double endLevel = 3000.0;

            DA.GetData(3, ref endLevel);

            var startPoint = path.PointAtStart - centroid;
            var endPoint = path.PointAtEnd - centroid;

            var startSection = columnSection.Duplicate() as Curve;
            startSection.Translate(startPoint);

            var endSection = columnSection.Duplicate() as Curve;
            endSection.Translate(endPoint);

            var direction = path.PointAtEnd - path.PointAtStart;
            var sideSurface = Brep.CreateFromSurface(Surface.CreateExtrusion(startSection, direction));

            var startCap = Brep.CreatePlanarBreps(startSection, 0.01)[0];
            var endCap = Brep.CreatePlanarBreps(endSection, 0.01)[0];

            var output = Brep.CreateSolid(new List<Brep> {startCap, sideSurface, endCap }, 0.01)[0];

            DA.SetData(0, output);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Resource.pillar;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("abe8829c-4923-4859-b517-d8c9b864e102"); }
        }
    }
}