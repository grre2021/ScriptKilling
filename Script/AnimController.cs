using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimController : MonoBehaviour
{
    
    public  Animator animator;
    public NetworkAnimator networkAnimator;
    public GameObject character;
    public Rigidbody rb;
    
    private Animator animPlayer;
    private GameObject Player;
    private Vector3 sourcePosition;
    private float dis;
    
    private bool isMove;


    //��¼�������ƶ���Ϣ���͸������������ɷ��������������ͻ���
    //���ܲ���Ҫͬ�����������
    //ʧ��
    //�ж�����λ�ÿ��ƶ���
    private void Start()
    {
        Debug.Log("start");
        animPlayer = GetComponent<Animator>();
        Player = this.gameObject;
        sourcePosition = Player.transform.position;
    }
    //public override void OnStartLocalPlayer()
   // {

        //animator = character.GetComponent<Animator>();
        // networkAnimator = GetComponent<NetworkAnimator>();         
        // rb = character.GetComponent<Rigidbody>();
       

   // }

    private void Update()
    {

        //SynAnim();
        //if(isLocalPlayer)
        if (Player.activeSelf)
        {
            AnimContro();
        }
              
    }


    void AnimContro()
    {

         dis=Vector3.Distance(sourcePosition,Player.transform.position);

        if (!isMove)
        {
            if (dis > 0.03f)
            {
                animPlayer.SetBool("run", true);
                sourcePosition = Player.transform.position;
                isMove = true;
                StartCoroutine(nameof(IEAnimContro));

            }
        }
       
    }

    IEnumerator IEAnimContro()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            if(dis<0.03f)
            {
                animPlayer.SetBool("run", false);
                isMove = false;
                StopCoroutine(nameof(IEAnimContro));
                
            }
            else
            {
                sourcePosition = Player.transform.position;
            }
            yield return new WaitForSeconds(0.3f);
        }
        
    }

    
    void SynAnim()
    {
        if (character == null || networkAnimator == null) return;        
            if (character.activeSelf)
            {
                networkAnimator.enabled = true;
            }
            else
            {
                networkAnimator.enabled = false;
            }
        
    }

   // [Command]
    private void Test()
    {
        Debug.Log("command");
        if (isMove)
        {
            if (animator.gameObject.activeSelf)
                animator.SetBool("run", true);

        }
        else
        {
            if (animator.gameObject.activeSelf)
                animator.SetBool("run", false);
        }
        test();
    }

    //[ClientRpc(includeOwner = false)]
    void test()
    {
       
            
            if (isMove)
            {
                if (animator.gameObject.activeSelf)
                    animator.SetBool("run", true);
            }
            else
            {
                if (animator.gameObject.activeSelf)
                    animator.SetBool("run", false);
            }
        
    }
 

  
}
