using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GerenciadorPartida : MonoBehaviour
{
    [SerializeField] private GameObject painelResultado;
    [SerializeField] private TMP_Text textoVencedor;
    private bool partidaEncerrada;
    public bool Encerrada => partidaEncerrada;

    void Awake()
    {
        partidaEncerrada = false;
        painelResultado.SetActive(false);
    }
    public void ResolverContato(
        CombateJogador jogadorA,
        CombateJogador jogadorB)
    {
        if (partidaEncerrada)
        {
            return;
        }

        bool aTemFaca = jogadorA.Arma.TemFaca;
        bool bTemFaca = jogadorB.Arma.TemFaca;

        if (aTemFaca && bTemFaca)
        {
            jogadorA.Arma.RemoverFaca();
            jogadorB.Arma.RemoverFaca();

            Debug.Log("As facas se anularam!");
            return;
        }

        if (aTemFaca)
        {
            EncerrarPartida(jogadorA, jogadorB);
        }
        else if (bTemFaca)
        {
            EncerrarPartida(jogadorB, jogadorA);
        }
    }

    private void EncerrarPartida(CombateJogador vencedor, CombateJogador perdedor)
    {
        partidaEncerrada = true;

        vencedor.Parar();
        perdedor.Parar();

        perdedor.gameObject.SetActive(false);

        textoVencedor.text = vencedor.gameObject.name + " venceu!";
        painelResultado.SetActive(true);
    }
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene("Arena");
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}