using UnityEngine;

public class QuitButton : MonoBehaviour
{
    /// <summary>
    /// Appelé par le bouton pour quitter le jeu.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu...");

#if UNITY_EDITOR
        // Si on est dans l'éditeur Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si on est dans le build
        Application.Quit();
#endif
    }
}