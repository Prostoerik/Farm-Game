using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicClips;
    public AudioSource audioSource;
    public float crossfadeDuration = 5f;
    public float timeBetweenSongs = 5f;

    private int currentClipIndex;
    private float nextSongTime;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
        PlayRandomSong();
    }

    private void Update()
    {
        if (!audioSource.isPlaying && Time.time >= nextSongTime)
        {
            PlayRandomSong();
        }
    }

    private void PlayRandomSong()
    {
        if (musicClips.Length > 0)
        {
            currentClipIndex = Random.Range(0, musicClips.Length);
            audioSource.clip = musicClips[currentClipIndex];
            audioSource.Play();

            nextSongTime = Time.time + audioSource.clip.length + timeBetweenSongs;
        }
    }
}