using System;
using _RTSGameProject.Logic.Common.Score.Model;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreGameUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        public ScoreGameData ScoreGameData;
        
        public string Id { get; private set; }
        
        [Inject]
        public void Construct(ScoreGameData scoreGameData)
        {
            ScoreGameData = scoreGameData;
        }
        
        private void Start()
        {
            Id = Guid.NewGuid().ToString();
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        public void Show()
        {
            scoreText.text = $"Score - Win: {ScoreGameData.WinScore} - Lose: {ScoreGameData.LoseScore}";
        }
        
        public void CreateId(string id)
        {
            Id = id;
        }

        public void GiveScoreGameData(ScoreGameData scoreGameData)
        {
            ScoreGameData = scoreGameData;
        }
    }
}
