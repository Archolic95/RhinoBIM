﻿using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GHBIMExecutor
{
    public class GHBIMExecutorInfo : GH_AssemblyInfo
    {
        internal static readonly string PLUGIN_NAME = "GHBIMExecutor";
        public override string Name
        {
            get
            {
                return "GHBIMExecutor";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("2007f5df-2012-4a06-8850-9c499861b2d0");
                //return new Guid("c5c1242e-37ee-4cd8-a057-62191d642174");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Runjia Tian";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "runjia_tian@gsd.harvard.edu";
            }
        }
    }
}
