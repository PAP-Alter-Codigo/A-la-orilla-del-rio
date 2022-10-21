// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using System.Collections;
using UnityEngine.AI;


namespace Fungus
{
    /// <summary>
    /// The block will execute when the user clicks or taps on the clickable object.
    /// </summary>
    [EventHandlerInfo("Sprite",
                    "Object Clicked",
                    "The block will execute when the user clicks or taps on the clickable object.")]
    [AddComponentMenu("")]
    public class ObjectClicked : EventHandler
    {
        public class ObjectClickedEvent
        {
            public Clickable2D ClickableObject;
            public ObjectClickedEvent(Clickable2D clickableObject)
            {
                ClickableObject = clickableObject;
            }
        }

        [Tooltip("Object that the user can click or tap on")]
        [SerializeField] protected Clickable2D clickableObject;

        [Tooltip("Wait for a number of frames before executing the block.")]
        [SerializeField] protected int waitFrames = 1;

        protected EventDispatcher eventDispatcher;

        //reference Target script
        public Target target;

        private NavMeshAgent agent;

        public Verb verb;

        void Start()
        {
            target = FindObjectOfType<Target>();
            agent = GetComponent<NavMeshAgent>();
            verb = FindObjectOfType<Verb>();
            //  agent.updateRotation = false;
            // agent.updateUpAxis = false;

        }
        protected virtual void OnEnable()
        {
            eventDispatcher = FungusManager.Instance.EventDispatcher;

            eventDispatcher.AddListener<ObjectClickedEvent>(OnObjectClickedEvent);
        }

        protected virtual void OnDisable()
        {
            eventDispatcher.RemoveListener<ObjectClickedEvent>(OnObjectClickedEvent);

            eventDispatcher = null;
        }

        void OnObjectClickedEvent(ObjectClickedEvent evt)
        {
            OnObjectClicked(evt.ClickableObject);
        }

        /// <summary>
        /// Executing a block on the same frame that the object is clicked can cause
        /// input problems (e.g. auto completing Say Dialog text). A single frame delay 
        /// fixes the problem.
        /// </summary>
        protected virtual IEnumerator DoExecuteBlock(int numFrames)
        {
            //edited by Gabriel
            while (Vector3.Distance(clickableObject.transform.position, target.transform.position) > clickableObject.ActivateDistance)
            {
                // yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(.1f);

            }
            //edited by Gabriel
            if (Vector3.Distance(clickableObject.transform.position, target.transform.position) <= clickableObject.ActivateDistance)
            {
                //edited by Gabriel
                target.inDialogue = true;
                target.cutsceneInProgress = true;
                target.SetDestinationTarget();
                target.followSpot = target.transform.position;
                verb.gameObject.SetActive(false);
                //change verb to Use
                verb.UpdateVerbTextBox(clickableObject.clickableName);
                //target.animator.SetFloat("distance",0);


                if (numFrames == 0)
                {
                    ExecuteBlock();
                    yield break;
                }

                int count = Mathf.Max(waitFrames, 1);
                while (count > 0)
                {
                    count--;
                    yield return new WaitForEndOfFrame();
                }

                ExecuteBlock();

            }

        }

        #region Public members

        /// <summary>
        /// Called by the Clickable2D object when it is clicked.
        /// </summary>
        public virtual void OnObjectClicked(Clickable2D clickableObject)
        {
            if (clickableObject == this.clickableObject)
            {
                StartCoroutine(DoExecuteBlock(waitFrames));
            }
        }

        public override string GetSummary()
        {
            if (clickableObject != null)
            {
                return clickableObject.name;
            }

            return "None";
        }

        #endregion
    }
}
