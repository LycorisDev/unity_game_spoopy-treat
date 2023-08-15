using System.Collections;
using UnityEngine;

public class UsePortal : MonoBehaviour
{
    [SerializeField] private Character _playerScript;
    [SerializeField] private bool _isPortalFed = false;
    [SerializeField] private int _requiredCandyAmount = 3;
    [SerializeField] private Sound _soundPortalIdleNoise;
    [SerializeField] private Sound _soundPortalCrossing;

    private bool _isIdleNoisePlaying = false;
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
        switch (_requiredCandyAmount)
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

        if (!_isPortalFed)
        {
            time = Mathf.PingPong(Time.time, 1f) / 1f;
            _lightComponent.color = Color.Lerp(_dullColor, _candyColor, time);
        }

        CanPortalBeFed(_playerScript.CandyAmount);
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isPortalFed)
            {
                if (_characterScript.CandyAmount >= _requiredCandyAmount)
                {
                    _characterScript.ModifyCandyAmount(-_requiredCandyAmount);
                    _isPortalFed = true;
                }
                else
                    yield break;
            }

            _soundPortalCrossing.Play();
            yield return new WaitForSecondsRealtime(1f);

            // End the game
            _menuScript.DisableFirstMainMenuOption();
            _menuScript.OpenMainMenu();
        }
    }

    private void CanPortalBeFed(int playerCandyAmount)
    {
        if (!_isIdleNoisePlaying)
        {
            if (!_isPortalFed && playerCandyAmount == _requiredCandyAmount)
            {
                _isIdleNoisePlaying = true;
                _soundPortalIdleNoise.Play();
            }
        }
        else if (_isPortalFed || playerCandyAmount < _requiredCandyAmount)
        {
            _isIdleNoisePlaying = false;
            _soundPortalIdleNoise.Stop();
        }
    }
}
