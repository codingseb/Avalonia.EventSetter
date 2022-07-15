using Avalonia.Markup.Xaml;
using System;
using System.Reflection;

namespace Avalonia.Styling
{
    /// <summary>
    /// A EventSetter for a <see cref="Style"/>.
    /// </summary>
    /// <remarks>
    /// A <see cref="EventSetter"/> is used to subscribe an event on a <see cref="StyledElement"/> with an handler in the code behind of the current xaml root object.
    /// </remarks>
    public class EventSetter : ISetter
    {
        private EventInfo eventInfo;
        private MethodInfo methodInfo;
        private Delegate handlerDelegate;
        private object handlerContainer;

        /// <summary>
        /// The event name on which to subscribe
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// The Handler of the event to connect
        /// </summary>
        public string Handler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EventSetter()
        {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <param name="handlerContainer"></param>
        public EventSetter(string eventName, string handler, object handlerContainer)
        {
            Event = eventName;
            Handler = handler;
            this.handlerContainer = handlerContainer;
        }

        /// <inheritdoc/>
        public ISetterInstance Instance(IStyleable target)
        {
            target = target ?? throw new ArgumentNullException(nameof(target));

            if (Event is null)
            {
                throw new InvalidOperationException($"{nameof(EventSetter)}.{nameof(Event)} must be set.");
            }

            if (Handler is null)
            {
                throw new InvalidOperationException($"{nameof(EventSetter)}.{nameof(Handler)} must be set.");
            }

            if (handlerContainer is null)
            {
                throw new InvalidOperationException($"{nameof(EventSetter)}.{nameof(handlerContainer)} is null.");
            }

            eventInfo ??= target.GetType().GetEvent(Event);

            if (eventInfo is null)
            {
                throw new InvalidOperationException($"Can not find an event named [{Event}] on the styled Element {target}");
            }

            methodInfo ??= handlerContainer.GetType().GetMethod(Handler, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            if(methodInfo is null)
            {
                throw new InvalidOperationException($"Can not find a method named [{Handler}] on the object {handlerContainer}");
            }

            handlerDelegate ??= Delegate.CreateDelegate(eventInfo.EventHandlerType, handlerContainer, methodInfo);

            return new EventSetterInstance(target, eventInfo, handlerDelegate);
        }

        /// <summary>
        /// To set by default the HandlerContainer on the xaml context rootObject
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider is IRootObjectProvider rootObjectProvider && handlerContainer == null)
            {
                handlerContainer = rootObjectProvider.RootObject;
            }

            return this;
        }
    }
}
