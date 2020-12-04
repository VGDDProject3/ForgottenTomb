using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinController : MonoBehaviour
{
    #region Coin Variables
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private GameObject emptyCoin;

    [SerializeField]
    private Transform parentCanvas;

    [SerializeField]
    private Vector3 beginPos;

    private int numCoins = 0;
    private float dist = 150;
    #endregion
    static GameManager gameManager;

    #region Sound
    [SerializeField]
    private AudioSource fx;

    [SerializeField]
    private AudioClip coinFx;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null ) {
            gameManager = (GameManager) Object.FindObjectOfType(typeof(GameManager));
        }
        StartCoroutine("SpinCoins");
    }

    IEnumerator SpinCoins() 
    {
        Debug.Log("SpinCoins called");
        yield return new WaitForSeconds(1); //Wait for level complete letters
        Vector3 position;
        int i = 0;
        for (; i < gameManager.NumCoins(); i += 1) {
            position = new Vector3(beginPos.x + numCoins * dist, beginPos.y, 0f);
            Debug.Log(position);
            Instantiate(coin, position, Quaternion.identity, parentCanvas);
            fx.PlayOneShot(coinFx);
            yield return new WaitForSeconds(1);
            numCoins += 1;
        }
        for (; i < 3; i += 1) {
            position = new Vector3(beginPos.x + numCoins * dist, beginPos.y, 0f);
            Instantiate(emptyCoin, position, Quaternion.identity, parentCanvas);
            yield return new WaitForSeconds(0.25f);
            numCoins += 1;
        }
    }
}
