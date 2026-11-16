using UnityEngine;

public class GeradorFacas : MonoBehaviour
{
    [SerializeField] private GerenciadorPartida partida;
    [SerializeField] private FacaColetavel prefabFaca;

    [SerializeField] private float intervaloMinimo = 3f;
    [SerializeField] private float intervaloMaximo = 10f;
    [SerializeField] private int maximoFacas = 3;

    [SerializeField] private float alcanceHorizontal = 10f;
    [SerializeField] private float alcanceVertical = 5f;

    private float tempoRestante;

    void Start()
    {
        SortearIntervalo();
    }

    void Update()
    {
        if (partida.Encerrada)
        {
            return;
        }

        tempoRestante -= Time.deltaTime;

        if (tempoRestante > 0f)
        {
            return;
        }

        if (transform.childCount < maximoFacas)
        {
            CriarFaca();
        }

        SortearIntervalo();
    }

    private void SortearIntervalo()
    {
        tempoRestante = Random.Range(
            intervaloMinimo,
            intervaloMaximo);
    }

    private void CriarFaca()
    {
        float x = Random.Range(
            -alcanceHorizontal,
            alcanceHorizontal);

        float y = Random.Range(
            -alcanceVertical,
            alcanceVertical);

        Vector3 posicao = new Vector3(x, y, 0f);

        Instantiate(
            prefabFaca,
            posicao,
            Quaternion.identity,
            transform);
    }
}