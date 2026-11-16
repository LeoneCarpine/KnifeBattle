using UnityEngine;

public class FacaColetavel : MonoBehaviour
{
    private bool coletada;

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (coletada)
        {
            return;
        }

        ArmaJogador jogador = outro.GetComponent<ArmaJogador>();

        if (jogador == null)
        {
            return;
        }

        if (jogador.TentarPegarFaca())
        {
            coletada = true;
            Destroy(gameObject);
        }
    }
}