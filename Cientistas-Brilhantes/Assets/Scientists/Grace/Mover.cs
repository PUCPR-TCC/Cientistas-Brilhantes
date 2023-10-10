using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidade de movimento
    private Vector3 originalPosition; // Posi��o original do objeto

    private bool isAtOriginalPosition = true; // Verifica se o objeto est� na posi��o original

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAtOriginalPosition)
            {
                TeleportToNewPosition(); // Chama a fun��o para teleportar para uma nova posi��o
                isAtOriginalPosition = false;
            }
            else
            {
                ReturnToOriginalPosition(); // Chama a fun��o para retornar � posi��o original
                isAtOriginalPosition = true;
            }
        }
    }

    void TeleportToNewPosition()
    {
        // Gere uma nova posi��o aleat�ria (por exemplo, dentro de um raio)
        Vector3 newPosition = originalPosition + new Vector3(Random.Range(100f, 110f), 0f, Random.Range(100f, 110f));

        // Teleporta o objeto para a nova posi��o
        transform.position = newPosition;
    }

    void ReturnToOriginalPosition()
    {
        // Retorna o objeto para a posi��o original
        transform.position = originalPosition;
    }
}
