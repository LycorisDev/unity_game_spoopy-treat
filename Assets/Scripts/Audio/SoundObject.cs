using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound_Object", menuName = "ScriptableObjects/SoundObject", order = 1)]
public class SoundObject : ScriptableObject
{
	public string soundName;
	public AudioClip clip;
	public AudioMixerGroup mixerGroup;
	public bool loop = false;
	public bool is3d = false;
	[Range(0f, 1f)] public float volume = 0.75f;
	[Range(0.1f, 3f)] public float pitch = 1f;
	[HideInInspector] public AudioSource source;
}
