using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    using System.Drawing;

    public class ChangeWeaponModeEventArgs:EventArgs
    {
        public ICharacter Character { get; }
        public int OldWeaponMode { get; }
        public  int NewWeaponMode { get; }
    }

}
