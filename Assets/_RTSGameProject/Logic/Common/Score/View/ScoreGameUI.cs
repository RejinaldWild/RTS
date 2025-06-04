using _RTSGameProject.Logic.Common.Score.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreGameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private ScoreGameData _scoreGameData;
        
        [Inject]
        public void Construct(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
        
        private void Start()
        {
            _scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        public void Show()
        {
            _scoreText.text = $"Score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
        }
        
        public void GiveScoreGameData(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
    }
}
