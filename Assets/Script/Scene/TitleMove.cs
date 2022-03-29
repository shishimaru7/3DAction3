using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMove : MonoBehaviour
{
  //  UI　ボタンによる遷移
 //   public付ける事、無いと付けられない　参照0でも動く

    public void MoveMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
