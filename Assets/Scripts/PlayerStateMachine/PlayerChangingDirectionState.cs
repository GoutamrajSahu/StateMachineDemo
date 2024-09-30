using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerChangingDirectionState : PlayerBaseState
{
    private Vector3 origPosForDirectionChange, targetPosForDirectionChange;
    private bool isChangingDirection;
    PlayerStateManager player;
    public override void EnterState(PlayerStateManager player)
    {
        this.player = player;
        if (PlayerControllerGridMovement.instance.GetInputDirectionsBufferStorageLength() > 0)
        {
            player.StartCoroutine(ChangeDirection(PlayerControllerGridMovement.instance.GetAndDeleteNextBufferDirection()));
        }
        else
        {
            //player.StartCoroutine(MovePlayer(currentDirection)); //<---Changing from State 2 to State 1.
            player.SwitchState(player.movingState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    private IEnumerator ChangeDirection(Vector3 direction) //State 2  //If we make a normal function instead of coroutine then we have to call this function in a update update function which may cause problem. 
    {
        Debug.Log("ChangeDirection==>");
        isChangingDirection = true;
        if (PlayerControllerGridMovement.instance.currentDirection * -1 != direction)
        {
            PlayerControllerGridMovement.instance.currentDirection = direction;
            float elapsedTime = 0;

            origPosForDirectionChange = player.transform.position;
            targetPosForDirectionChange = origPosForDirectionChange + direction * GridController.instance.cellSize;

            while (elapsedTime < PlayerControllerGridMovement.instance.changingDirectionTime)
            {
                player.transform.position = Vector3.Lerp(origPosForDirectionChange, targetPosForDirectionChange, (elapsedTime / PlayerControllerGridMovement.instance.changingDirectionTime));
                CameraHandler.instance.SetCameraPosition(player.gameObject.transform.position + new Vector3(0, 0, -10));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            player.transform.position = targetPosForDirectionChange;
            origPosForDirectionChange = Vector3.zero;
            targetPosForDirectionChange = Vector3.zero;
        }

        if (PlayerControllerGridMovement.instance.GetInputDirectionsBufferStorageLength() == 0)
        {
            isChangingDirection = false;
            player.SwitchState(player.movingState);
        }
        else
        {
            Vector3 tmpD = PlayerControllerGridMovement.instance.GetAndDeleteNextBufferDirection();
            Debug.Log("NextBufferDirection: " + tmpD);
            player.StartCoroutine(ChangeDirection(tmpD));
        }
    }
}
