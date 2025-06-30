using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MFarm.Dialogue
{
    //[RequireComponent(typeof(NPCMovement))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogueController : MonoBehaviour
    {
        //private NPCMovement npc => GetComponent<NPCMovement>();

        public UnityEvent onFinishEvent;
        public List<DialoguePiece> dialogueList = new List<DialoguePiece>();

        private Stack<DialoguePiece> dialogueStack;

        private bool canTalk;

        private bool isTalking;

        private void Awake()
        {
            FillDialogueStack();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {

                //�˴�������޸ģ����npc����ʱ��
                canTalk = true;
            }
        }

        private void Update()
        {
            if(canTalk & Input.GetKeyDown(KeyCode.Space) && !isTalking)
            {
                StartCoroutine(DailogueRoutine());
            }
        }

        private void FillDialogueStack()
        {
            dialogueStack = new Stack<DialoguePiece>();
            for (int i = dialogueList.Count - 1; i > -1; i--)
            {
                dialogueList[i].isDone = false;
                dialogueStack.Push(dialogueList[i]);
            }
        }

        private IEnumerator DailogueRoutine()
        {
            isTalking = true;
            if (dialogueStack.TryPop(out DialoguePiece result))
            {
                //����UI��ʾ�Ի�
                EventHandler.CallShowDialogueEvent(result);
                EventHandler.CallUpdateGameStateEvent(GameState.Pause);
                yield return new WaitUntil(() => result.isDone);
                isTalking = false;
            }
            else
            {
                EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
                EventHandler.CallShowDialogueEvent(null);
                FillDialogueStack();

                isTalking = false;
                if(onFinishEvent != null)
                {
                    onFinishEvent.Invoke();
                    canTalk = false;
                }
            }
        }
        }
}