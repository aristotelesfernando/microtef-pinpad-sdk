using CrossPlatformBase;
using PinPadSDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PinPadSDK.Tools {
    /// <summary>
    /// Class used to provide feedback on locking state of a object and allow other thread to request access over a locked object
    /// </summary>
    public class LockController : IDisposable {
        #region Static Members

        /// <summary>
        /// Buffers all LockControllers and their locked objects so we can request access later
        /// </summary>
        private static Dictionary<object, IList<LockController>> LockedObjectCollection = new Dictionary<object, IList<LockController>>();

        private static Dictionary<object, List<Thread>> ReservedObjectDictionary = new Dictionary<object, List<Thread>>();

        /// <summary>
        /// Amount of Lock Controller instantiated for hashing purposes
        /// </summary>
        private static int LockControllersCount = 0;

        #endregion

        #region Static Methods

        /// <summary>
        ///  Request access of a object to the thread currently locking it
        /// </summary>
        /// <param name="objectToLock">The locked object we need access</param>
        /// <param name="requestingObject">The object that requires access to the locked object</param>
        /// <returns>true only if the object is locked with LockController and OnAccessRequested is watched</returns>
        public static bool RequestAccess(object lockedObject, object requestingObject) {
            lock (LockedObjectCollection) {
                if (!LockedObjectCollection.ContainsKey(lockedObject))
                    return false;

                bool Success = true;

                IList<LockController> lockControllerCollection = LockedObjectCollection[lockedObject];
                foreach (LockController lockController in lockControllerCollection) {
                    if (lockController.OnAccessRequested != null) {
                        lockController.OnAccessRequested(lockController, new AccessRequestedEventArgs(lockedObject, requestingObject));
                    }
                    else
                        Success = false;
                }
                return Success;
            }
        }

        public static void ReserveAccess(object obj) {
            lock (ReservedObjectDictionary) {
                if (IsObjectReserved(obj) == true) {
                    if (ReservedObjectDictionary[obj].Contains(Thread.CurrentThread) == false) {
                        ReservedObjectDictionary[obj].Add(Thread.CurrentThread);
                    }
                }
                else {
                    ReservedObjectDictionary.Add(obj, new List<Thread>());
                    ReservedObjectDictionary[obj].Add(Thread.CurrentThread);
                }
            }
        }

        public static bool ReleaseAccess(object obj) {
            lock (ReservedObjectDictionary) {
                if (IsObjectReservedByCurrentThread(obj) == false) {
                    return false;
                }
                else {
                    ReservedObjectDictionary[obj].Remove(Thread.CurrentThread);
                    if (ReservedObjectDictionary[obj].Count == 0) {
                        ReservedObjectDictionary.Remove(obj);
                    }
                    return true;
                }
            }
        }

        public static bool IsObjectReserved(object obj) {
            lock (ReservedObjectDictionary) {
                return ReservedObjectDictionary.ContainsKey(obj);
            }
        }

        public static bool IsObjectReservedByCurrentThread(object obj) {
            lock (ReservedObjectDictionary) {
                if (IsObjectReserved(obj) == false) {
                    return false;
                }
                else {
                    if (ReservedObjectDictionary[obj].Contains(Thread.CurrentThread) == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        public static bool IsObjectReservedByDifferentThread(object obj) {
            lock (ReservedObjectDictionary) {
                if (IsObjectReserved(obj) == false) {
                    return false;
                }
                else {
                    if (ReservedObjectDictionary[obj].Contains(Thread.CurrentThread) == false) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Called when successfully obtained the lock of the object
        /// </summary>
        public event EventHandler<EventArgs> OnEnter;

        /// <summary>
        /// Called when successfully obtained the lock of the object
        /// </summary>
        public event EventHandler<EventArgs> OnTryEnterFail;

        /// <summary>
        /// Called when successfully obtained the lock of the object
        /// </summary>
        public event EventHandler<EventArgs> OnExit;

        /// <summary>
        /// Called when a thread requests access over the locked object
        /// </summary>
        public event EventHandler<AccessRequestedEventArgs> OnAccessRequested;

        #endregion

        #region Members

        /// <summary>
        /// Flag: Is the object locked?
        /// </summary>
        bool isLocked = false;

        /// <summary>
        /// Id for hashing
        /// </summary>
        int LockControllerId;

        /// <summary>
        /// The object we have locked
        /// </summary>
        object lockedObject = null;

        #endregion

        #region Properties

        public object LockedObject {
            get {
                return lockedObject;
            }
        }

        #endregion

        #region Methods

        public LockController(object objectToLock) {
            LockControllerId = LockControllersCount;
            LockControllersCount++;

            lockedObject = objectToLock;
        }

        /// <summary>
        /// Locks the object and calls the events accordingly
        /// </summary>
        public void LockObject() {
            if (!isLocked) {
                while (IsObjectReservedByDifferentThread(lockedObject)) {
                    CrossPlatformController.CrossPlatformThread.Sleep(0);
                }

                if (!Monitor.TryEnter(lockedObject)) {
                    if (OnTryEnterFail != null)
                        OnTryEnterFail(lockedObject, EventArgs.Empty);

                    Monitor.Enter(lockedObject);
                }

                lock (LockedObjectCollection) {
                    if (!LockedObjectCollection.ContainsKey(lockedObject))
                        LockedObjectCollection.Add(lockedObject, new List<LockController>());

                    LockedObjectCollection[lockedObject].Add(this);
                }

                isLocked = true;

                if (OnEnter != null)
                    OnEnter(lockedObject, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Attempts to lock the object
        /// </summary>
        /// <returns>True if the object is locked</returns>
        public bool TryLockObject() {
            if (!isLocked) {
                if (IsObjectReservedByDifferentThread(lockedObject)) {
                    return false;
                }

                if (!Monitor.TryEnter(lockedObject)) {
                    if (OnTryEnterFail != null)
                        OnTryEnterFail(lockedObject, EventArgs.Empty);

                    return false;
                }

                lock (LockedObjectCollection) {
                    if (!LockedObjectCollection.ContainsKey(lockedObject))
                        LockedObjectCollection.Add(lockedObject, new List<LockController>());

                    LockedObjectCollection[lockedObject].Add(this);
                }

                isLocked = true;

                if (OnEnter != null)
                    OnEnter(lockedObject, EventArgs.Empty);
            }

            return true;
        }


        /// <summary>
        /// Unlocks the object and calls the events accordingly
        /// </summary>
        public void UnlockObject() {
            if (isLocked) //Check if we have the lock of the object
            {
                // We did have the lock, unlock and call the event
                Monitor.Exit(lockedObject);
                lock (LockedObjectCollection) {
                    LockedObjectCollection[lockedObject].Remove(this);
                    if (LockedObjectCollection[lockedObject].Count == 0)
                        LockedObjectCollection.Remove(lockedObject);
                }

                isLocked = false;

                if (OnExit != null)
                    OnExit(lockedObject, EventArgs.Empty);
            }
        }

        public void Dispose() {
            UnlockObject();
        }

        public override int GetHashCode() {
            return LockControllerId;
        }

        #endregion
    }
}
