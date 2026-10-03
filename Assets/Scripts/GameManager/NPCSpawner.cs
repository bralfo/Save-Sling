using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameManager gameManager;

    private int npcsAtivos;

    void Start()
    {
        void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                SpawnNPC();
            }
        }
    }

    void Update()
    {
        if (npcsAtivos < 6 && !gameManager.partidaAcabou)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        GameObject novoNPC = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

        novoNPC.GetComponent<NPC>().SetSpawner(this);

        npcsAtivos++;
    }
    public void NPCSaiu()
    {
        npcsAtivos--;
    }
}