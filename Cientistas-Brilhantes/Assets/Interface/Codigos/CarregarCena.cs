using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingUIObject;

    public GameObject elementoASumir;

    public void TrocarElementosUI()
    {
        elementoASumir.SetActive(false);
    }

    public void MostrarImagemECarregarCena()
    {
        if (loadingUIObject != null)
        {
            TrocarElementosUI();
            loadingUIObject.SetActive(true); // Torna a imagem de UI vis�vel
        }

        // Carrega a nova cena ap�s um pequeno atraso
        Invoke("Museu", 1.0f);

        CarregarNovaCena();
    }

    private void CarregarNovaCena()
    {
        SceneManager.LoadScene("Museu"); // Substitua "NovaCena" pelo nome da sua cena
    }
}
