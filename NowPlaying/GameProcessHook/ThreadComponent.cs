using System;
using System.Threading;

namespace NowPlaying.GameProcessHook
{
    public abstract class ThreadComponent : IDisposable
    {
        protected virtual string ThreadName => nameof(ThreadComponent);
        protected virtual TimeSpan ThreadTimeout { get; set; } = new TimeSpan(0, 0, 0, 3);
        protected virtual TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 1);
        private Thread Thread { get; set; }
        protected ThreadComponent()
        {
            Thread = new Thread(ThreadStart)    
            {
                Name = ThreadName, // Virtual member call in constructor
            };
        }

        public virtual void Dispose()
        {
            Thread.Interrupt();
            if (!Thread.Join(ThreadTimeout))
            {
                Thread.Abort();
            }
            Thread = default;
        }

        public void Start() => Thread.Start();

        private void ThreadStart()
        {
            try
            {
                while (true)
                {
                    FrameAction();
                    Thread.Sleep(ThreadFrameSleep);
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }
        protected abstract void FrameAction();
    }
}
