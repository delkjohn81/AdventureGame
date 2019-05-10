using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public enum PotionType
    {
        Health, Strength, Defense, Speed
    }
    public class Potion : Item
    {
        public PotionType Type;
        public Potion(string name, Vector pos, Animation ani, int value, PotionType type)
            : base(name, pos, ani, value)
        {
            Type = type;
        }
    }
}
