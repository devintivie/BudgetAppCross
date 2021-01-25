using System;
using System.Collections.Generic;
using System.Text;

namespace sqlite.pcl.replacement
{
    public enum ForeignKeyAction
    {
        NO_ACTION,
        RESTRICT,
        SET_NULL,
        SET_DEFAULT,
        CASCADE
    }
}
