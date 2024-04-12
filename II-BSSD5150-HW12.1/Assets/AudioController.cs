using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public Text lossMessageText;
    public Button restartButton;
    private GameObject playerInstance;
    public Transform spawnPoint;

    [SerializeField] AudioMixerSnapshot exploring;
    [SerializeField] AudioMixerSnapshot battling;
    [SerializeField] AudioMixerSnapshot loss;
    [SerializeField] AudioSource stingSound;
    [SerializeField] AudioSource fallSound;

    private float transitionTime = 1f;

    private void Start()
    {
        // Hide the loss message and restart button at the start
        lossMessageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void ShowLossMessage()
    {
        // Display the loss message
        lossMessageText.gameObject.SetActive(true);
        lossMessageText.text = "YOU LOSE";
    }

    public void ShowRestartButton()
    {
        // Display the restart button
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        // Respawn the player
        RespawnPlayer();

        // Hide the loss message and restart button
        lossMessageText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void RespawnPlayer()
    {
        transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BattleZone")
        {
            // Transition from whatever is playing to battling in transitionTime seconds
            battling.TransitionTo(transitionTime);
            stingSound.Play();
        }

        if (collision.tag == "SpawnZone")
        {
            // Transition from whatever is playing to exploring in transitionTime seconds
            exploring.TransitionTo(transitionTime);
        }

        if (collision.tag == "Fall")
        {
            loss.TransitionTo(transitionTime);
            fallSound.Play();
            ShowLossMessage();
            ShowRestartButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BattleZone")
        {
            // Transition from whatever is playing to exploring in transitionTime seconds
            exploring.TransitionTo(transitionTime);
        }
        if (collision.tag == "Fall")
        {
            exploring.TransitionTo(transitionTime);
        }

    }
}