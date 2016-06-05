using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class BussinesActionController : ApiController
    {

        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();

        [Route("api/pdf/primka/{id}")]
        [HttpGet]
        public HttpResponseMessage Pdf(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Prijemni_dokument pd = (from p in db.Prijemni_dokument.Include("Stavka_dokumenta.Roba.Jedinica_mere").Include("Poslovna_godina").Include("Poslovni_partner").Include("Magacin1")
                                    where p.Id_Prijemni_dokument == id
                                    select p).FirstOrDefault();

            PdfDocument document = new PdfDocument();
            //document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            
            // Draw the text 
            gfx.DrawString(pd.Poslovna_godina.Godina_Poslovna_godina + "/" + pd.Redni_broj_Prijemni_dokument, font, XBrushes.Black,
              new XRect(0, 40, page.Width, page.Height/8),
              XStringFormats.TopCenter);

            font = new XFont("Verdana", 12, XFontStyle.Regular);

            gfx.DrawString("Prijemni dokument", font, XBrushes.Black,
              new XRect(0, 25, page.Width, page.Height / 8),
              XStringFormats.TopCenter);

            gfx.DrawString("Datum: " + pd.Datum_formiranja_Prijemni_dokument.ToString("dd.MM.yyyy.") , font, XBrushes.Black,
              new XRect(450, 25, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Magacin: " + pd.Magacin1.Naziv_Magacin, font, XBrushes.Black,
              new XRect(20, 25, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Partner: " + pd.Poslovni_partner.Naziv_Partner, font, XBrushes.Black,
              new XRect(20, 45, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, 20, 110, page.Width-20, 110);

            font = new XFont("Verdana", 10, XFontStyle.Regular);

            gfx.DrawString("Šifra", font, XBrushes.Black,
              new XRect(20, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Roba", font, XBrushes.Black,
              new XRect(60, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Količina", font, XBrushes.Black,
              new XRect(180, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("M.j.", font, XBrushes.Black,
              new XRect(260, 90, page.Width, page.Height),
              XStringFormats.TopLeft);
            
            gfx.DrawString("Cena", font, XBrushes.Black,
              new XRect(300, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Vrednost", font, XBrushes.Black,
              new XRect(380, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            int razmak = 0;

            foreach(Stavka_dokumenta sd in pd.Stavka_dokumenta){
                gfx.DrawString(sd.Roba.Id_Roba.ToString(), font, XBrushes.Black,
                  new XRect(20, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Roba.Naziv_Roba, font, XBrushes.Black,
                  new XRect(60, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Kolicina_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(180, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Roba.Jedinica_mere.Oznaka_Jedinica_mere, font, XBrushes.Black,
                  new XRect(260, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Nabavna_cena_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(300, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Nabavna_vrednost_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(380, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                razmak += 20;
            }

            // Save the document...
        
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);

            byte[] bytes = stream.ToArray();
            var res = new HttpResponseMessage();
            res.Content = new ByteArrayContent(bytes);
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            stream.Close();
            return res;
        }

        [Route("api/pdf/kartica/{id}")]
        [HttpGet]
        public HttpResponseMessage Kartica(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.TopCenter);

            font = new XFont("Verdana", 12, XFontStyle.Regular);

            gfx.DrawString("Hello, Worasdld!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.TopLeft);

            // Save the document...

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);

            byte[] bytes = stream.ToArray();
            var res = new HttpResponseMessage();
            res.Content = new ByteArrayContent(bytes);
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            stream.Close();
            return res;
        }

        [Route("api/calculate/{id}")]
        [HttpPost]
        public bool Calculate(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Iskalkulisi(id);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [Route("api/record/{id}")]
        [HttpPost]
        public bool Record(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Proknjizi(id, false);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [Route("api/cancel/{id}")]
        [HttpPost]
        public bool Cancel(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Proknjizi(id, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
