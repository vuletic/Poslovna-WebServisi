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

            font = new XFont("Verdana", 10, XFontStyle.Regular);
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

            font = new XFont("Verdana", 8, XFontStyle.Regular);

            gfx.DrawString("Šifra", font, XBrushes.Black,
              new XRect(20, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Roba", font, XBrushes.Black,
              new XRect(50, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Količina", font, XBrushes.Black,
              new XRect(160, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("M.j.", font, XBrushes.Black,
              new XRect(220, 90, page.Width, page.Height),
              XStringFormats.TopLeft);
            
            gfx.DrawString("Cena", font, XBrushes.Black,
              new XRect(250, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Nab. vr.", font, XBrushes.Black,
              new XRect(300, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Zav. troškovi", font, XBrushes.Black,
              new XRect(350, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Kalk. cena", font, XBrushes.Black,
              new XRect(410, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Uk. vrednost", font, XBrushes.Black,
              new XRect(470, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            int razmak = 0;

            foreach(Stavka_dokumenta sd in pd.Stavka_dokumenta){
                gfx.DrawString(sd.Roba.Id_Roba.ToString(), font, XBrushes.Black,
                  new XRect(20, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Roba.Naziv_Roba, font, XBrushes.Black,
                  new XRect(50, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Kolicina_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(160, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Roba.Jedinica_mere.Oznaka_Jedinica_mere, font, XBrushes.Black,
                  new XRect(220, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Nabavna_cena_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(250, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Nabavna_vrednost_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(300, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString((sd.Zavisni_trosak_Stavka_dokumenta + sd.Transportni_trosak_Stavka_dokumenta).ToString(), font, XBrushes.Black,
                  new XRect(350, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Kalkulisana_cena_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(410, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(sd.Ukupna_vrednost_Stavka_dokumenta.ToString(), font, XBrushes.Black,
                  new XRect(470, 120 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                razmak += 15;
            }

            gfx.DrawLine(XPens.Black, 20, 120 + razmak, page.Width - 20, 120 + razmak);

            gfx.DrawString("Ukupna vrednost: " + pd.Ukupna_vrednost_Prijemni_dokument, font, XBrushes.Black,
                  new XRect(20, 130 + razmak, page.Width, page.Height),
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

        [Route("api/pdf/kartica/{id}")]
        [HttpGet]
        public HttpResponseMessage Kartica(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Robna_kartica rk = (from r in db.Robna_kartica.Include("Roba.Jedinica_mere").Include("Poslovna_godina").Include("Magacin").Include("Analitika_magacinske_kartice")
                                    where r.Id_Robna_kartica == id
                                    select r).FirstOrDefault();

            PdfDocument document = new PdfDocument();
            //document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 16, XFontStyle.Regular);

            // Draw the text 
            gfx.DrawString("Robna kartica", font, XBrushes.Black,
              new XRect(0, 15, page.Width, page.Height / 8),
              XStringFormats.TopCenter);

            font = new XFont("Verdana", 10, XFontStyle.Regular);

            gfx.DrawString("Skladište: " + rk.Magacin.Naziv_Magacin, font, XBrushes.Black,
              new XRect(20, 40, page.Width, page.Height / 8),
              XStringFormats.TopLeft);

            gfx.DrawString("Roba: " + rk.Roba.Naziv_Roba, font, XBrushes.Black,
              new XRect(20, 55, page.Width, page.Height / 8),
              XStringFormats.TopLeft);

            gfx.DrawString("Poslovna godina: " + rk.Poslovna_godina.Godina_Poslovna_godina, font, XBrushes.Black,
              new XRect(page.Width/2 + 100, 40, page.Width, page.Height / 8),
              XStringFormats.TopLeft);

            gfx.DrawString("Jedinica mere: " + rk.Roba.Jedinica_mere.Oznaka_Jedinica_mere, font, XBrushes.Black,
              new XRect(page.Width / 2 + 100, 55, page.Width, page.Height / 8),
              XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, 20, 105, page.Width - 20, 105);

            font = new XFont("Verdana", 8, XFontStyle.Regular);

            gfx.DrawString("Rbr.", font, XBrushes.Black,
              new XRect(20, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Vrsta", font, XBrushes.Black,
              new XRect(60, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Smer", font, XBrushes.Black,
              new XRect(120, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Količina", font, XBrushes.Black,
              new XRect(160, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Jedinična cena", font, XBrushes.Black,
              new XRect(250, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Vrednost", font, XBrushes.Black,
              new XRect(340, 90, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString("Stanje", font, XBrushes.Black,
              new XRect(430, 90, page.Width, page.Height),
              XStringFormats.TopLeft);


            int razmak = 0;

            var an = rk.Analitika_magacinske_kartice.OrderBy(a => a.Redni_broj_Analitika_magacinske_kartice);

            foreach (Analitika_magacinske_kartice a in an)
            {
                gfx.DrawString(a.Redni_broj_Analitika_magacinske_kartice.ToString(), font, XBrushes.Black,
                  new XRect(20, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Vrsta_prometa_Analitika_magacinske_kartice, font, XBrushes.Black,
                  new XRect(60, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Smer_Analitika_magacinske_kartice, font, XBrushes.Black,
                  new XRect(120, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Kolicina_Analitika_magacinske_kartice.ToString(), font, XBrushes.Black,
                  new XRect(160, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Cena_Analitika_magacinske_kartice.ToString(), font, XBrushes.Black,
                  new XRect(250, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Vrednost_Analitika_magacinske_kartice.ToString(), font, XBrushes.Black,
                  new XRect(340, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                gfx.DrawString(a.Ukupno_stanje_Analitika_magacinske_kartice.ToString(), font, XBrushes.Black,
                  new XRect(430, 115 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

                razmak += 15;
            }

            gfx.DrawLine(XPens.Black, 20, 115 + razmak, page.Width - 20, 115 + razmak);

            gfx.DrawString("Prosečna cena: " + rk.Prosecna_cena_Robna_kartica, font, XBrushes.Black,
                  new XRect(20, 125 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Promet ulaza količinski: " + rk.Promet_ulaza_kol_Robna_kartica, font, XBrushes.Black,
                  new XRect(20, 140 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Promet izlaza količinski: " + rk.Promet_izlaza_kol_Robna_kartica, font, XBrushes.Black,
                  new XRect(20, 155 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Ukupna količina: " + rk.Ukupna_kolicina_Robna_kartica, font, XBrushes.Black,
                  new XRect(20, 170 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Promet ulaza vrednosno: " + rk.Promet_ulaza_vr_Robna_kartica, font, XBrushes.Black,
                  new XRect(page.Width / 2 + 100, 140 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Promet izlaza vrednosno: " + rk.Promet_izlaza_vr_Robna_kartica, font, XBrushes.Black,
                  new XRect(page.Width / 2 + 100, 155 + razmak, page.Width, page.Height),
                  XStringFormats.TopLeft);

            gfx.DrawString("Ukupna vrednost: " + rk.Ukupna_vrednost_Robna_kartica, font, XBrushes.Black,
                  new XRect(page.Width / 2 + 100, 170 + razmak, page.Width, page.Height),
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
