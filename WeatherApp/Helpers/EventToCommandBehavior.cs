using System;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace WeatherApp.Helpers
{
    public class EventToCommandBehavior : Behavior<View>
    {
        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            SubscribeEvent(EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            UnsubscribeEvent(EventName);
        }

        private void SubscribeEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return;

            EventInfo eventInfo = this.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't subscribe to the '{eventName}' event.");

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod(nameof(OnEvent));
            Delegate eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(this, eventHandler);
        }

        private void UnsubscribeEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return;

            EventInfo eventInfo = this.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't unsubscribe from the '{eventName}' event.");

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod(nameof(OnEvent));
            Delegate eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.RemoveEventHandler(this, eventHandler);
        }

        private void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
                return;

            object resolvedParameter = CommandParameter ?? eventArgs;
            if (resolvedParameter == null && eventArgs != null)
            {
                var propertyInfo = eventArgs.GetType().GetProperty("Parameter");
                if (propertyInfo != null)
                    resolvedParameter = propertyInfo.GetValue(eventArgs);
            }

            if (Command.CanExecute(resolvedParameter))
                Command.Execute(resolvedParameter);
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            behavior.UnsubscribeEvent((string)oldValue);
            behavior.SubscribeEvent((string)newValue);
        }


    }
}
