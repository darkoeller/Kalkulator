using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BiznisSloj.Benefit;
using BiznisSloj.Doprinosi;
using BiznisSloj.Ispis;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace ObracunPlace
{
    /// <summary>
    ///   Interaction logic for BeneficiraniUc.xaml
    /// </summary>
    public partial class BeneficiraniUc
    {
        private readonly ObservableCollection<Beneficirani> _popis;
        private Beneficirani _bene;
        private decimal _beneficirani;
        private decimal _dvadeset;

        public BeneficiraniUc()
        {
            InitializeComponent();
            _popis = new ObservableCollection<Beneficirani>();
            Mediator.GetInstance().NoviBruto += (s, e) => { TxtBruto.Text = e.BrutoIznos; };
            Wait.Visibility = Visibility.Hidden;
        }

        private int Odabrano { get; set; }

        private decimal GetBruto()
        {
            var textBruto = TxtBruto.Text;
            if (textBruto.Contains('.')) textBruto = textBruto.Replace('.', ',');
            decimal.TryParse(textBruto, out var olaksica);
            return olaksica;
        }


        private void BeneficiraniUc_OnLoaded(object sender, RoutedEventArgs e)
        {
            string[] stavke = {"12/14", "12/15", "12/16"};
            CmbVrstaBene.ItemsSource = stavke;
            CmbVrstaBene.SelectedIndex = 0;
            OdDatuma.SelectedDateFormat = DatePickerFormat.Short;
            DoDatuma.SelectedDateFormat = DatePickerFormat.Short;
            OdDatuma.SelectedDate = DateTime.Today.AddMonths(-1);
            DoDatuma.SelectedDate = DateTime.Today.AddMonths(-1);

            ImePrezime.Focus();
        }

        private void CmbVrstaBene_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var odabrano = CmbVrstaBene.SelectedIndex;
            Odabrano = odabrano;
            switch (odabrano)
            {
                case 0:
                    LblBene1.Content = "Benef. stup 1 - 3,61%";
                    LblBene2.Content = "Benef. stup 2 - 1,25%";
                    LblBene1I2.Content = "Benef. stup 1 i 2 - 4,86%";
                    break;
                case 1:
                    LblBene1.Content = "Benef. stup 1 - 5,83%";
                    LblBene2.Content = "Benef. stup 2 - 2,01%";
                    LblBene1I2.Content = "Benef. stup 1 i 2 - 7,84%";
                    break;
                case 2:
                    LblBene1.Content = "Benef. stup 1 - 8,39%";
                    LblBene2.Content = "Benef. stup 2 - 2,89%";
                    LblBene1I2.Content = "Benef. stup 1 i 2 - 11,28%";
                    break;
                default:
                    LblBene1.Content = "Beneficirani za stup 1.";
                    LblBene2.Content = "Beneficirani za 2. stup";
                    LblBene1I2.Content = "Benefeficirani stup 1 i 2";
                    break;
            }

            OcistiLabele();
            ImePrezime.Focus();
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBruto.Text)) return;
            _bene = new Beneficirani();
            IzracunajPetPetnaest();
            IzracunajBeneficirani();
            TxtUkupno.Text = Math.Round(_beneficirani + _dvadeset, 2).ToString(new CultureInfo("hr-HR"));
            ImePrezime.Focus();
            var oddatuma = OdDatuma.SelectedDate.ToString();
            oddatuma = oddatuma.TrimEnd('0', ':');
            _bene.OdDatuma = oddatuma;
            var dodatuma = DoDatuma.SelectedDate.ToString();
            dodatuma = dodatuma.TrimEnd('0', ':');
            _bene.DoDatuma = dodatuma;
            _bene.Bruto = GetBruto();
            _bene.Vrsta = CmbVrstaBene.SelectedItem.ToString();
            _bene.Ukupno = _beneficirani + _dvadeset;
            _bene.Ime = ImePrezime.Text;
            PopuniDataGrid();
        }

        private void PopuniDataGrid()
        {
            _popis.Add(_bene);
            DataGridBene.ItemsSource = _popis;
            DataGridBene.UpdateLayout();
        }

        private void IzracunajBeneficirani()
        {
            var odabir = new OdabireVrstuBeneficirano(GetBruto(), Odabrano);
            odabir.VratiBeneficirani();
            var bene1 = odabir.Beneficirani1;
            _bene.Beneficirani1 = bene1;
            var bene2 = odabir.Beneficirani2;
            _bene.Beneficirani2 = bene2;
            _beneficirani = bene1 + bene2;
            TxtBene1.Text = Math.Round(bene1, 2).ToString(new CultureInfo("hr-HR"));
            TxtBene2.Text = Math.Round(bene2, 2).ToString(new CultureInfo("hr-HR"));
            TxtUkBene1I2.Text = Math.Round(bene1 + bene2, 2).ToString(new CultureInfo("hr-HR"));
        }

        private void IzracunajPetPetnaest()
        {
            var petnaest = new DoprinosPetnaestPosto();
            var doprinospetnaest = petnaest.RacunajDoprinos(GetBruto());
            _bene.Doprinos15 = doprinospetnaest;
            var pet = new DoprinosPetPosto();
            var doprinospet = pet.RacunajDoprinos(GetBruto());
            _bene.Doprinos5 = doprinospet;
            _dvadeset = doprinospet + doprinospetnaest;
            TxtDop15.Text = Math.Round(doprinospetnaest, 2).ToString(new CultureInfo("hr-HR"));
            TxtDop5.Text = Math.Round(doprinospet, 2).ToString(new CultureInfo("hr-HR"));
            Txt20.Text = Math.Round(doprinospet + doprinospetnaest, 2).ToString(new CultureInfo("hr-HR"));
        }

        private void BtnOcisti_Click(object sender, RoutedEventArgs e)
        {
            TxtBruto.Text = "0.00";
            OcistiLabele();
            ImePrezime.Focus();
        }

        private void OcistiLabele()
        {
            TxtBene1.Text = "";
            TxtBene1.Text = "";
            TxtBene2.Text = "";
            TxtDop15.Text = "";
            TxtDop5.Text = "";
            Txt20.Text = "";
            TxtUkBene1I2.Text = "";
            TxtUkupno.Text = "";
            ImePrezime.Text = "";
        }

        private void BtnIspis_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard) FindResource("WaitStoryboard")).Begin();
            Wait.Visibility = Visibility.Visible;
            try
            {
                var doc = new Document(PageSize.A4.Rotate(), 20, 15, 25, 30);
                var pdwri = PdfWriter.GetInstance(doc
                    , new FileStream("Ispis.pdf", FileMode.Create, FileAccess.Write, FileShare.None));
                var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
                //// var times2 = FontFactory.GetFont(FontFactory.TIMES, 9,new CMYKColor(100, 70, 29,25));
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
                //var bojaFonta = new BaseColor(0,0,255);
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
                doc.Add(
                    new Paragraph("IZNOSI DOPRINOSA ZA BENEFICIRANI STAŽ") {SpacingBefore = 10f, Alignment = 1});
                var centar = new PdfPTable(DataGridBene.Columns.Count) {SpacingBefore = 10f};

                foreach (var k in DataGridBene.Columns) centar.AddCell(new Phrase(k.Header.ToString(), desetka));
                centar.HeaderRows = 1;
                float[] sirina = {20f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f};
                centar.SetWidths(sirina);
                var izvor = DataGridBene.ItemsSource;
                if (izvor != null)
                    foreach (var item in izvor)
                    {
                        if (!(DataGridBene.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow red))
                            continue;
                        var presenter = FindVisualChild<DataGridCellsPresenter>(red);
                        for (var i = 0; i < DataGridBene.Columns.Count; ++i)
                        {
                            var cell = (DataGridCell) presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            if (cell.Content is TextBlock txt) centar.AddCell(new Phrase(txt.Text, desetka));
                        }
                    }

                centar.HorizontalAlignment = 1;
                doc.Add(centar);
                pdwri.PageEvent = new Footer();
                doc.Close();
                Process.Start("Ispis.pdf");
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do pogreške, zatvorite otvoren .pdf dokument!", "Pozor");
            }

            ((Storyboard) FindResource("WaitStoryboard")).Stop();
            Wait.Visibility = Visibility.Hidden;
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T visualChild) return visualChild;
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null) return childOfChild;
            }

            return null;
        }

        private void OcistiListu_Click(object sender, RoutedEventArgs e)
        {
            _popis.Clear();
            DataGridBene.UpdateLayout();
        }
    }
}