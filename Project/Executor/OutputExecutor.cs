using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace GHBIMExecutor.Executor
{
    public class OutputExecutor : ExecutorComponent
    {
        protected override System.Drawing.Bitmap Icon => Properties.Resources.icons_Stair;
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public OutputExecutor()
          : base("OutputExecutor", "OE",
              "Execute Grasshopper output and send to Unity")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("Brep", "B", "inputBreps", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "output Mesh", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var inputBrep = new Brep();
            DA.GetData(0, ref inputBrep);
            var outputMesh = Mesh.CreateFromBrep(inputBrep, new MeshingParameters())[0];

            DA.SetData(0, outputMesh);
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("667caa89-600c-40ee-a677-f1bb298f4bb3"); }
        }
    }
}