using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace DataGraph
{
    /// <summary>
    /// Point list
    /// </summary>
    public class PointList : List<PointF>
    {
        /// <summary>
        /// First point
        /// </summary>
        public PointF First
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    throw new Exception("Point list is empty");
                }
            }
        }

        /// <summary>
        /// Last point
        /// </summary>
        public PointF Last
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    throw new Exception("Point list is empty");
                }
            }
        }
    }
}
