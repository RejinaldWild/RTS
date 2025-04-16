using _RTSGameProject.Logic.Common.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreMenuUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreMenuController _scoreMenuController;
        
        public string Id { get; private set; }
        
        public void Construct(ScoreMenuController scoreMenuController)
        {
            _scoreMenuController = scoreMenuController;
        }
        
        private void Start()
        {
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        private void Update()
        {
            _scoreMenuController.Show();
        }

        public void CreateId(string id)
        {
            Id = id;
        }
    }
}
