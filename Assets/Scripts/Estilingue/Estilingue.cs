using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Estilingue : MonoBehaviour
{
    
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float maxPullDistance = 5f;

    
    [SerializeField] private Transform launchPoint;
    [SerializeField] private SalvageItem Item;

    private SalvageItem currentItem;
    private Rigidbody2D currentRigidbody;

    private Camera mainCamera;
    private bool isAiming;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        SetItem(Item);
    }

    private void Update()
    {
        if (currentItem == null) // verifica se tem item no estilingue
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartAiming();
        }

        if (isAiming)
        {
            UpdateAim();

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Launch();
            }
        }
    }

    public void SetItem(SalvageItem item)
    {
        if (currentItem != null)
            return;

        currentItem = item; 

        currentRigidbody = item.GetComponent<Rigidbody2D>(); // procura o Rigidbody2D do item

        currentRigidbody.bodyType = RigidbodyType2D.Kinematic; // essa parte fala: se o Item estiver no estilingue, ele não vai ser afetado pela gravidade, nem por colisões, ele vai ficar parado no estilingue. Lembre-se Dynamic é o tipo de corpo que é afetado pela física, Kinematic é o tipo de corpo que não é afetado pela física, e Static é o tipo de corpo que não se move e não é afetado pela física.
        currentRigidbody.linearVelocity = Vector2.zero; // aqui ele fala: se o Item estiver no estilingue, ele não vai ter velocidade, ele vai ficar parado no estilingue.
        currentRigidbody.angularVelocity = 0f; // zera a rotação do item, para que ele não fique girando quando estiver no estilingue.

        item.transform.position = launchPoint.position; // no momento brendon, colocamos o item no estilingue, mas ele não vai ficar no estilingue, ele vai ficar na posição do launchPoint, que é o ponto de lançamento do estilingue.
    }

    private void StartAiming()
    {
        isAiming = true;
    }

    private void UpdateAim()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue(); // pega a posição do mouse na tela, X e Y. Z aqui é a profundidade, que é a distância da câmera até o objeto, nesse caso, o estilingue.

        mouseScreenPosition.z =
            -mainCamera.transform.position.z;  // aqui noix tiramos a profundidade da câmera, para que o mouse fique na mesma profundidade do estilingue

        Vector3 mouseWorldPosition =
            mainCamera.ScreenToWorldPoint(mouseScreenPosition); //  coloca o mouse no mundo(jogo)

        mouseWorldPosition.z = 0f;

        Vector2 direction =
            (Vector2)(mouseWorldPosition - launchPoint.position);

        direction = Vector2.ClampMagnitude(
            direction,
            maxPullDistance
        );

        currentItem.transform.position =
            launchPoint.position + (Vector3)direction; // aqui é " de fato o estilingue puxando o item", ele pega a posição do launchPoint e adiciona a direção do mouse, que é a direção que o jogador está puxando o estilingue, e limita a distância máxima que o jogador pode puxar o estilingue.
    }

    private void Launch() 
    {
        isAiming = false;

        Vector2 launchDirection =
            (Vector2)(launchPoint.position - currentItem.transform.position); // aqui é a direção do estilingue, que é a direção que o jogador está puxando, ai vai pro outro lado né( meio confuso, mas é assim que funciona: meio que jogando o Item de volta para o launchPoint, ai entra o add force etc etc.

        currentRigidbody.bodyType = RigidbodyType2D.Dynamic; //  tranforma o item para Dynamic (perguntinha: PQ TRANSFORMA EM DYNAMIC? R:          )

        currentRigidbody.AddForce(
            launchDirection.normalized * launchForce,
            ForceMode2D.Impulse
        ); // AQUI ELE APLICA A FORÇA,, com normalized, ele aplica força de forma controlada, ainda n ta completamente feito, mas vai servir para calcular a distancia que o player puxa o estilingue e aplicar força equivalente

        currentItem = null;
        currentRigidbody = null; // 
    }  
}
