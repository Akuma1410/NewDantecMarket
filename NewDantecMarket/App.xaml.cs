using NewDantecMarket.Vues;

namespace NewDantecMarket
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new VueLogin()));
        }
    }
}