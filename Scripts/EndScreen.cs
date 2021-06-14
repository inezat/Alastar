using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(end());
    }

    public IEnumerator end(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
