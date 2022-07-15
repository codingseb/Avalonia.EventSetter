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
    public class EventSetter : AvaloniaObject, ISetter
    {
        private EventInfo eventInfo;
        private MethodInfo methodInfo;
        Delegate handlerDelegate;

        /// <summary>
        /// The event name on which to subscribe
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// The Handler of the event to connect
        /// </summary>
        public string Handler { get; set; }

        /// <summary>
        /// The DirectProperty for the object that implement the handler
        /// </summary>
        public static readonly DirectProperty<EventSetter, object> HandlerContainerProperty = AvaloniaProperty.RegisterDirect<EventSetter, object>(
            nameof(HandlerContainer),
            o => o.HandlerContainer,
            (o, value) => o.HandlerContainer = value,
            defaultBindingMode: Data.BindingMode.OneTime);

        /// <summary>
        /// The object that implement the handler
        /// </summary>
        public object HandlerContainer { get; set; }

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
            HandlerContainer = handlerContainer;
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

            if (HandlerContainer is null)
            {
                throw new InvalidOperationException($"{nameof(EventSetter)}.{nameof(HandlerContainer)} is null.");
            }

            eventInfo ??= target.GetType().GetEvent(Event);

            if (eventInfo is null)
            {
                throw new InvalidOperationException($"Can not find an event named [{Event}] on the styled Element {target}");
            }

            methodInfo ??= HandlerContainer.GetType().GetMethod(Handler, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            if(methodInfo is null)
            {
                throw new InvalidOperationException($"Can not find a method named [{Handler}] on the object {HandlerContainer}");
            }

            handlerDelegate ??= Delegate.CreateDelegate(eventInfo.EventHandlerType, HandlerContainer, methodInfo);

            return new EventSetterInstance(target, eventInfo, handlerDelegate);
        }

        /// <summary>
        /// To set by default the HandlerContainer on the xaml context rootObject
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider is IRootObjectProvider rootObjectProvider && HandlerContainer == null)
            {
                HandlerContainer = rootObjectProvider.RootObject;
            }

            return this;
        }
    }
}
