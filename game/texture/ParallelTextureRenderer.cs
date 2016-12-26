using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AbrahmanAdventure.level
{
    internal class ParallelTextureRenderer
    {
        #region Private Parts
        private Thread visitorThread;

        private int remainingCount = 0;
        #endregion

        #region Public Methods
        public void Render(List<Ground> groundList)
        {
            visitorThread = Thread.CurrentThread;


            foreach (Ground ground in groundList)
            {
                if (ground.TopTexture != null && !ground.TopTexture.IsRendered)
                    remainingCount++;

                if (ground.BottomTexture != null && !ground.BottomTexture.IsRendered)
                    remainingCount++;
            }

            foreach (Ground ground in groundList)
            {
                if (ground.TopTexture != null && !ground.TopTexture.IsRendered)
                    ThreadPool.QueueUserWorkItem(ThreadPoolCallBackRenderTexture, ground.TopTexture);

                if (ground.BottomTexture != null && !ground.BottomTexture.IsRendered)
                    ThreadPool.QueueUserWorkItem(ThreadPoolCallBackRenderTexture, ground.BottomTexture);
            }

            lock (this)
            {
                if (remainingCount > 0)
                    visitorThread.Suspend();
            }

        }
        #endregion

        #region Event Handlers
        private void ThreadPoolCallBackRenderTexture(object threadContext)
        {
            Texture texture = (Texture)threadContext;
            texture.Render();
            remainingCount--;

            if (remainingCount <= 0)
            {
                visitorThread.Resume();
            }
        }
        #endregion
    }
}
