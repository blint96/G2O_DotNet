using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public interface IItemInstanceProvider
    {
        IItemInstance GetItemInstance(string instance);
    }
}
