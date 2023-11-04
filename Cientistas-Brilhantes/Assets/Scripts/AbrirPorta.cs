using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPorta : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    public float maxDistance = 2f; // Defina a dist�ncia m�xima para a intera��o.

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da anima��o de abertura
    [SerializeField] private string closeAnimationName; // = "Fechar Porta 2"; // Nome da anima��o de fechamento

    private bool isInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play(openAnimationName, 0, 0.0f);
                gameObject.SetActive(false);
            }
            
        }
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!isInteracted && distanceToPlayer <= maxDistance)
        {
            myDoor.Play(openAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = true;
        }
        else if (isInteracted)
        {
            myDoor.Play(closeAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = false;
        }
    }
}