using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MelonLoader;
using Noesis;
using NoesisApp;

// ReSharper disable UnusedMember.Global

namespace Ritterschlag
{
    public static class Utils
    {
        public static IEnumerable<T> GetFieldsRecursively<T>(object obj)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields()
                         .Where(info => typeof(T).IsAssignableFrom(info.FieldType)))
            {
                T t1 = (T)fieldInfo.GetValue(obj);
                yield return t1;
                foreach (T t in GetFieldsRecursively<T>(t1))
                {
                    yield return t;
                }
            }
        }

        public static IEnumerable<T> GetFieldsRecursivelyAll<T>(object obj)
        {
            foreach (FieldInfo info in obj.GetType().GetFields())
            {
                if (info.FieldType == typeof(T) || info.FieldType.IsSubclassOf(typeof(T)))
                {
                    T type = (T)info.GetValue(obj);
                    yield return type;
                }
                else
                {
                    foreach (T type in GetFieldsRecursivelyAll<T>(info.GetValue(obj)))
                    {
                        yield return type;
                    }
                }
            }
        }

        public static void ObjectDumpMelonlogger(object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                MelonLogger.Msg($"{descriptor.Name}={descriptor.GetValue(obj)}");
                // Console.WriteLine("{0}={1}", name, value);
            }
        }

        public static void ObjectDumpFile(object obj, string filename)
        {
            using StringWriter sr = new();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                sr.WriteLine($"{descriptor.Name}={descriptor.GetValue(obj)}");
            }

            File.WriteAllText(filename, sr.ToString());
        }

        public static bool NoesisIterateElement(DependencyObject element, Func<DependencyObject, int, bool> action)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            if (childrenCount == 0)
            {
                return action(element, childrenCount);
            }

            if (!action(element, childrenCount))
            {
                bool flag = false;
                for (int i = 0; i < childrenCount; i++)
                {
                    if (NoesisIterateElement(VisualTreeHelper.GetChild(element, i), action))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }

            return true;
        }

        public static void NoesisIterateElement(DependencyObject element, Action<DependencyObject, int, int> action,
            int depth = 0)
        {
            // int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            IEnumerable enumerable = LogicalTreeHelper.GetChildren(element).Cast<object>().ToList();
            int childrenCount = enumerable.Cast<object>().Count();
            action(element, childrenCount, depth);
            foreach (object child in enumerable)
            {
                try
                {
                    NoesisIterateElement((DependencyObject)child, action, depth + 1);
                }
                catch (InvalidCastException)
                {
                }
            }
            // if (childrenCount == 0)
            // {
            //     return;
            // }
            //
            // action(element, childrenCount, depth);
            // for (int i = 0; i < childrenCount; i++)
            // {
            //     DependencyObject child = VisualTreeHelper.GetChild(element, i);
            //     iterateNoesisElement(child, action, depth + 1);
            // }
        }

        public static StringBuilder NoesisIterateElement(DependencyObject element)
        {
            StringBuilder stringBuilder = new();
            NoesisIterateElement(element,
                (o, childrenCount, depth) =>
                {
                    string prefix = string.Concat(Enumerable.Repeat(" ", depth * 2));
                    stringBuilder.Append($"{prefix}");
                    stringBuilder.Append($"{o.GetType().FullName}");

                    // List<string> Properties = new(new[] { "Name", "ItemsSource" });
                    stringBuilder.Append($"{{{Environment.NewLine}{prefix}");
                    stringBuilder.AppendJoin($"{Environment.NewLine}{prefix}",
                        o.GetType().GetProperties().Select(info => $"{info.Name}=\"{info.GetValue(o)}\"").ToList());
                    // foreach (PropertyInfo propertyInfo in o.GetType().GetProperties())
                    // {
                    //     stringBuilder.Append($"{propertyInfo.Name}=\"{propertyInfo.GetValue(o)}\"");
                    // }
                    // foreach (string name in Properties)
                    // {
                    //     PropertyInfo propertyInfo = o.GetType().GetProperty(name);
                    //     if (propertyInfo != null) stringBuilder.Append($"{name}=\"{propertyInfo.GetValue(o)}\"");
                    // }

                    stringBuilder.Append($"{Environment.NewLine}{prefix}}}");

                    stringBuilder.Append(
                        $"|{childrenCount}{Environment.NewLine}");
                });
            return stringBuilder;
        }

        public static IEnumerable<FrameworkElement> NoesisEnumerateChildrenFrameworkElements(DependencyObject element)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(element))
            {
                FrameworkElement frameworkElement = child as FrameworkElement;
                if (frameworkElement != null)
                {
                    yield return frameworkElement;
                } 
                DependencyObject dependencyObject = child as DependencyObject;
                if (dependencyObject == null) continue;
                foreach (FrameworkElement sub in NoesisEnumerateChildrenFrameworkElements(dependencyObject))
                {
                    yield return sub; 
                }
            }
        }

        public static void NoesisDumpElementMelonlogger(DependencyObject element)
        {
            MelonLogger.Msg(NoesisIterateElement(element).ToString());
        }

        public static void NoesisDumpElementToFile(DependencyObject element, string filename)
        {
            File.WriteAllText(filename, NoesisIterateElement(element).ToString());
        }

        public static void NoesisResourcesMelonlogger(ResourceDictionary resourceDictionary)
        {
            MelonLogger.Msg(resourceDictionary);
            foreach (var resourcesKey in resourceDictionary.Keys)
            {
                MelonLogger.Msg($"{resourcesKey} {resourceDictionary[resourcesKey]}");
            }
        }

        public static class NoesisBehaviorsFixes
        {
            /// <summary>
            /// Fixes detaching behaviours when manipulating UI items.
            /// </summary>
            /// <remarks>
            /// When a visual is removed from visual tree and this visual has an attached behaviours in it
            /// (or its children) these behaviours gets a notification that they are removed from visual tree
            /// and detach themselves, but adding them back to tree does not make them reattach.
            /// This method remembers all objects that were detached and not reattached until returned object
            /// is disposed and detaches them on dispose.
            /// </remarks>
            /// <returns></returns>
            public static IDisposable GuardDetachingBehaviors()
            {
                return new AttachmentTracker();
            }

            private class AttachmentTracker : IDisposable
            {
                private readonly HashSet<AttachableObject> objectsTodetach = new();
                private readonly PropertyMetadata metadata;
                private readonly PropertyChangedCallback originalChangeCallback;
                private bool isDisposed;

                public AttachmentTracker()
                {
                    Type type = typeof(AttachableObject);
                    FieldInfo field = type.GetField("AttachmentProperty", BindingFlags.Static | BindingFlags.NonPublic);
                    if (field == null)
                    {
                        MelonLogger.Msg("AttachmentProperty=null");
                    }

                    DependencyProperty attachmentProperty = field.GetValue(null) as DependencyProperty;
                    if (attachmentProperty == null)
                    {
                        isDisposed = true;
                        return;
                    }

                    metadata = attachmentProperty.GetMetadata(typeof(AttachableObject));
                    originalChangeCallback = metadata.PropertyChangedCallback;
                    metadata.PropertyChangedCallback = OnAttachmentChanged;
                }

                public void Dispose()
                {
                    if (!isDisposed)
                    {
                        DetachObjects();
                        metadata.PropertyChangedCallback = originalChangeCallback;
                        isDisposed = true;
                    }
                }

                private void OnAttachmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    if ((Visibility)e.NewValue == (Visibility)(-1))
                    {
                        if (d is AttachableObject obj)
                            objectsTodetach.Add(obj);
                    }
                    else
                    {
                        if (d is AttachableObject obj)
                            objectsTodetach.Remove(obj);
                    }
                }

                private void DetachObjects()
                {
                    foreach (AttachableObject item in objectsTodetach)
                        item.Detach();
                }
            }
        }
    }
}