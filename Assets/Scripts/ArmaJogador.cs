using UnityEngine;

public class ArmaJogador : MonoBehaviour
{
    [SerializeField] private GameObject facaNaMao;
    [SerializeField] private float duracaoFaca = 5f;

    private float tempoRestante;

    public bool TemFaca { get; private set; }
    // de 1 (acabou de pegar) até 0 (vai sumir)
    public float FracaoRestante => TemFaca ? tempoRestante / duracaoFaca : 0f;

    void Awake()
    {
        RemoverFaca();
    }

    void Update()
    {
        if (!TemFaca)
        {
            return;
        }

        tempoRestante -= Time.deltaTime;

        if (tempoRestante <= 0f)
        {
            RemoverFaca();
        }
    }

    public bool TentarPegarFaca()
    {
        if (TemFaca)
        {
            return false;
        }

        TemFaca = true;
        tempoRestante = duracaoFaca;
        facaNaMao.SetActive(true);

        return true;
    }

    public void RemoverFaca()
    {
        TemFaca = false;
        tempoRestante = 0f;
        facaNaMao.SetActive(false);
    }
}