using _RTSGameProject.Logic.Common.Score.Model;
using TMPro;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreGameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private ScoreGameData _scoreGameData;
        
        public void Show()
        {
            if (_scoreGameData != null)
            {
                _scoreText.text = $"Score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
            }
        }
        
        public void GiveScoreGameData(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
    }
}
