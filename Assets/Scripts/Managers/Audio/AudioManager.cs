using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // assign via Inspector
    [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>(); // add your songs here
    [SerializeField] private float fadeDuration = 2f; // fade in/out duration in seconds

    private List<AudioClip> shuffledPlaylist = new List<AudioClip>();
    private int currentSongIndex = 0;

    void Start()
    {
        if (musicTracks.Count > 0)
        {
            ShufflePlaylist();
            StartCoroutine(PlayShuffledMusic());
        }
        else
        {
            Debug.LogWarning("No music tracks assigned to MusicManager.");
        }
    }

    void ShufflePlaylist()
    {
        // Create a new list from the original musicTracks list
        shuffledPlaylist = new List<AudioClip>(musicTracks);
        // Shuffle using Fisher-Yates algorithm
        for (int i = 0; i < shuffledPlaylist.Count; i++)
        {
            AudioClip temp = shuffledPlaylist[i];
            int randomIndex = Random.Range(i, shuffledPlaylist.Count);
            shuffledPlaylist[i] = shuffledPlaylist[randomIndex];
            shuffledPlaylist[randomIndex] = temp;
        }
        currentSongIndex = 0;
    }

    IEnumerator PlayShuffledMusic()
    {
        while (true)
        {
            // Get the next song from the shuffled playlist
            AudioClip currentSong = shuffledPlaylist[currentSongIndex];

            // Fade in the song
            yield return StartCoroutine(FadeIn(currentSong));

            // Wait for the song to finish, then wait an additional random delay (20 to 60 seconds)
            yield return new WaitForSeconds(currentSong.length + Random.Range(20f, 60f));

            // Fade out the current song
            yield return StartCoroutine(FadeOut());

            // Move to the next song and reshuffle if needed
            currentSongIndex++;
            if (currentSongIndex >= shuffledPlaylist.Count)
            {
                ShufflePlaylist();
            }
        }
    }

    IEnumerator FadeIn(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.volume = 0f;
        audioSource.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            // Lerp volume from 0 to 1 over fadeDuration
            audioSource.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;
    }

    IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            // Lerp volume from startVolume down to 0 over fadeDuration
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
