using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovingState : PlayerBaseState
{
    private Vector3 origPosForNormalMovement, targetPosForNormalMovement; //For State 1
    private bool isMoving;
    PlayerStateManager player;
    public override void EnterState(PlayerStateManager player)
    {
        this.player = player;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if (!PlayerControllerGridMovement.instance.isMoving && !PlayerControllerGridMovement.instance.isChangingDirection)
        if (!isMoving)
        {
            if (PlayerControllerGridMovement.instance.GetInputDirectionsBufferStorageLength() > 0)
            {
                player.SwitchState(player.changingDirectionState);
                //Debug.Log("Switiching to changingDirectionState");
            }
            else
            {
                player.StartCoroutine(MovePlayer(PlayerControllerGridMovement.instance.currentDirection));
            }
        }
    }

    private IEnumerator MovePlayer(Vector3 direction) //State 1 //<=if there is any direction change then store the direction in buffer and let the coroutine complete then switch to direction change.
    {
        Debug.Log("MovePlayer===============");
        isMoving = true;
        float elapsedTime = 0;

        origPosForNormalMovement = player.transform.position;
        targetPosForNormalMovement = origPosForNormalMovement + direction * GridController.instance.cellSize;

        while (elapsedTime < PlayerControllerGridMovement.instance.timeToMove)
        {
            Debug.Log("MovePlayer1" + elapsedTime);
            player.transform.position = Vector3.Lerp(origPosForNormalMovement, targetPosForNormalMovement, (elapsedTime / PlayerControllerGridMovement.instance.timeToMove));
            CameraHandler.instance.SetCameraPosition(player.gameObject.transform.position + new Vector3(0, 0, -10));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosForNormalMovement;
        isMoving = false;
    }
}
