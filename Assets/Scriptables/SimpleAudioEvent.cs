using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent {
	public AudioClip[] clips;
	public RangedFloat volume;

	AudioClip selectClip;
	float selectVolume;

	[MinMaxRange(0,2)]
	public RangedFloat pitch;


	public override void Play(AudioSource source)
	{
		if (clips.Length == 0)return;

		selectClip = clips [Random.Range (0, clips.Length)];
		selectVolume = Random.Range (volume.minValue, volume.maxValue);
		source.pitch = Random.Range (pitch.minValue, pitch.maxValue);
		source.PlayOneShot (selectClip, selectVolume);

	}
}