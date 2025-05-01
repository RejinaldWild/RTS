using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Common.Score.View
{
    public class ScoreMenuUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI scoreText;
        
        private ScoreMenuData _scoreMenuData;
        
        public string Id { get; private set; }
        
        [Inject]
        public void Construct(ScoreMenuData scoreMenuData)
        {
            _scoreMenuData = scoreMenuData;
        }
        
        private void Start()
        {
            scoreText.text = "Score - Win: 0 - Lose: 0";
        }

        public void Show()
        {
            scoreText.text = $"Score - Win: {_scoreMenuData.WinScore} - Lose: {_scoreMenuData.LoseScore}";
        }

        public void CreateId(string id)
        {
            Id = id;
        }
    }
}
