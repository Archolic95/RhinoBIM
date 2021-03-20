using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Elements.utils;

namespace Elements.src
{
    public class Wall : ElementsComponent
    {
        protected override System.Drawing.Bitmap Icon => Properties.Resources.brickwall;

        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Wall()
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
            var wallCurve = defaultRect.Curve;

            DA.GetData(0, ref wallCurve);

            var thickness = 150.0;
            DA.GetData(1, ref thickness);

            var level = 0.0;
            DA.GetData(2, ref level);

            var height = 4500.0;
            DA.GetData(3, ref height);

            var profileSrf = Surface.CreateExtrusion(wallCurve, unitZ * height) as BrepFace;

            var solid = Brep.CreateFromOffsetFace(profileSrf, thickness / 2.0, 0.01, true, true);

            DA.SetData(0, solid);
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