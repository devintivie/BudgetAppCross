using System;
using System.Collections.Generic;
using System.Text;

namespace sqlite.pcl.replacement
{
    public interface ITableAttribute
    {
        string ToDbString();
    }
}
