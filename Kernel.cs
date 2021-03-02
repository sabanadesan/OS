using System;
using System.IO;
using System.Drawing;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.Debug.Kernel;

namespace OS
{
    public class Kernel : Sys.Kernel
    {
        public static DoubleBufferedVMWareSVGAII vMWareSVGAII;
        public Debugger debugger = new Debugger("System", "CGS");

        protected override void BeforeRun()
        {
            CosmosVFS cosmosVFS = new CosmosVFS();
            VFSManager.RegisterVFS(cosmosVFS);

            //Bitmap sb = new Bitmap(@"0:\sb.bmp");
            //Bitmap bitmap = new Bitmap(@"0:\logo.bmp");

            vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
            vMWareSVGAII.SetMode(640, 480);

            vMWareSVGAII.DoubleBuffer_Clear((uint)Color.Blue.ToArgb());

            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(100, 100, 100, 100, (uint)Color.Green.ToArgb());

            //vMWareSVGAII.DoubleBuffer_SetVRAM(bitmap.rawData);

            vMWareSVGAII.DoubleBuffer_Update();

        }

        protected override void Run()
        {
            try
            {
                mDebugger.Send("Run");

                Console.ReadKey(true);

                Sys.Power.Shutdown();
            }
            catch (Exception e)
            {
                debugger.Send($"Got fatal exception {e.Message}");
                Console.WriteLine($"Got fatal exception {e.Message}");
            }
        }
    }
}
