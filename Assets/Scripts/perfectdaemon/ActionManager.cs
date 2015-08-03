using UnityEngine;
using System.Collections.Generic;

namespace perfectdaemon.Utils
{
    /// <summary>
    /// SimpleAction represents one single-step action
    /// </summary>
    public delegate void SimpleAction();

    /// <summary>
    /// ContinuousAction represents action that performs for some period
    /// </summary>
    /// <param name="dt">Deltatime</param>
    public delegate void ContinuousAction(float dt);   

    /// <summary>
    /// Base class that stores info about action performing
    /// </summary>
    internal abstract class ActionInfo
    {
        /// <summary>
        /// Is action done
        /// </summary>
        public bool Done = false;

        /// <summary>
        /// Delay before action performing
        /// </summary>
        public float StartAfter = 0.0f;

        /// <summary>
        /// Virtual method for performing action due to 
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Perform(float dt);
    }

    /// <summary>
    /// Stores information about SimpleAction performing
    /// </summary>
    internal class SimpleActionInfo : ActionInfo
    {
        /// <summary>
        /// Action that should be performed
        /// </summary>
        public SimpleAction Action = null;        

        /// <summary>
        /// Performes action if it's not done and if it's time to do that
        /// </summary>
        /// <param name="dt">DeltaTime</param>
        public override void Perform(float dt)
        {
            if (Done)
                return;

            if (StartAfter > 0)
                StartAfter -= dt;
            else
            {
                Action();
                Done = true;
            }
        }

        /// <summary>
        /// Instantiates object
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <param name="startAfter">Dealy before action performing</param>
        public SimpleActionInfo(SimpleAction action, float startAfter = 0.0f)
        {
            this.Action = action;
            this.StartAfter = startAfter;
        }
    }

    /// <summary>
    /// Stores information about ContinuousAction performing
    /// </summary>
    internal class ContinuousActionInfo : ActionInfo
    {
        /// <summary>
        /// Action that should be performed
        /// </summary>
        public ContinuousAction Action = null;

        /// <summary>
        /// Period of time that action will be performing
        /// </summary>
        public float Period = 0.0f;

        /// <summary>
        /// Performes action
        /// </summary>
        /// <param name="dt">DeltaTime</param>
        public override void Perform(float dt)
        {
            if (Done)
                return;

            if (StartAfter > 0)
                StartAfter -= dt;
            else if (Period > 0)
            {
                Action(dt);
                Period -= dt;
            }
            else
                Done = true;
        }

        /// <summary>
        /// Instantiates object
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <param name="period">Duration of action performing</param>
        /// <param name="startAfter">Delay before action performing</param>
        public ContinuousActionInfo(ContinuousAction action, float period, float startAfter = 0.0f)
        {
            this.Action = action;
            this.StartAfter = startAfter;
            this.Period = period;
        }
    }

    /// <summary>
    /// Manages independent and queued actions
    /// </summary>
    public class ActionManager : MonoBehaviour
    {
        private List<SimpleActionInfo> simpleActions = new List<SimpleActionInfo>();
        private List<ContinuousActionInfo> continuousActions = new List<ContinuousActionInfo>();
        private Queue<ActionInfo> queueActions = new Queue<ActionInfo>();

        private bool IsActionDone(ActionInfo info)
        {
            return info.Done;
        }

        public void AddIndependent(SimpleAction action, float startAfter = 0.0f)
        {
            simpleActions.Add(new SimpleActionInfo(action, startAfter));
        }

        public void AddIndependent(ContinuousAction action, float period, float startAfter = 0.0f)
        {
            continuousActions.Add(new ContinuousActionInfo(action, period, startAfter));
        }

        public void AddToQueue(SimpleAction action, float startAfter = 0.0f)
        {
            queueActions.Enqueue(new SimpleActionInfo(action, startAfter));
        }

        public void AddToQueue(ContinuousAction action, float period, float startAfter = 0.0f)
        {
            queueActions.Enqueue(new ContinuousActionInfo(action, period, startAfter));
        }   

        void Update()
        {
            foreach (var action in simpleActions)
                action.Perform(Time.deltaTime);

            foreach (var action in continuousActions)
                action.Perform(Time.deltaTime);

            while (queueActions.Count > 0)
            {
                var action = queueActions.Peek();
                if (action.Done)
                {
                    queueActions.Dequeue();
                    continue;
                }
                else
                    action.Perform(Time.deltaTime);
            }

            simpleActions.RemoveAll(IsActionDone);
            continuousActions.RemoveAll(IsActionDone);
        }
    }
}

