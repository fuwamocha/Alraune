using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToInstructions : MonoBehaviour
{

    public void GotoInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}

