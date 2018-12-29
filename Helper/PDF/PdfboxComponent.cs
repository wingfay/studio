using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.awt;
using java.io;
using java.awt.image;
using javax.imageio;
using org.apache.pdfbox.exceptions;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.pdmodel.edit;
using org.apache.pdfbox.pdmodel.graphics.xobject;
using org.apache.pdfbox.util;

namespace Helper.PDF
{
    public class PdfboxComponent : IPdfComponentFunc
    {
        public string ComponentName => "PDFBox";

        public void AddWaterprint(string absoluteFilePath, string outputPath)
        {


            //var watermarkImgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DefaultResource", "waterMarkImg.jpeg");
            //using (PDDocument pdf = PDDocument.load(absoluteFilePath))
            //{
            //    // Load watermark
            //    java.awt.Image srcImg = ImageIO.read(new FileInputStream(watermarkImgPath));

            //    System.Console.Write("width:" + srcImg.getWidth(null));
            //    System.Console.Write("height:" + srcImg.getHeight(null));

            //    BufferedImage buffImg = new BufferedImage(srcImg.getWidth(null),
            //    srcImg.getHeight(null), BufferedImage.TYPE_INT_RGB);

            //    PDJpeg watermark = new PDJpeg(pdf, buffered);


            //    w

            //    using (var watermarkImg = PDXObjectImage.createThumbnailXObject(watermarkImgPath))
            //    {
            //        // Loop through pages in PDF
            //        var pages = pdf.getDocumentCatalog().getAllPages();
            //        var iter = pages.iterator();
            //        while (iter.hasNext())
            //        {
            //            PDPage page = (PDPage)iter.next();

            //            // Add watermark to individual page
            //            PDPageContentStream stream = new PDPageContentStream(pdf, page, true, false);
            //            stream.drawImage(watermarkImg, 100, 0);
            //            stream.close();
            //        }

            //        try
            //        {
            //            pdf.save(outputPath);
            //        }
            //        catch (COSVisitorException e)
            //        {
            //            e.printStackTrace();
            //        }
            //    }
            //}
        }

        public void FromXps(string absoluteFilePath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void InsertPage(string absoluteFilePath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void ToHtml(string absoluteFilePath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void ToJpeg(string absoluteFilePath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public void ToTxt(string absoluteFilePath, string outputPath)
        {
            using (PDDocument pdf = PDDocument.load(new java.io.File(absoluteFilePath)))
            {
                Writer output = new PrintWriter(outputPath, "utf-8");
                //new PDFDomTree().writeText(pdf, output);
                output.close();
            }

                
            
            
        }
    }
}
