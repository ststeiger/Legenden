
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace DrawLegends
{


    static class Program
    {



        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string strURL = "http://www.cor-management.ch/corwebsite/pictures/Titelbild_01.jpg";
            // Tools.Web.DownloadHelper.GetFileAsByteArray(strURL, @"d:\test1.jpg");
            // Tools.Web.DownloadHelper.GetHeaders(strURL);
            // Tools.Web.DownloadHelper.SaveFileToPath(strURL);
            
            //byte[] ba = Tools.Web.DownloadHelper.GetFileAsByteArray(strURL);
            ////byte[] baPDF = Tools.FileFormat.PDF.ImageDataToPdfData(ba);
            //byte[] baPDF = Tools.FileFormat.PDF.ImageToPdfData(System.Drawing.Image.FromFile(@"d:\aalol.png"));

            //System.IO.File.WriteAllBytes(@"d:\myimage.pdf", baPDF);

            bool bShow = true;
            if (bShow)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                return;
            } // End if (bShow)


            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" --- Press any key to continue --- ");
            Console.ReadKey();
        } // End Sub Main


        public static void HSLColorTest()
        {
            Tools.ColorSpace.HSLColor x = new Tools.ColorSpace.HSLColor(0.5, 1.0, 0.25);
            Console.WriteLine(x.Color);

            System.Drawing.Color col = System.Drawing.Color.HotPink;
            Tools.ColorSpace.HSLColor hsl = new Tools.ColorSpace.HSLColor(col);
            Console.WriteLine(hsl.Color);
        } // End Sub HSLColorTest


    } // End Class Program


} // End Namespace DrawLegends
