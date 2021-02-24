
namespace Micro_Marine.src
{
    class Animation
    {
        private float interval;
        private double timePassed;
        private int[] frames;
        private int currentFrame;
        private bool loop;
        
        public Animation(float interval, int[] frameIndecies, bool loop)
        {
            this.interval = interval;
            this.frames = frameIndecies;
            this.loop = loop;
            this.currentFrame = 0;
            this.timePassed = 0f;
        }

        public void Update(double dt)
        {
            timePassed += dt;
            if (timePassed >= interval)
            {
                setNextFrame();
                timePassed = 0d;
            }
        }

        private void setNextFrame()
        {
            bool onLastFrame = isLastFrame(currentFrame);

            if (onLastFrame && loop)
            {
                currentFrame = 0;
            }
            else if (onLastFrame)
            {
                // do nothing
            }
            else
            {
                currentFrame++;
            }
        }

        private bool isLastFrame(int index)
        {
            return currentFrame == frames.Length - 1;
        }

        public int GetFrame()
        {
            return currentFrame;
        }

        public void SetInterval(float value)
        {
            interval = value;
        }
    }
}
