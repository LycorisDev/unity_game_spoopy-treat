using System.Linq;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private TextMeshProUGUI _tmpro;
    private Character _playerScript;
    private GameObject[] _hudCandies;

    private void Awake()
    {
        _tmpro = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        _hudCandies = GameObject.FindGameObjectsWithTag("HUDCandy");
    }

    private void Start()
    {
        _hudCandies = _hudCandies.OrderBy(e => e.name).ToArray();
        HideAllCandies();
        DisplayAppropriateCandy(_hudCandies[0]);
    }

    public void UpdateCandyCounter()
    {
        _tmpro.text = _playerScript.CandyAmount.ToString() + "/" + _playerScript.MaxCandyAmount.ToString();
    }

    public void UpdateCandyIcon()
    {
        HideAllCandies();

        if (_playerScript.CandyAmount >= 1 && _playerScript.CandyAmount <= 3)
            DisplayAppropriateCandy(_hudCandies[_playerScript.CandyAmount]);
        else
            DisplayAppropriateCandy(_hudCandies[0]);
    }

    private void HideAllCandies()
    {
        foreach (GameObject candy in _hudCandies)
            candy.SetActive(false);
    }

    private void DisplayAppropriateCandy(GameObject candy)
    {
        candy.SetActive(true);
    }
}
