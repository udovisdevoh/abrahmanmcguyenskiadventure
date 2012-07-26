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

            List<Thread> threadList = new List<Thread>();

            foreach (Ground ground in groundList)
            {
                if (ground.TopTexture != null && !ground.TopTexture.IsRendered)
                {
                    remainingCount++;
                    ground.TopTexture.RenderingComplete += TextureRenderingCompleteHandler;
                    Thread workerThread = new Thread(ground.TopTexture.Render);
                    threadList.Add(workerThread);
                }

                if (ground.BottomTexture != null && !ground.BottomTexture.IsRendered)
                {
                    remainingCount++;
                    ground.BottomTexture.RenderingComplete += TextureRenderingCompleteHandler;
                    Thread workerThread = new Thread(ground.BottomTexture.Render);
                    threadList.Add(workerThread);
                }
            }

            foreach (Thread thread in threadList)
                thread.Start();

            visitorThread.Suspend();

        }
        #endregion

        #region Event Handlers
        private void TextureRenderingCompleteHandler(object sender, EventArgs e)
        {
            remainingCount--;

            if (remainingCount <= 0)
                visitorThread.Resume();
        }
        #endregion
    }
}
