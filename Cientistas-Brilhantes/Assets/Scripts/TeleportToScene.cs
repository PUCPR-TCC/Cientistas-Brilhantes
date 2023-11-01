using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingUIObject; // Refer�ncia ao objeto da imagem que voc� deseja mostrar.
    [SerializeField]
    private string sceneToLoad;    // Nome da cena para carregar (se necess�rio).

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o jogador colidiu com um objeto marcado como "LoadSceneTrigger"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + gameObject.name);

            // Verifica se h� um objeto de imagem para mostrar.
            if (loadingUIObject != null)
            {
                loadingUIObject.SetActive(true); // Ativa o objeto da imagem.
            }

            // Se necess�rio, carrega a cena especificada.
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                LoadScene();
            }
        }
        else
        {
            Debug.Log("Player collided with " + gameObject.name + " but it is not tagged as Player");
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
