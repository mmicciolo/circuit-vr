using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Puzzles.Components
{
    public enum ResistorColor
    {
        RED,
        GREEN,
        BLUE
    }

    class ResistorCircuitComponent : CircuitComponent
    {
        public ResistorColor resistorColor;
    }
}
