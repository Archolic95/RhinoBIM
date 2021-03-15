using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Elements
{
    public class ElementsInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Elements";
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
