using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerVolume : MonoBehaviour
{
	public static AudioMixerVolume Instance { get; private set; }

	public enum AudioMixerVolumeGroup
	{
		MasterVolume,
		MusicVolume,
		AmbienceVolume,
		EffectsVolume
    }

	[SerializeField] private AudioMixer _audioMixer;
	[field: SerializeField] public AudioMixerGroup MixerGroup { get; private set; }

	[SerializeField] private Sound _soundError;
	[SerializeField] private Sound _soundForward;
	[SerializeField] private Sound _soundBack;
	[SerializeField] private Sound _soundLimit;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);

		Debug.Log("TODO: Refactor SetMixerVolume() as best as possible.");
		Debug.Log("TODO: New input system.");
		Debug.Log("TODO: Instead of having to press the key for each volume point, allow the key to remain pressed.");
		Debug.Log("TODO: Instead of having all the sounds in the Audio Manager gameobject, put some inside of certain objects, " +
			"for example the candies, and have the sound be triggered from within the candy's script. This way we can have 3D sound.");
	}

	public int SetMixerVolume(int indexOption, int input)
	{
		string group = indexOption == 1 ? "MusicVolume" : indexOption == 2 ? "AmbienceVolume" : indexOption == 3 ? "EffectsVolume" : "MasterVolume";
		float currVolume = 0f;
		bool result = _audioMixer.GetFloat(group, out currVolume);
		int percentage = 0;

		// "currVolume": 0f (100%) / -80f (0%)
		// 1% is 0.8

		// Compensate for floating point imprecision
		if (currVolume > 0f) currVolume = 0f;
		else if (currVolume < -80f) currVolume = -80f;

		// The rounding is around "currVolume / 0.8f" for the same reason
		percentage = 100 + (int)Math.Round(currVolume / 0.8f, 0);

		if (input == 1)
		{
			if (percentage == 100)
			{
				_soundLimit.Play();
				return -1;
			}

			_soundForward.Play();
		}
		else if (input == -1)
		{
			if (percentage == 0)
			{
				_soundLimit.Play();
				return -1;
			}

			_soundBack.Play();
		}
		else
		{
			_soundError.Play();
			return -1;
		}

		// Update percentage
		percentage += input;

		// Update volume
		currVolume = (percentage - 100) * 0.8f;
		_audioMixer.SetFloat(group, currVolume);

		return percentage;
	}
}
