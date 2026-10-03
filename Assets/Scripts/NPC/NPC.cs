using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private int npcEscolhido;
    private string nomeAnimacao;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcEscolhido = Random.Range(0, 16);
        nomeAnimacao = "NPC " + (char)('A' + npcEscolhido);
        animator.Play(nomeAnimacao);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
