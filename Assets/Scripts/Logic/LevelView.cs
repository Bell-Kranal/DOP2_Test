using TMPro;
using UnityEngine;

namespace Logic
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _descriptionLevelText;
        
        private const string Level = "Уровень: ";

        public void SetValues(int currentLevel, string description)
        {
            _currentLevelText.text = Level + (currentLevel + 1);
            _descriptionLevelText.text = description;
        }
    }
}
