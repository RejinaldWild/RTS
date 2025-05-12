using _RTSGameProject.Logic.Common.Score.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreMenuUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreGameData _scoreGameData;
        
        public string Id { get; private set; }
        
        [Inject]
        public void Construct(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
        
        private void Start()
        {
            scoreText.text = $"Last score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
        }

        public void Show()
        {
            scoreText.text = $"Last score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
        }

        public void CreateId(string id)
        {
            Id = id;
        }

        public void GiveScoreGameData(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
    }
}
