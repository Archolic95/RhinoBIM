using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Elements.utils;

namespace Elements.src
{
    public class Floor : GH_Component
    {
        protected override System.Drawing.Bitmap Icon => Properties.Resources.wood;

        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Floor()
          : base("Floors", "Nickname",
              "Description",
              "RhinoBIM", "Elements")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Boundary", "B", "Floor Boundary", GH_ParamAccess.item);
            pManager.AddNumberParameter("Thickness", "T", "Floor Thickness", GH_ParamAccess.item);
            pManager.AddNumberParameter("Level", "L", "Floor Level", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Floor", "F", "Floor Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var defaultRect = new defaultRectangle(10000.0,10000.0);
            var floorProfile = defaultRect.Curve;

            DA.GetData(0, ref floorProfile);

            var thickness = 300.0;
            DA.GetData(1, ref thickness);

            var level = 0.0;
            DA.GetData(2, ref level);

            var direction = new Vector3d(0.0,0.0,1.0) * thickness;
            var sideSurface = Brep.CreateFromSurface(Surface.CreateExtrusion(floorProfile, direction));

            var massProperty = AreaMassProperties.Compute(floorProfile);
            var centroid = massProperty.Centroid;

            var startSection = floorProfile.Duplicate() as Curve;
            startSection.Translate(new Vector3d(0,0,level - centroid.Z));

            var endSection = floorProfile.Duplicate() as Curve;
            endSection.Translate(new Vector3d(0, 0, level+thickness - centroid.Z));

            var startCap = Brep.CreatePlanarBreps(startSection, 0.01)[0];
            var endCap = Brep.CreatePlanarBreps(endSection, 0.01)[0];

            var output = Brep.CreateSolid(new List<Brep> { startCap, sideSurface, endCap }, 0.01)[0];

            DA.SetData(0, output);
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("048489b4-055c-4255-8c5e-46dc0fe8f9cf"); }
        }
    }
}