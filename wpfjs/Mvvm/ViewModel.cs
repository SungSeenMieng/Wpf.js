using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpfjs.Mvvm
{
    public class ViewModel<T> : DynamicObject, INotifyPropertyChanged
    {
        public T Data { get; private set; }
        public ViewModel(T data)
        {
            this.Data = data;
        }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }
        public virtual void OnSeriesListAssistantSelected()
        {

        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <param name="onChanged">Action that is called after the property value has been changed.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.GetDynamicMemberNames();
            }
            return base.GetDynamicMemberNames();
        }
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryGetMember(binder, out result);
            }
            return base.TryGetMember(binder, out result);
        }
        public override DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            return base.GetMetaObject(parameter);
        }
        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryBinaryOperation(binder, arg, out result);
            }
            return base.TryBinaryOperation(binder, arg, out result);
        }
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryGetIndex(binder, indexes, out result);
            }
            return base.TryGetIndex(binder, indexes, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TrySetMember(binder, value);
            }
            return base.TrySetMember(binder, value);
        }
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TrySetIndex(binder, indexes, value);
            }
            return base.TrySetIndex(binder, indexes, value);
        }
        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryDeleteMember(binder);
            }
            return base.TryDeleteMember(binder);
        }
        public override bool TryDeleteIndex(DeleteIndexBinder binder, object[] indexes)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryDeleteIndex(binder, indexes);
            }
            return base.TryDeleteIndex(binder, indexes);
        }
        public override bool TryInvoke(InvokeBinder binder, object?[]? args, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryInvoke(binder, args, out result);
            }
            return base.TryInvoke(binder, args, out result);
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryInvokeMember(binder, args, out result);
            }
            return base.TryInvokeMember(binder, args, out result);
        }
        public override bool TryUnaryOperation(UnaryOperationBinder binder, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryUnaryOperation(binder, out result);
            }
            return base.TryUnaryOperation(binder, out result);
        }
        public override bool TryConvert(ConvertBinder binder, out object? result)
        {
            if (this.Data is DynamicObject dynamicObject)
            {
                return dynamicObject.TryConvert(binder, out result);
            }
            return base.TryConvert(binder, out result);
        }
        public override bool TryCreateInstance(CreateInstanceBinder binder, object?[]? args, [NotNullWhen(true)] out object? result)
        {
            return base.TryCreateInstance(binder, args, out result);
        }
    }
}
