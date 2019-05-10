using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Adventure
{
    public class Locateable
    {
        public string Name;
        public Vector Position;
        public virtual Image Img
        {
            get
            {
                return aniNone.GetFrame(this);
            }
        }

        protected Animation aniNone;
        public int Width
        {
            get
            {
                return aniNone.Width;
            }
        }

        public int Height
        {
            get
            {
                return aniNone.Height;
            }
        }

        public Locateable(string name, Vector pos, Animation aniNone)
        {
            Name = name;
            Position = pos;
            this.aniNone = aniNone;
            if (aniNone != null)
            {
                aniNone.AddLocateable(this);
            }
        }
    }
}
