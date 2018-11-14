using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BiznisSloj.Ispis
{
    public class Footer : PdfPageEventHelper

    {
        public override void OnEndPage(PdfWriter writer, Document doc)

        {
            var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            var maliFont = new Font(bfTimes, 8);
            doc.Add(
                new Paragraph(
                    "Petrokemija, d.d. tvornica gnojiva, Aleja Vukovar 4, 44320 Kutina; predsjednik Nadzornog odbora: Sandor Fasimon;"
                    , maliFont) {SpacingBefore = 110f, Alignment = 1});
            doc.Add(
                new Paragraph("Uprava: mr.sc. Davor Žmegač - predsjednik Uprave, Krešimir Rendeli - član, Goran Pleše - član, Peter Suba - član;"
                    , maliFont) {Alignment = 1});
            doc.Add(new Paragraph(
                "OIB 24503685008; PDV identifikacijski broj: HR24503685008; IBAN: HR45 2340 0091 1001 2191 2 kod Privredne banke Zagreb d.d.,"
                , maliFont) {Alignment = 1});
            doc.Add(new Paragraph(
                "temeljni kapital: 386.135.400,00 kuna; izdano 12.871.180 dionica koje glase na ime, nominalne vrijednosti 30,00 kn."
                , maliFont) {Alignment = 1});
        }
    }
}