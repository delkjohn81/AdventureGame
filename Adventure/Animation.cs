using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Adventure
{
    public delegate void EndedHandler(Animation ani, Locateable loc);
    public struct AnimationState
    {
        public bool Running;
        public double Index;

        public AnimationState(bool run, double idx)
        {
            Running = run;
            Index = idx;
        }
    }
    public class Animation
    {
        protected bool continuous;//, running;
        protected Image spriteMap;
        protected List<Image> frames = new List<Image>();
        protected int frameCount;
        protected double index, rate;
        protected Dictionary<Locateable, AnimationState> locs = new Dictionary<Locateable, AnimationState>();
//      protected Dictionary<Locateable, double> locs = new Dictionary<Locateable, double>();
//      protected Dictionary<Locateable, bool> locRuns = new Dictionary<Locateable, bool>();

        public int Width, Height;
        public event EndedHandler Ended;

        #region OldWayOfGettingFrame
        /*public Image Frame
        {
            get
            {
                index = index + rate;
                if(index >= frameCount - 1)
                {
                    if(Ended != null)
                    {
                        index = 0;
                        Ended(this);
                    }
                }
                
                if(index >= frameCount)
                {
                    if (continuous)
                    {
                        index = 0;
                    }
                    else
                    {
                        index--;
                    }
                }
                return frames[(int)index];
            }
        }*/
        #endregion

        public Animation(Image spriteMap, int startX, int startY, int cols, int rows,
            int width, int height, bool continuous, double rate, double scale)
        {
            frameCount = cols * rows;
            this.continuous = continuous;
            if (continuous)
            {
                index = Game.rand.Next(-1, frameCount - 1);
            }
            else
            {
                index = -1;
            }
            this.rate = rate;
            Width = (int)(width * scale);
            Height = (int)(height * scale);
            startX = (int)(scale * startX);
            startY = (int)(scale * startY);
            Bitmap bmap = new Bitmap(spriteMap, new Size((int)(scale * spriteMap.Width), (int)(scale * spriteMap.Height)));
            PixelFormat pf = spriteMap.PixelFormat;
            for (int y = startY; y < startY + rows * Height; y = y + Height)
            {
                for (int x = startX; x < startX + cols * Width; x = x + Width)
                {
                    Image frame;
                    int cloneW = Width;
                    int cloneH = Height;
                    if(x + Width > bmap.Width)
                    {
                        cloneW = bmap.Width - x;
                    }
                     
                    if(y + Height > bmap.Height)
                    {
                        cloneH = bmap.Height - y;
                    }
                    frame = bmap.Clone(new Rectangle(x, y, cloneW, cloneH), pf);
                    frames.Add(frame);
                }
            }
        }

        public void AddLocateable(Locateable loc)
        {
            if (continuous)
            {
                AnimationState aState = new AnimationState(true, Game.rand.Next(0, frameCount));
                locs.Add(loc, aState);
            }
            else
            {
                AnimationState aState = new AnimationState(false, -1);
                locs.Add(loc, aState);
            }
        }

        public Image GetFrame(Locateable loc)
        {
            double idx = locs[loc].Index;
            bool running = locs[loc].Running;
            if(!continuous && !running)
            {   // restart
                idx = -1;
                running = true;
            }
            idx += rate;
            if(idx >= frameCount - 1)
            {
                if (!continuous && Ended != null)
                {
                    running = false;
                    Ended(this, loc);
                }
            }

            if(idx >= frameCount)
            {
                if (continuous)
                {
                    idx = 0;
                }
                else
                {
                    idx = frameCount - 1;
                }
            }
            locs[loc] = new AnimationState(running, idx);
            return frames[(int)idx];
        }
    }
}
