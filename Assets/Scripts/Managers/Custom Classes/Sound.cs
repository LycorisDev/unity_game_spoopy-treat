using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;
	[HideInInspector] public AudioSource source;
	public AudioMixerGroup mixerGroup;

	public bool loop = false;

	[Range(0f, 1f)] public float volume = 0.75f;
	[Range(0f, 1f)] public float volumeVariance = 0.1f;

	[Range(0.1f, 3f)] public float pitch = 1f;
	[Range(0f, 1f)] public float pitchVariance = 0.1f;
}
