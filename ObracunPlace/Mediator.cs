using System;

namespace ObracunPlace
{
    public sealed class Mediator
    {
        private static readonly Mediator Instance = new Mediator();

        private Mediator()
        {
        }

        public static Mediator GetInstance() => Instance;

        public event EventHandler<BrutoEventArgs> NoviBruto;

        public void OnNoviBruto(object sender, string bruto)
        {
            if (NoviBruto is EventHandler<BrutoEventArgs> noviBruto)
                noviBruto(sender, new BrutoEventArgs {BrutoIznos = bruto});
        }
    }
}