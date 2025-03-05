using System;
using _RTSGameProject.Logic.Common.Services;
using TMPro;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreController _scoreController;
        
        public string Id { get; private set; }
        
        public void Construct(ScoreController scoreController)
        {
            _scoreController = scoreController;
        }
        
        void Start()
        {
            Id = Guid.NewGuid().ToString();
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        void Update()
        {
            _scoreController.Show();
        }
    }
}
