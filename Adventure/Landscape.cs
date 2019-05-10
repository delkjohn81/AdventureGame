using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Adventure
{
    public enum LandscapeType
    {
        Tree, Wall, Water, Mountain
    }

    public class Landscape : Locateable
    {
        public bool Passable;
        public LandscapeType Type;

        public Landscape(string name, Vector pos, Animation ani,
            bool pass, LandscapeType type) : base(name, pos, ani)
        {
            Type = type;
            Passable = pass;
        }
    }
}
