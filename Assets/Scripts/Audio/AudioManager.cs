using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	public enum AudioMixerVolumeGroup
    {
		MasterVolume,
		MusicVolume,
		AmbienceVolume,
		EffectsVolume
    }

	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private AudioMixerGroup _mixerGroup;
	[SerializeField] private SoundObject[] _sounds;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);

		foreach (SoundObject sound in _sounds)
			AddAudioSource(sound, gameObject);

		Debug.Log("TODO: Refactor SetMixerVolume() as best as possible.");
		Debug.Log("TODO: New input system.");
		Debug.Log("TODO: Instead of having to press the key for each volume point, allow the key to remain pressed.");
		Debug.Log("TODO: Instead of having all the sounds in the Audio Manager gameobject, put some inside of certain objects, " +
			"for example the candies, and have the sound be triggered from within the candy's script. This way we can have 3D sound.");
	}

    public void AddAudioSource(SoundObject s, GameObject g)
    {
		s.source = g.AddComponent<AudioSource>();
		s.source.clip = s.clip;
		s.source.outputAudioMixerGroup = s.mixerGroup != null ? s.mixerGroup : _mixerGroup;
		s.source.loop = s.loop;
		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
	}

	public void Play(string soundName)
	{
		SoundObject s = Array.Find(_sounds, e => e.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}

		s.source.Play();
	}

	public void Stop(string soundName)
	{
		SoundObject s = Array.Find(_sounds, e => e.soundName == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}

		s.source.Stop();
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
				Play("MenuLimit");
				return -1;
			}

			Play("MenuForward");
		}
		else if (input == -1)
		{
			if (percentage == 0)
			{
				Play("MenuLimit");
				return -1;
			}

			Play("MenuBack");
		}
		else
		{
			Play("Error");
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
