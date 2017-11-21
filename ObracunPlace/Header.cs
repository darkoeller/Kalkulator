using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ObracunPlace
{
    internal class Header : PdfPageEventHelper
    {
// Fields...
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            // var times2 = FontFactory.GetFont(FontFactory.TIMES, 9,new CMYKColor(100, 70, 29,25));
            var times = new Font(bfTimes, 9);
            // var desetka = new Font(bfTimes, 10);
            // document.Open();
            //kod za cijeli header
            var logo = Image.GetInstance("logo.png");
            logo.ScalePercent(50f);
            document.Add(logo);
            document.Add(new Paragraph("Petrokemija, d.d tvornica gnojiva", times));
            document.Add(new Paragraph("Aleja Vukovar 4", times));
            document.Add(new Paragraph("44320 Kutina", times));
            document.Add(new Paragraph("Hrvatska", times));
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph("Ured Uprave", times));
            document.Add(new Paragraph("Tel.: +385 44 647 270", times));
            document.Add(new Paragraph("Fax: +385 44 680 882", times));
            document.Add(new Paragraph("E-mail: antonija.galovic@petrokemija.hr", times));
            //var bojaFonta = new BaseColor(0,0,255);
            var anchor = new Anchor("www.petrokemija.hr", times) {Reference = "http://www.petrokemija.hr"};
            document.Add(anchor);
            document.Add(new Paragraph(" "));
            var headertablica = new PdfPTable(2) {TotalWidth = 100f, LockedWidth = true, HorizontalAlignment = 0};
            var kutina = new Paragraph("Kutina, ", times);
            var celijakutina = new PdfPCell(kutina)
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = Rectangle.NO_BORDER
            };
            var datum = new Phrase(DateTime.Now.Date.ToString("d"), times);
            var celijadatum = new PdfPCell(datum)
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = Rectangle.NO_BORDER
            };
            headertablica.AddCell(celijakutina);
            headertablica.AddCell(celijadatum);
            document.Add(headertablica);
        }
    }
}