using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BiznisSloj.Procesi;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BiznisSloj.Ispis
{
    public class IspisListicePlace
    {
        private readonly decimal _iznosPrijevoza;
        private readonly string _lblprijevoz;
        private readonly ProcesuirajPlacu _listica;
        private readonly string _naslov;
        private readonly string _odbici;
        private readonly string _prirezTextBox;
        private readonly double? _txtOdbici;

        public IspisListicePlace(PodaciZaIspisPlace podaci)
        {
            _listica = podaci.Placa;
            _iznosPrijevoza = podaci.Prijevoz;
            _txtOdbici = podaci.TxtOdbiciIznos;
            _naslov = podaci.NaslovniText;
            _odbici = podaci.LblOdbici;
            _lblprijevoz = podaci.LblPrijevoz;
            _prirezTextBox = podaci.PrirezTxtB;
        }

        public void Ispis()
        {
            using (var doc = new Document(PageSize.A4, 20, 15, 25, 30))
            {
                var pdwri = PdfWriter.GetInstance(doc
                    , new FileStream("PlatnaLista.pdf", FileMode.Create, FileAccess.Write, FileShare.None));
                var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
                var times = new Font(bfTimes, 9);
                var desetka = new Font(bfTimes, 10);
                doc.Open();
                ////kod za cijeli header
                var logo = Image.GetInstance("logo.png");
                logo.ScalePercent(50f);
                doc.Add(logo);
                doc.Add(new Paragraph("Petrokemija, d.d tvornica gnojiva", times));
                doc.Add(new Paragraph("Aleja Vukovar 4", times));
                doc.Add(new Paragraph("44320 Kutina", times));
                doc.Add(new Paragraph("Hrvatska", times));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Ured Uprave", times));
                doc.Add(new Paragraph("Tel.: +385 44 647 270", times));
                doc.Add(new Paragraph("Fax: +385 44 680 882", times));
                doc.Add(new Paragraph("E-mail: ", times));
                var anchor = new Anchor("www.petrokemija.hr", times) {Reference = "http://www.petrokemija.hr"};
                doc.Add(anchor);
                doc.Add(new Paragraph(" "));
                var headertablica = new PdfPTable(2)
                {
                    TotalWidth = 100f,
                    LockedWidth = true,
                    HorizontalAlignment = 0
                };
                var kutina = new Paragraph("Kutina, ", times);
                var celijakutina = new PdfPCell(kutina)
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
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
                doc.Add(headertablica);
                doc.Add(new Paragraph(_naslov, desetka) {SpacingBefore = 20f, Alignment = 1});
                doc.Add(new Paragraph(" "));
                //ovdje dođe tijelo platne liste
                var listaIznosa = new PdfPTable(2);
                float[] sirina = {16f, 8f};
                listaIznosa.SpacingBefore = 10f;
                listaIznosa.SpacingAfter = 30f;
                listaIznosa.SetWidths(sirina);
                listaIznosa.WidthPercentage = 55;
                var opis = new PdfPCell(new Phrase("OPISI STAVKI", desetka))
                {
                    HorizontalAlignment = 1,
                    FixedHeight = 10f
                };
                listaIznosa.AddCell(opis);
                var iznosi = new PdfPCell(new Phrase("IZNOSI", desetka)) {HorizontalAlignment = 1};
                listaIznosa.AddCell(iznosi);
                listaIznosa.AddCell(new Phrase("Bruto iznos : ", times));
                var brutoIznos =
                    new PdfPCell(new Phrase(Math.Round(_listica.Bruto, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(brutoIznos);
                listaIznosa.AddCell(new Phrase("Doprinos 15% : ", times));
                var petnaestPosto =
                    new PdfPCell(new Phrase(Math.Round(_listica.PetnaestPostoDoprinos, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(petnaestPosto);
                listaIznosa.AddCell(new Phrase("Doprinos 5% : ", times));
                var petPosto =
                    new PdfPCell(new Phrase(Math.Round(_listica.PetPostoDoprinos, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(petPosto);
                listaIznosa.AddCell(new Phrase("Doprinosi iz plaće ukupno (20%) : ", times));
                var dopUkupno =
                    new PdfPCell(new Phrase(Math.Round(_listica.DoprinosiIzPlaceUkupno, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(dopUkupno);
                listaIznosa.AddCell(new Phrase("Dohodak (bruto - doprinosi) : ", times));
                var dohodak =
                    new PdfPCell(new Phrase(Math.Round(_listica.Dohodak, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(dohodak);
                listaIznosa.AddCell(new Phrase("Iznos olakšice (vaš neoporezivi iznos) : ", times));
                var olaksica =
                    new PdfPCell(new Phrase(Math.Round(_listica.Olaksica, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(olaksica);
                listaIznosa.AddCell(new Phrase("Porezna osnovica (dohodak - olakšica) : ", times));
                var porOsnovica =
                    new PdfPCell(new Phrase(Math.Round(_listica.PoreznaOsnovica, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(porOsnovica);
                listaIznosa.AddCell(new Phrase("Porez po stopi od 24% : ", times));
                var dvacetri =
                    new PdfPCell(new Phrase(Math.Round(_listica.PorezDvadesetCetiriPosto, 2).ToString("c"),
                            times))
                        {HorizontalAlignment = 2};
                listaIznosa.AddCell(dvacetri);
                listaIznosa.AddCell(new Phrase("Porez po stopi od 36% : ", times));
                var trisest =
                    new PdfPCell(new Phrase(Math.Round(_listica.PorezTridesetSestPosto, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(trisest);
                listaIznosa.AddCell(
                    new Phrase("Prirez (u vašem gradu ili općini) " + "=(" + _prirezTextBox + "%)" + ": ", times));
                var prirez =
                    new PdfPCell(new Phrase(Math.Round(_listica.Prirez, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(prirez);
                listaIznosa.AddCell(new Phrase("Ukupno porezi + prirez : ", times));
                var ukPorez =
                    new PdfPCell(new Phrase(Math.Round(_listica.UkupniPorez, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(ukPorez);
                listaIznosa.AddCell(new Phrase("Neto plaća : ", times));
                var netoPlaca =
                    new PdfPCell(new Phrase(Math.Round(_listica.Neto, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(netoPlaca);
                listaIznosa.AddCell(
                    new Phrase("Neto iznos + naknada za prijevoz (" + _iznosPrijevoza + "kn" + ") :", times));
                var prijevoz = new PdfPCell(new Phrase(_lblprijevoz, times)) {HorizontalAlignment = 2};
                listaIznosa.AddCell(prijevoz);
                var labOdbici =
                    new PdfPCell(new Phrase(
                        "Neto iznos - osobni odbici (" + _txtOdbici + "kn" + ") = " + " za isplatu :", times))
                    {
                        BackgroundColor = new BaseColor(0, 255, 255)
                    };
                listaIznosa.AddCell(labOdbici);
                var totalOdbici = new PdfPCell(new Phrase(_odbici, times))
                {
                    BackgroundColor = new BaseColor(0, 255, 255),
                    HorizontalAlignment = 2
                };
                listaIznosa.AddCell(totalOdbici);
                listaIznosa.AddCell(new Phrase("Doprinos za zdravstveno 15% : ", times));
                var zdravstveno =
                    new PdfPCell(new Phrase(Math.Round(_listica.DoprinosZaZdravstveno, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(zdravstveno);
                listaIznosa.AddCell(new Phrase("Doprinos za zaštitu na radu 0,5% : ", times));
                var znr = new PdfPCell(new Phrase(Math.Round(_listica.DoprinosZaZnr, 2).ToString("c"), times))
                {
                    HorizontalAlignment = 2
                };
                listaIznosa.AddCell(znr);
                listaIznosa.AddCell(new Phrase("Doprinos za zapošljavanje 1,7% : ", times));
                var zaposljavanje =
                    new PdfPCell(new Phrase(_listica.DoprinosZaZaposljavanje.ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(zaposljavanje);
                listaIznosa.AddCell(new Phrase("Doprinosi na plaću ukupno (15% + 0,5% + 1,7%) : ", times));
                var dopriNpUkupno =
                    new PdfPCell(new Phrase(Math.Round(_listica.DoprinosNaPlacUkupno, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(dopriNpUkupno);
                listaIznosa.AddCell(new Phrase("Ukupan trošak plaće za poslodavca : ", times));
                var ukupTrosak =
                    new PdfPCell(new Phrase(Math.Round(_listica.UkupniTrosakPlace, 2).ToString("c"), times))
                    {
                        HorizontalAlignment = 2
                    };
                listaIznosa.AddCell(ukupTrosak);
                doc.Add(listaIznosa);
                doc.Add(new Paragraph("Obračun izradio/la: ", times)
                {
                    SpacingBefore = 10f,
                    Alignment = 0,
                    IndentationLeft = 80f
                });
                pdwri.PageEvent = new Footer();
                doc.Close();
                //Process.Start("PlatnaLista.pdf");
                Parallel.Invoke(() => Process.Start("PlatnaLista.pdf"));
            }
        }
    }
}