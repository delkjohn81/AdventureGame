using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Adventure
{
    public class Item : Locateable
    {
        public int Value;

        public Item(string name, Vector pos, Animation ani,
            int value) : base(name, pos, ani)
        {
            Value = value;
        }
    }
}
