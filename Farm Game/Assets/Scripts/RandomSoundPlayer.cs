using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource audioSource;
    public float minDelay = 5f;
    public float maxDelay = 10f;

    private float nextPlayTime;

    void Start()
    {
        // Задаем начальное время для воспроизведения первого звука
        SetNextPlayTime();
        audioSource = gameObject.AddComponent<AudioSource>();
        // Установка громкости на 0.5
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        // Проверяем, прошло ли достаточно времени для воспроизведения следующего звука
        if (Time.time >= nextPlayTime)
        {
            PlayRandomSound();
            SetNextPlayTime();
        }
    }

    void SetNextPlayTime()
    {
        // Генерируем случайное время задержки до следующего воспроизведения
        nextPlayTime = Time.time + Random.Range(minDelay, maxDelay);
    }

    void PlayRandomSound()
    {
        // Проверяем наличие звуков
        if (sounds.Length > 0)
        {
            // Генерируем случайный индекс звука
            int randomIndex = Random.Range(0, sounds.Length);
            // Получаем случайный звук по индексу
            AudioClip randomSound = sounds[randomIndex];
            // Устанавливаем случайный звук для AudioSource и воспроизводим его
            audioSource.clip = randomSound;
            audioSource.Play();
        }
    }
}