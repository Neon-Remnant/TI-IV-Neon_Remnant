using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosSonoros : MonoBehaviour
{
    public AudioSource somDoTiro, somDoEscudo;

    public void TocarSomDoEscudo()
    {
        if (somDoEscudo != null && !somDoEscudo.isPlaying) 
        {
            somDoEscudo.Play();
        }
    }

    public void PararSomDoEscudo()
    {
        if (somDoEscudo != null && somDoEscudo.isPlaying)
        {
            somDoEscudo.Stop();
        }
    }

    public void TocarSomDoTiro()
    {
        if (somDoTiro != null && !somDoTiro.isPlaying) 
        {
            somDoTiro.Play();
        }
    }

    public void PararSomDoTiro()
    {
        if (somDoTiro != null && somDoTiro.isPlaying)
        {
            somDoTiro.Stop();
        }
    }
}
