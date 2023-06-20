using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerVolume : MonoBehaviour
{
	public static AudioMixerVolume Instance { get; private set; }

	public enum VolumeGroup
	{
		[Description("MasterVolume")] MasterVolume,
		[Description("MusicVolume")] MusicVolume,
		[Description("AmbienceVolume")] AmbienceVolume,
		[Description("EffectsVolume")] EffectsVolume
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

		Debug.Log("TODO: New input system.");
		Debug.Log("TODO: Instead of having to press the key for each volume point, allow the key to remain pressed.");
	}

	public int SetMixerVolume(VolumeGroup group, int input)
	{
		float currVolume;
		int percentage;

		/*
			-80f is 0% of volume, and 0f is 100%
			1% is 0.8f
		*/
		_audioMixer.GetFloat(group.ToString(), out currVolume);

		// Compensate for floating point imprecision
		currVolume = Mathf.Clamp(currVolume, -80f, 0f);
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
		_audioMixer.SetFloat(group.ToString(), currVolume);

		return percentage;
	}
}
