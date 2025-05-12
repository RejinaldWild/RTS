using System;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreGameUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreGameController _scoreGameController;
        private ScoreGameData _scoreGameData;
        
        public string Id { get; private set; }
        
        [Inject]
        public void Construct(ScoreGameData scoreGameData)
        {
            _scoreGameData = scoreGameData;
        }
        
        private void Start()
        {
            Id = Guid.NewGuid().ToString();
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        public void Show()
        {
            scoreText.text = $"Score - Win: {_scoreGameData.WinScore} - Lose: {_scoreGameData.LoseScore}";
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
