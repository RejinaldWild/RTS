using System;
using _RTSGameProject.Logic.Common.Services;
using TMPro;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreGameUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreGameController _scoreGameController;
        
        public string Id { get; private set; }
        
        public void Construct(ScoreGameController scoreGameController)
        {
            _scoreGameController = scoreGameController;
        }
        
        private void Start()
        {
            Id = Guid.NewGuid().ToString();
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        private void Update()
        {
            _scoreGameController.Show();
        }
        
        public void CreateId(string id)
        {
            Id = id;
        }
    }
}
