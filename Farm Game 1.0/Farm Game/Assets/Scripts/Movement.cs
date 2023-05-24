using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
    public float speed;

    public Animator animator;

    private Vector3 direction;

    bool isBoosting;
    
    public AudioClip groundWalkingClip;
    private AudioSource audioSource;
    
    public float fadeDuration; // Длительность затухания звука (в секундах)
    private float targetVolume; // Целевое значение громкости
    private float initialVolume; // Начальное значение громкости
    private bool isFading; // Флаг, указывающий на процесс затухания
    public float walkVolume = 0.1f;
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = groundWalkingClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = walkVolume;
    
        initialVolume = walkVolume;
        
    }
    
    private void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical);

        AnimateMovement(direction);

        if (direction.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (direction.magnitude == 0 && audioSource.isPlaying && !isFading)
        {
            StartCoroutine(FadeOut());
        }
    }
    
    IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = initialVolume;
        isFading = false;
    }

    private void FixedUpdate()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
