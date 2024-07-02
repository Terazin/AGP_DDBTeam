using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] float distance = 50.0f;//���o�\�ȋ���
    Transform transforms;//�|�����G�̕ۑ�
    [SerializeField] GameObject player;
    GameObject game;

    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTransform()
    {
        return game;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //�J�����̈ʒu����Ƃ΂�
            var rayStartPosition = this.transform.position;

            //�J�����������Ă�����ɂƂ΂�
            var rayDirection = this.transform.forward.normalized;

            //Hit�����I�u�W�F�N�g�i�[�p
            RaycastHit raycastHit;

            Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);

            if (Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance))
            {
                // Log��Hit�����I�u�W�F�N�g�����o��
                //Debug.Log(context.phase);
                Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);

                if (raycastHit.collider.tag == "Enemy")
                {
                    Debug.Log("EnemyHit");
                    transforms = raycastHit.transform;
                    game=raycastHit.collider.gameObject;
                }
            }
        }
    }

    //public void ChangeEnemy(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed && transforms != null)
    //    {
    //        Debug.Log("Change");

    //        //�e��Enemy��
    //        transform.parent.gameObject.tag = "Enemy";
    //        objParent.transform.rotation = Quaternion.identity;

    //        //�e�̕t���ւ�
    //        player.gameObject.transform.parent = transforms;
    //        objParent = player.transform.parent.gameObject;

    //        //�e��Player��
    //        player.transform.parent.gameObject.tag = "Player";

    //        //_cam.Follow = raycastHit.transform;
    //        player.transform.position = transforms.position;
    //        //this.transform.localPosition = new Vector3(0f, 1.5f, -3f);
    //        //this.transform.localRotation = Quaternion.identity;

    //        transforms = null;
    //    }
    //}
}