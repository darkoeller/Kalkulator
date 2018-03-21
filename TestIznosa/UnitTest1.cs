using BiznisSloj;
using BiznisSloj.Doprinosi;
using BiznisSloj.KoefSati;
using BiznisSloj.Olaksice;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestIznosa
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProcesaVisokogNeta()
        {
            var neto = new ProcesuirajNeto(33000m, 2.7m, 12m);
            neto.Izracunaj();
            Assert.AreEqual(neto.Bruto, 55539.28m );
        }
        [TestMethod]
        public void TestProcesaVisokogNeta2()
        {
            var neto = new ProcesuirajNeto(27000m, 1m, 18m);
            neto.Izracunaj();
            Assert.AreEqual(neto.Bruto, 49449.73m );
        }
        
        [TestMethod]
        public void TestDoprinosa()
        {
            var doprinosi = new RacunajDoprinoseIzPlace(6000);
            doprinosi.Izracun();
            var dop = doprinosi.VratiDoprinose();
            Assert.AreEqual(dop, 1200m);
        }

        [TestMethod]
        public void TestDoprinosaNaPlacu()
        {
            var doprinosinp = new RacunajDoprinoseNaPlacu(6500.0m);
            doprinosinp.Izracun();
            var dnp = doprinosinp.VratiDoprinoseNaPlacu();
            Assert.AreEqual(dnp, 1118.0m);
        }

        [TestMethod]
        public void VratiPoreznuOlaksicu()
        {
            var olaksica = new IzracunOlaksice(2.2m);
            var izracun = olaksica.VratiOlaksicu();
            Assert.AreEqual(izracun, 6800.0m);
        }



        [TestMethod]
        public void IzracunajKoeficijentSat()
        {
            var koef = new KoefSatSamoPrva(40m, 0.14m, 2.55m, 1m);
            var izracun = koef.RacunajKoefSat();
            Assert.AreEqual(izracun, 1123.75m);
        }

        [TestMethod]
        public void IzracunajKoefSatSmjene()
        {
            var koef = new KoeficijentSatSmjene(3.4m, 0.59m, 3.2m, 1.65m);
            var izracun = koef.RacunajKoefSat();
            Assert.AreEqual(izracun, 212.15m);
        }

        [TestMethod]
        public void ProvjeraVracanjaIznosa()
        {
            var iznos = Koeficijenti.VratiIznos("Naknada za G.O. nedjeljom noću");
            Assert.AreEqual(iznos, 1.85m);
        }

        [TestMethod]
        public void IzracunKoeficientSati()
        {
            var iznos = new IzracunajKoeficijentSate(48m, 0.46m, 2.55m, 1m);
            var rezultat = iznos.Izracun();
            Assert.AreEqual(rezultat, 1348.51m);
        }

        [TestMethod]
        public void IzracunGodinaStaza()
        {
            var god = new IzracunGodinaStaza(31, 2.5m);
            var rez = god.Izracun();
            Assert.AreEqual(rez, 0.39m);
        }

        [TestMethod]
        public void IzracunajBeneficiraniDvaMjeseca()
        {
            var bene = new RacunajBeneficiraniDvaMjeseca(366257.13m);
            bene.Izracun();
            var rezultat = bene.VratiBeneDvaMjeseca();
            Assert.AreEqual(rezultat, 17800.10m);
        }
    }
}