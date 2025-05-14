using System;
using RedRats.Core;

namespace RedRats.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public static class ActionHistorySystem
    {
        public static event Action OnUpdateUndoHistory;
        public static event Action OnUpdateRedoHistory;
        
        private static readonly ObservableStack<IAction> undoHistory = new();
        private static readonly ObservableStack<IAction> redoHistory = new();

        private static IAction lastAction;
        private static GroupActionBase currentGroup;
        private static bool canCreateGroups = true;
        private static bool ignoreConstructs = false;

        static ActionHistorySystem()
        {
            undoHistory.OnChange += () => OnUpdateUndoHistory?.Invoke();
            redoHistory.OnChange += () => OnUpdateRedoHistory?.Invoke();
        }

        /// <summary>
        /// Adds an action to the history and executes it.
        /// </summary>
        /// <param name="action">The action to add & execute</param>
        /// <param name="blockGrouping">Excerpt this action from grouping with similar ones.</param>
        public static void AddAndExecute(IAction action, bool blockGrouping = false)
        {
            if (action == null) return;
            if (action.NothingChanged()) return;
            
            action.Execute();
            DecideGroupingResponseFor(action, blockGrouping);
            redoHistory.Clear();
            lastAction = action;
        }

        public static void Undo()
        {
            //If there is a group open, add it to undo
            if (currentGroup != null) AddCurrentGroupToUndo();
            if (undoHistory.Count == 0) return;

            IAction newestAction = undoHistory.Pop();
            redoHistory.Push(newestAction);
            newestAction.Undo();
        }

        public static void Redo()
        {
            if (redoHistory.Count == 0) return;

            IAction newestAction = redoHistory.Pop();
            undoHistory.Push(newestAction);
            newestAction.Execute();
        }

        public static void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            lastAction = null;
            currentGroup = null;
        }

        #region Group Processing
        /// <summary>
        /// Enables/disables grouping of actions.
        /// </summary>
        /// <param name="value">TRUE to enable.</param>
        public static void EnableGroupingBehaviour(bool value)
        {
            canCreateGroups = value;
            if (!value) EndCurrentGroup();
        }
        
        /// <summary>
        /// Forces the system to end current group and prepare for a new one.
        /// </summary>
        public static void StartNewGroup() => StartNewGroup(false);
        public static void StartNewGroup(bool allowDifferentConstructs)
        {
            if (allowDifferentConstructs) ignoreConstructs = true;
            if (canCreateGroups) EndCurrentGroup();
        }

        /// <summary>
        /// Forces the system to add the current group to undo.
        /// </summary>
        public static void EndCurrentGroup()
        {
            ignoreConstructs = false;
            AddCurrentGroupToUndo();
        }

        private static void DecideGroupingResponseFor(IAction action, bool blockGrouping = false)
        {
            // If in group mode
            if (!blockGrouping && canCreateGroups)
            {
                //Init group if it doesn't exist
                if (currentGroup == null)
                {
                    currentGroup = (ignoreConstructs) ? new MixedGroupAction() : new GroupAction();
                    currentGroup.AddAction(action);
                    return;
                }
                
                //Add to group if action is on the same construct
                if (ignoreConstructs || action.AffectedConstruct == lastAction?.AffectedConstruct)
                {
                    currentGroup.AddAction(action);
                    return;
                }
                
                //End grouping if action is different
                AddCurrentGroupToUndo();
            }

            undoHistory.Push(action);
        }

        private static void AddCurrentGroupToUndo()
        {
            if (currentGroup == null) return;
            if (!currentGroup.NothingChanged()) undoHistory.Push(currentGroup);
            currentGroup = null;
        }
        
        #endregion
        
        public static int UndoCount => undoHistory.Count;
        public static int RedoCount => redoHistory.Count;
        
        public static GroupActionBase CurrentGroup { get => currentGroup; }
    }
}