using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip enemyDeathSound;
    public AudioClip enemyExitSound;
    public AudioClip towerPlacedSound;
    public AudioClip moneyEarnedSound;
    public AudioClip beatMissedSound;

    public AudioSource audioSource;

	public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "enemyDeath":
                audioSource.clip = enemyDeathSound;
                break;
            case "enemyExitSound":
                audioSource.clip = enemyExitSound;
                break;
            case "towerPlacedSound":
                audioSource.clip = towerPlacedSound;
                break;
            case "moneyEarnedSound":
                audioSource.clip = moneyEarnedSound;
                break;
            case "beatMissedSound":
                audioSource.clip = beatMissedSound;
                break;
            default:
                audioSource.clip = null;
                break;
        }

        if (audioSource.clip)
            audioSource.Play();
    }
}
