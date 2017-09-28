using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ObracunPlace
{
    public partial class MainWindow
    {
        //delegate void PokreniIspis();
        private void Ispis_Click(object sender, RoutedEventArgs e)
        {
            if (_listica == null)
            {
                MessageBox.Show("Provjerite da li ste izračunali iznose", "Pozor", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            using (var doc = new Document(PageSize.A4, 20, 15, 25, 30))
            {
                try
                {
                    var pdwri = PdfWriter.GetInstance(doc,
                        new FileStream("PlatnaLista.pdf", FileMode.Create, FileAccess.Write, FileShare.None));
                    var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
                    // var times2 = FontFactory.GetFont(FontFactory.TIMES, 9,new CMYKColor(100, 70, 29,25));
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
                    doc.Add(headertablica);
                    doc.Add(new Paragraph(NaslovniText.Text, desetka) {SpacingBefore = 20f, Alignment = 1});
                    doc.Add(new Paragraph(" "));
                    //ovdje dođe tijelo platne liste
                    var listaIznosa = new PdfPTable(2);
                    float[] sirina = {20f, 8f};
                    listaIznosa.SpacingBefore = 10f;
                    listaIznosa.SpacingAfter = 30f;
                    listaIznosa.SetWidths(sirina);
                    listaIznosa.WidthPercentage = 50;
                    listaIznosa.AddCell(new Phrase(" OPISI STAVKI ", times));
                    listaIznosa.AddCell(new Phrase(" IZNOSI", times));
                    listaIznosa.AddCell(new Phrase("Bruto iznos : ", times));
                    listaIznosa.AddCell(new Phrase(Bruto.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinos 15% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.PetnaestPostoDoprinos.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinos 5% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.PetPostoDoprinos.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinos 20% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DvadesetPostoDoprinos.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinosi iz plaće ukupno : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DoprinosiIzPlaceUkupno.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Dohodak (bruto - doprinosi) : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.Dohodak.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Iznos olakšice (vaš neoporezivi iznos) : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.Olaksica.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Porezna osnovica (dohodak - olakšica) : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.PoreznaOsnovica.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Porez po stopi od 24% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.PorezDvadesetCetiriPosto.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Porez po stopi od 36% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.PorezTridesetSestPosto.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Prirez (u vašem gradu ili općini) : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.Prirez.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Ukupno porezi + prirez : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.UkupniPorez.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Neto plaća : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.Neto.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Neto iznos + naknada za prijevoz : ", times));
                    listaIznosa.AddCell(new Phrase(LblPrijevoz.Content.ToString(), times));
                    var labOdbici =
                        new PdfPCell(new Phrase("Neto iznos - osobni odbici (za isplatu) : ", times))
                        {
                            BackgroundColor = new BaseColor(0, 255, 255)
                        };
                    listaIznosa.AddCell(labOdbici);
                    var totalOdbici =
                        new PdfPCell(new Phrase(LblOdbici.Content.ToString(), times))
                        {
                            BackgroundColor = new BaseColor(0, 255, 255)
                        };
                    listaIznosa.AddCell(totalOdbici);
                    listaIznosa.AddCell(new Phrase("Doprinos za zdravstveno 15% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DoprinosZaZdravstveno.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinos za zaštitu na radu 0,5% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DoprinosZaZnr.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinos za zapošljavanje 1,7% : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DoprinosZaZaposljavanje.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Doprinosi na plaću ukupno (15% + 0,5% + 1,7%) : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.DoprinosNaPlacUkupno.ToString("c"), times));
                    listaIznosa.AddCell(new Phrase("Ukupan trošak plaće za poslodavca : ", times));
                    listaIznosa.AddCell(new Phrase(_listica.UkupniTrosakPlace.ToString("c"), times));
                    doc.Add(listaIznosa);
                    doc.Add(new Paragraph("Obračun izradio ref. Obračuna plaće", times){Alignment = 2});
                    doc.Add(new Paragraph("Darko Eller", times) {SpacingBefore = 10f, Alignment = 2});
                    //Footer
                    pdwri.PageEvent = new Footer();
                    doc.Close();
                    Process.Start("PlatnaLista.pdf");
                }
                catch (Exception)
                {
                    MessageBox.Show("Došlo je do pogreške, zatvorite otvoren .pdf dokument!", "Pozor",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}