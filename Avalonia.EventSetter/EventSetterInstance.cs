using System;
using System.Reflection;

namespace Avalonia.Styling
{
    internal class EventSetterInstance : ISetterInstance
    {
        private State _state;

        /// <summary>
        /// The event on which to subscribe
        /// </summary>
        public EventInfo Event { get; }

        /// <summary>
        /// The Source of the event on which to subscribe
        /// </summary>
        public object Source { get; private set; }

        /// <summary>
        /// The Handler of the event to connect on subscribe.
        /// </summary>
        public Delegate Handler { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target">The target on which to subscribe</param>
        /// <param name="eventInfo">The event name on which to subscribe</param>
        public EventSetterInstance(object source, EventInfo eventInfo, Delegate handler)
        {
            Source = source;
            Event = eventInfo;
            Handler = handler;
        }

        private bool IsActive => _state == State.Active;

        /// <inheritdoc/>
        public void Activate()
        {
            if(!IsActive)
            {
                _state = State.Active;
                Subcribe();
            }
        }

        /// <inheritdoc/>
        public void Deactivate()
        {
            if (IsActive)
            {
                _state = State.Inactive;
                Unsubscribe();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_state == State.Disposed)
                return;

            if (IsActive)
                Unsubscribe();

            _state = State.Disposed;

            Source = null;
            Handler = null;
        }

        /// <inheritdoc/>
        public void Start(bool hasActivator)
        {
            if(!hasActivator)
            {
                Subcribe();
            }
        }

        /// <summary>
        /// Connect the event <see cref="EventName"/> from <see cref="Target"/>
        /// </summary>
        public void Subcribe()
        {
            Event?.AddEventHandler(Source, Handler);
        }

        /// <summary>
        /// Disconnect the event
        /// </summary>
        public void Unsubscribe()
        {
            Event?.RemoveEventHandler(Source, Handler);
        }

        private enum State
        {
            Inactive,
            Active,
            Disposed,
        }
    }
}
