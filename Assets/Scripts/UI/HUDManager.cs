using System.Linq;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private TextMeshProUGUI _tmpro;
    private Character _playerScript;
    private GameObject[] _hudCandies;
    private Vector3 _hudCandyPosDisplayed, _hudCandyPosHidden;

    private void Awake()
    {
        _tmpro = GameObject.FindGameObjectWithTag("PlayerCandyCounter").GetComponent<TextMeshProUGUI>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        _hudCandies = GameObject.FindGameObjectsWithTag("HUDCandy");
    }

    private void Start()
    {
        _hudCandies = _hudCandies.OrderBy(e => e.name).ToArray();
        _hudCandyPosDisplayed = _hudCandies[0].transform.position;
        _hudCandyPosHidden = _hudCandies[1].transform.position;
    }

    public void UpdateCandyCounter()
    {
        _tmpro.text = _playerScript.CandyAmount.ToString() + "/" + _playerScript.MaxCandyAmount.ToString();
    }

    public void UpdateCandyIcon()
    {
        // Hide all the versions
        foreach (GameObject candy in _hudCandies)
            candy.transform.position = _hudCandyPosHidden;

        // Display the right one
        switch (_playerScript.CandyAmount)
        {
            case 1:
                _hudCandies[1].transform.position = _hudCandyPosDisplayed;
                break;
            case 2:
                _hudCandies[2].transform.position = _hudCandyPosDisplayed;
                break;
            case 3:
                _hudCandies[3].transform.position = _hudCandyPosDisplayed;
                break;
            default:
                _hudCandies[0].transform.position = _hudCandyPosDisplayed;
                break;
        }
    }
}
