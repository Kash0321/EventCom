using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using System.Threading.Tasks;

namespace Dotnetters.EventCom
{
    /// <summary>
    /// ViewModel base para la implementación del patrón MVVM
    /// </summary>
    public class BaseViewModel : PropertyChangeNotifier
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        /// <summary>
        /// Inicializa una instancia de <see cref="BaseViewModel"/>
        /// </summary>
        /// <param name="viewTitle">Título de la vista que se mostrará al usuario</param>
        public BaseViewModel(string viewTitle = "")
        {
            Title = viewTitle;
        }

        #region Gestión de estados


        bool isBusy;

        /// <summary>
        /// Flag para controlar si el <see cref="BaseViewModel">ViewModel</see> ya está ejecutado alguna otra operación
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    SetProperty(ref isBusy, value);

                    if (isBusy)
                    {
                        Task.Delay(500).ContinueWith((t) =>
                        {
                            //Si isBusy no ha cambiado lanzamos el evento del cambio
                            if (isBusy == value)
                            {
                                OnPropertyChanged("IsBusyDelayed");
                            }
                        });
                    }
                    else
                    {
                        OnPropertyChanged("IsBusyDelayed");
                    }
                }
            }
        }

        /// <summary>
        /// Flag para controlar si el <see cref="BaseViewModel">ViewModel</see> ya está ejecutado alguna otra operación con un retraso de varios milisegundos
        /// </summary>
        public bool IsBusyDelayed
        {
            get { return isBusy; }
        }

        #endregion

        #region Literales de la pantalla

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #endregion
    }
}
