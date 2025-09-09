using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIScript : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text hp;
    public Slider hpBar;
    [SerializeField]
    private PlayerController _playerController;
    public void UpdateUI()
    {
        score.text = "Очки: " + _playerController.score.ToString();
        hp.text = _playerController.hp.ToString() + "/" + _playerController.maxHp.ToString();
        hpBar.maxValue = _playerController.maxHp;
        hpBar.value = _playerController.hp;
    }
}
