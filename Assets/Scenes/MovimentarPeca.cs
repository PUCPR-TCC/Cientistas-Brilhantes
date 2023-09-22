using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPeca : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject objectDragToPos;

    // O objeto vai para  posi��o se estiver perto o suficiente
    public float DropDistance;

    // Verifica se est� no lugar certo
    public bool isLocked;

    // Adicione um contador para rastrear as pe�as no lugar certo
    private static int piecesInPlace = 0;
    private static int totalPieces; // Defina este valor no Inspector

    Vector2 objectInitPos;

    // Start is called before the first frame update
    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
        totalPieces++; // Incrementa o n�mero total de pe�as
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            // o mouse movimenta o objeto
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);

        if (Distance < DropDistance)
        {
            // est� na posi��o correta
            isLocked = true;
            Debug.Log("Correct Move");

            // o objeto que estamos arrastando est� na posi��o correta
            objectToDrag.transform.position = objectDragToPos.transform.position;

            // Incrementa o contador de pe�as no lugar certo
            piecesInPlace++;

            // Verifica se todas as pe�as est�o no lugar certo
            if (piecesInPlace == totalPieces)
            {
                Debug.Log("Todas as pe�as est�o no lugar certo!");
            }
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}
