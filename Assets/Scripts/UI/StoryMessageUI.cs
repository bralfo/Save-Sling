using TMPro;
using UnityEngine;
using System.Collections;

public class StoryMessageUI : MonoBehaviour
{
    [SerializeField] public TMP_Text StoryMessage;
    private Coroutine hideCoroutine;
    public void ShowMessage(string message)
    {
        if (StoryMessage == null)
        {
           
            return;
        }

        
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        
        StoryMessage.text = message;
        hideCoroutine = StartCoroutine(HideMessageAfterDelay(2f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        
        StoryMessage.text = "";
        hideCoroutine = null;
    }
}

