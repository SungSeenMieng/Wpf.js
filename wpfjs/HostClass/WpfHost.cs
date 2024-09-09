using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using wpfjs;

namespace wpfjs.HostClass
{
    public class WpfHost
    {
        public object createViewModel(dynamic obj)
        {
            var vm = new ViewModel<dynamic>(obj);
            obj.raisePropertyChanged = new Action<string>(vm.RaisePropertyChanged);
            return vm;
        }
        public Binding getBinding(string path)
        {
            Binding binding = new Binding()
            {
                Path = new PropertyPath(path)
            };
            return binding;
        }
        public Binding getBinding(string path, object source)
        {
            Binding binding = new Binding()
            {
                Path = new PropertyPath(path),
                Source = source
            };
            return binding;
        }
        public bool setBinding(FrameworkElement obj, string propertyName, string path)
        {
            Binding binding = new Binding()
            {
                Path = new PropertyPath(path)
            };
            if (!propertyName.EndsWith("Property"))
            {
                propertyName = $"{propertyName}Property";
            }
            Type type = obj.GetType();
            FieldInfo field = null;
            do
            {
                field = type.GetField(propertyName);
                if (field != null) break;
                type = type.BaseType;
            } while (type != typeof(FrameworkElement));
            if (field == null) return false;
            if (field.GetValue(null) is DependencyProperty dp)
            {
                obj.SetBinding(dp, binding);
                return true;
            }
            return false;
        }
        public bool setBinding(FrameworkElement obj, string propertyName, string path, object source)
        {
            Binding binding = new Binding()
            {
                Path = new PropertyPath(path),
                Source = source
            };
            if (!propertyName.EndsWith("Property"))
            {
                propertyName = $"{propertyName}Property";
            }
            Type type = obj.GetType();
            FieldInfo field = null;
            do
            {
                field = type.GetField(propertyName);
                if (field != null) break;
                type = type.BaseType;
            } while (type != typeof(FrameworkElement));
            if (field == null) return false;
            if (field.GetValue(null) is DependencyProperty dp)
            {
                obj.SetBinding(dp, binding);
                return true;
            }
            return false;
        }
        public RelayCommand getCommand(object execute)
        {
            Action action = Program.Host.proc(0, execute) as Action;
            return new RelayCommand(action);
        }
        public RelayCommand getCommand(object execute, object canExecute)
        {
            Action action = Program.Host.proc(0, execute) as Action;
            Func<bool> canFunc = Program.Host.func<bool>(0, canExecute) as Func<bool>;
            return new RelayCommand(action, canFunc);
        }
        public RelayCommand<object?> getParamCommand(object execute)
        {
            Action<object?> action = Program.Host.proc(1, execute) as Action<object?>;
            return new RelayCommand<object?>(action);
        }
        public RelayCommand<object?> getParamCommand(object execute, object canExecute)
        {
            Action<object?> action = Program.Host.proc(1, execute) as Action<object?>;
            Func<object?, bool> canFunc = Program.Host.func<bool>(1, canExecute) as Func<object?, bool>;
            Predicate<object?> predicate = new Predicate<object?>(canFunc);
            return new RelayCommand<object?>(action, predicate);
        }
    }
}
