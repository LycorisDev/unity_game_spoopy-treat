// Video: https://www.youtube.com/watch?v=6OT43pvUyfY
// Download Link: https://downloads.brackeys.com/wp-content/FilesForDownload/AudioManager.zip

using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public AudioMixer audioMixer;
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;

	void Awake()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.mixerGroup != null ? s.mixerGroup : mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}

		s.source.Stop();
	}

	public int SetMixerVolume(int indexOption, int input)
	{
		string group = indexOption == 1 ? "MusicVolume" : indexOption == 2 ? "AmbienceVolume" : indexOption == 3 ? "EffectsVolume" : "MasterVolume";
		float currVolume = 0f;
		bool result = audioMixer.GetFloat(group, out currVolume);
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
		audioMixer.SetFloat(group, currVolume);

		return percentage;
	}
}
