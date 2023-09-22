using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidade de movimento
    private Vector3 originalPosition; // Posi��o original do objeto
    private bool isMoving = false; // Verifica se o objeto est� se movendo

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleMovement();
        }

        if (isMoving)
        {
            // Movimenta o objeto para a direita
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void ToggleMovement()
    {
        isMoving = !isMoving;

        // Se o objeto estiver se movendo, define a posi��o original como a posi��o atual
        if (isMoving)
        {
            originalPosition = transform.position;
        }
        else
        {
            // Se o objeto n�o estiver se movendo, retorna � posi��o original
            transform.position = originalPosition;
        }
    }
}
