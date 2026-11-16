using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource fonteSom;
    [SerializeField] private AudioClip somJogar;
    [SerializeField] private AudioClip somSair;
    [SerializeField] private AudioSource musica;

    private bool acaoEmAndamento; // evita clicar duas vezes enquanto o som toca

    public void Jogar()
    {
        if (acaoEmAndamento) return;
        StartCoroutine(JogarComSom());
    }

    public void Sair()
    {
        if (acaoEmAndamento) return;
        StartCoroutine(SairComSom());
    }

    private IEnumerator JogarComSom()
    {
        acaoEmAndamento = true;
        fonteSom.PlayOneShot(somJogar);

        float duracao = somJogar.length;
        float volumeInicial = musica.volume;
        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            // vai de volumeInicial até 0 conforme o tempo passa
            musica.volume = Mathf.Lerp(volumeInicial, 0f, tempo / duracao);
            yield return null; // espera só 1 frame e repete
        }

        SceneManager.LoadScene("Arena");
    }

    private IEnumerator SairComSom()
    {
        acaoEmAndamento = true;
        fonteSom.PlayOneShot(somSair);

        yield return new WaitForSeconds(somSair.length);

        Debug.Log("Botao Sair acionado.");
        Application.Quit();
    }
}