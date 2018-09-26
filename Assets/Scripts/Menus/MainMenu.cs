namespace UnityEngine.UI.Extensions.Examples
{
    public class MainMenu : SimpleMenu<MainMenu>
    {
        
        public void OnPlayPressed()
        {
            GameMenu.Show();
            var g = new GameObject().AddComponent<Game>();
            g.gameObject.name = "Game";
           // g.BeginNewGame();
        }

        public void OnOptionsPressed()
        {
            OptionsMenu.Show();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}