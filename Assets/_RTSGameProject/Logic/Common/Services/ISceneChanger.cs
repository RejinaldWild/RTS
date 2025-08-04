namespace _RTSGameProject.Logic.Common.Services
{
    public interface ISceneChanger
    {
        public void ToStartGame();
        public void ToLoadGame();
        public void ToQuitGame();
        public void ToMainMenu();
        public void ToNextLevel();
    }
}