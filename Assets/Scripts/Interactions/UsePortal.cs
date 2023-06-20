using System.Collections;
using UnityEngine;

public class UsePortal : MonoBehaviour
{
    [SerializeField] private bool isPortalFed = false;
    [SerializeField] private int requiredCandyAmount = 3;
    [SerializeField] private Sound _soundPortalOpening;

    private Character _characterScript;
    private MenuManager _menuScript;
    private Light _lightComponent;
    private Color _defaultColor, _dullColor, _candyColor;

    private void Awake()
    {
        _characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        _menuScript = FindObjectOfType<MenuManager>();
        _lightComponent = gameObject.GetComponentInChildren(typeof(Light)) as Light;

        _defaultColor = _lightComponent.color;
        _dullColor = Color.gray;
        switch (requiredCandyAmount)
        {
            case 1:
                _candyColor = Color.red;
                break;
            case 2:
                _candyColor = Color.blue;
                break;
            case 3:
                _candyColor = Color.yellow;
                break;
            default:
                _candyColor = Color.black;
                break;
        }
    }

    private void Update()
    {
        float time;

        if (!isPortalFed)
        {
            time = Mathf.PingPong(Time.time, 1f) / 1f;
            _lightComponent.color = Color.Lerp(_dullColor, _candyColor, time);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPortalFed)
            {
                if (_characterScript.CandyAmount >= requiredCandyAmount)
                {
                    _characterScript.ModifyCandyAmount(-requiredCandyAmount);
                    isPortalFed = true;
                }
                else
                    yield break;
            }

            _soundPortalOpening.Play();
            yield return new WaitForSecondsRealtime(1f);

            // End the game now for the time being
            _menuScript.DisableFirstMainMenuOption();
            _menuScript.OpenMainMenu();
        }
    }
}
