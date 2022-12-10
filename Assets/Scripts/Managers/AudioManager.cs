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

	public int SetMixerVolume(int index_option, int input)
	{
		string group = index_option == 1 ? "MusicVolume" : index_option == 2 ? "AmbienceVolume" : index_option == 3 ? "EffectsVolume" : "MasterVolume";
		float curr_volume = 0f;
		bool result = audioMixer.GetFloat(group, out curr_volume);
		int percentage = 0;

		// "curr_volume": 0f (100%) / -80f (0%)
		// 1% is 0.8

		// Compensate for floating point imprecision
		if (curr_volume > 0f) curr_volume = 0f;
		else if (curr_volume < -80f) curr_volume = -80f;

		// The rounding is around "curr_volume / 0.8f" for the same reason
		percentage = 100 + (int)Math.Round(curr_volume / 0.8f, 0);

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
		curr_volume = (percentage - 100) * 0.8f;
		audioMixer.SetFloat(group, curr_volume);

		return percentage;
	}
}
