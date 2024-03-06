using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MelonLoader;
using Noesis;

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

        public static void dumpObject(object obj)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                MelonLogger.Msg($"{descriptor.Name}={descriptor.GetValue(obj)}");
                // Console.WriteLine("{0}={1}", name, value);
            }
        }

        public static bool iterateNoesisElement(DependencyObject element, Func<DependencyObject, int, bool> action)
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
                    if (iterateNoesisElement(VisualTreeHelper.GetChild(element, i), action))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }

            return true;
        }

        public static void iterateNoesisElement(DependencyObject element, Action<DependencyObject, int> action,
            int depth = 0)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            if (childrenCount == 0)
            {
                action(element, depth);
                return;
            }

            action(element, depth);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                iterateNoesisElement(child, action, depth + 1);
                // MelonLogger.Msg(child);
            }
        }

        public static StringBuilder iterateNoesisElement(DependencyObject element)
        {
            StringBuilder stringBuilder = new();
            iterateNoesisElement(element,
                (o, i) => stringBuilder.Append(
                    $"{string.Concat(Enumerable.Repeat(" ", i))}{o}{Environment.NewLine}"));
            return stringBuilder;
        }

        public static void dumpNoesisElement(DependencyObject element)
        {
            MelonLogger.Msg(iterateNoesisElement(element).ToString());
        }

        public static void dumpNoesisElementToFile(DependencyObject element, string filename)
        {
            StringBuilder stringBuilder = iterateNoesisElement(element);
            StreamWriter sr = File.CreateText(filename);
            sr.Write(stringBuilder.ToString());
            sr.Close();
        }

        public static void iterateResources(FrameworkElement frameworkElement)
        {
            MelonLogger.Msg(frameworkElement.Resources);
            foreach (var resourcesKey in frameworkElement.Resources.Keys)
            {
                MelonLogger.Msg($"{resourcesKey} {frameworkElement.Resources[resourcesKey]}");
            }
        }
    }
}