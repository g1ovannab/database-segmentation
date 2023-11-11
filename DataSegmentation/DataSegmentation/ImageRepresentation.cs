using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSegmentation
{
    class ImageRepresentation
    {
        public string patientID;
        public string pathology;
        public string path;

        public ImageRepresentation(string pid, string p, string cpath)
        {
            patientID = pid;
            pathology = p;
            path = cpath;
        }
    }
}
