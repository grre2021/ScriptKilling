using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CommandDemo : NetworkBehaviour
{


    float time = 2f;

    [SyncVar (hook =nameof(SetColor))]
    Color colorNeededToSynchronize=Color.red;
  
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //StartCoroutine(__RandomizeColor());
    }

    private void SetColor(Color oldcolor,Color newcolor)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = newcolor;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 2f;
            Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
            colorNeededToSynchronize = color;
        }
    }
    /*
    private IEnumerator __RandomizeColor()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
           
                Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);
                colorNeededToSynchronize = color;
                // SetColor(colorNeededToSynchronize);
               // RpcSetRandomColor(color);
                yield return wait;           
        }
        
        
    }
    */


    void ColorChange(Color oldcolor, Color newcolor)
    {
        Debug.Log("colorchange" + newcolor);
        
       // RpcSetRandomColor(newcolor);
    }
    /*
    [Command]
    private void CmdChangeColor()
    {
        Debug.Log("CmdChangeColor called");

        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f, 1f, 1f);

        //SetColor(color);
        RpcSetRandomColor(color);

    }
    */
    [ClientRpc]
    private void RpcSetRandomColor(Color color)
    {
       
    }
    
}
