using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dotnetters.EventCom
{
    /// <summary>
    /// Implementa la lógica básica de notificación de cambio de propiedades
    /// </summary>
    /// <remarks>
    /// Hemos segmentado esto con respecto al <see cref="BaseViewModel"/>, para permitir la 
    /// anidación de elementos en un <see cref="BaseViewModel">ViewModel</see> complejo
    /// </remarks>
    public abstract class PropertyChangeNotifier : INotifyPropertyChanged
    {
        /// <summary>
        /// Evento al que se suscriben las vistas mediante su mecanismo de bindeo, para actualizar la presentación
        /// en función de los cambios que se producen en el <see cref="ViewModelBase">ViewModel</see>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Debe invocarse desde las clases derivadas, para procurar la notificación de cambios en los datos
        /// de un <see cref="ViewModelBase">ViewModel</see> hacia la Vista
        /// </summary>
        /// <param name="name">Nombre de la propiedad que ha cambiado</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Establece el valor de una propiedad que notifica dicho cambio a los objetos que están escuchando el evento <see cref="PropertyChanged"/>
        /// </summary>
        /// <typeparam name="T">Tipo de dato que almacena la propiedad</typeparam>
        /// <param name="backingStore">Campo o variable miembro que sirve para almacenar el dato en memoria</param>
        /// <param name="value">Nuevo valor a establecer en la propiedad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="onChanged">Callback que se ejecutará respondiendo al cambio de la propiedad</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
