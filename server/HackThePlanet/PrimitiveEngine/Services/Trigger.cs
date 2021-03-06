﻿namespace PrimitiveEngine
{
    using System;
	using System.Collections.Generic;


    /// <summary>
    /// Class Trigger.
    /// </summary>
    public class Trigger
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public Trigger(params string[] propertyName)
        {
            this.IsFired = false;
            this.WorldPropertiesMonitored = new List<string>();
            foreach (string item in propertyName)
            {
                this.WorldPropertiesMonitored.Add(item);
            }
        }
        #endregion


        /// <summary>
        /// Occurs when [on fire].
        /// </summary>
        public event Action<Trigger> OnFire;


        #region Properties
        /// <summary>
        /// Gets the black board.
        /// </summary>
        /// <value>The black board.</value>
        public BlackBoard BlackBoard { get; internal set; }


        /// <summary>
        /// Gets the state of the trigger.
        /// </summary>
        /// <value>The state of the trigger.</value>
        public TriggerStateType TriggerStateType { get; private set; }


        /// <summary>
        /// Gets or sets the entityWorld properties monitored.
        /// </summary>
        /// <value>The entityWorld properties monitored.</value>
        public List<string> WorldPropertiesMonitored { get; protected set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is fired.
        /// </summary>
        /// <value><see langword="true" /> if this instance is fired; otherwise, <see langword="false" />.</value>
        internal bool IsFired { get; set; }
        #endregion


        /// <summary>
        /// Removes the this trigger.
        /// </summary>
        public void RemoveThisTrigger()
        {
            this.BlackBoard.RemoveTrigger(this);
        }


        /// <summary>
        /// Called if is fired.
        /// </summary>
        /// <param name="triggerStateType">State of the trigger.</param>
        protected virtual void CalledOnFire(TriggerStateType triggerStateType)
        {
        }


        /// <summary>
        /// Checks the condition to fire.
        /// </summary>
        /// <returns><see langword="true" /> if XXXX, <see langword="false" /> otherwise</returns>
        protected virtual bool CheckConditionToFire()
        {
            return true;
        }


        /// <summary>
        /// Fires the specified trigger state.
        /// </summary>
        /// <param name="triggerStateType">State of the trigger.</param>
        internal void Fire(TriggerStateType triggerStateType)
        {
            this.IsFired = true;
            this.TriggerStateType = triggerStateType;
            if (CheckConditionToFire())
            {
                CalledOnFire(triggerStateType);
                if (OnFire != null)
                {
                    OnFire(this);
                }
            }

            this.IsFired = false;
        }
    }
}