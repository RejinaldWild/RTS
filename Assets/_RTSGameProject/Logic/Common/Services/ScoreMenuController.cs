using _RTSGameProject.Logic.Common.SaveLoad;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Score.View;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _RTSGameProject.Logic.Common.Services
{
    public class ScoreMenuController: ITickable
    {
        private readonly ScoreMenuUI _scoreMenuUI;
        private readonly ISaveService _saveService;
        public ScoreGameData ScoreGameData { get; set; }

        public ScoreMenuController(ScoreMenuUI scoreMenuUI,
                                    ISaveService saveService)
        {
            _scoreMenuUI = scoreMenuUI;
            _saveService = saveService;
        }

        public void InitializeScoreGameData()
        {
            ScoreGameData = new ScoreGameData();
        }
        
        public async UniTask LoadDataAsync()
        {
            ScoreGameData = await _saveService.LoadAsync();
        }

        public void Tick()
        {
            _scoreMenuUI.Show();
        }
        
        public void GetDataToShowScore(ScoreGameData scoreGameData)
        {
            _scoreMenuUI.GiveScoreGameData(scoreGameData);
        }
        
        public async UniTask DeleteSaves()
        {
            await _saveService.DeleteAsync();
            _scoreMenuUI.GiveScoreGameData(new ScoreGameData());
        }
    }
}
