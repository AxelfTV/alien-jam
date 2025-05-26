using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menus : MonoBehaviour
{
    [SerializeField] TMP_Text wavesCompleted;
    private void Start()
    {
        if(wavesCompleted != null)
        {
            if (CombatManager.wave < CombatManager.fWave) wavesCompleted.text = "You got to wave " + CombatManager.wave.ToString() + "!";
            else wavesCompleted.text = "You got to the end of the Jam version!";
        }
    }
    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
