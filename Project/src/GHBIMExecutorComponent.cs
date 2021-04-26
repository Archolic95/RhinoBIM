using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHBIMExecutor
{
    /// <summary>
    /// A base class for all components of this plugin to inherit from
    /// </summary>
    public abstract class GHBIMExecutorComponent : GH_Component
    {
        /// <summary>
        /// The constructor always uses the library name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nickname"></param>
        /// <param name="description"></param>
        /// <param name="category"></param>
        public GHBIMExecutorComponent(string name, string nickname, string description, string category)
            : base(name, nickname, description, GHBIMExecutorInfo.PLUGIN_NAME, category) { }


    }
}
