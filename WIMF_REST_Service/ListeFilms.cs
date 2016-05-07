using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WIMF_REST_Service
{
    /// Représente une séquence de films
    [DataContract(Namespace = "HTTP://schemas.developpez.com/BoxOfficeREST/2008/03", Name = "ListeFilms")]
    public class ListeFilms
    {
        /// Nombre total de films
        [DataMember(Order = 1)]
        public int TotalCount { get; set; }

        /// Numéro à partir duquel commence la séquence
        [DataMember(Order = 2)]
        public int Start { get; set; }

        /// Nombre de films dans la séquence
        [DataMember(Order = 3)]
        public int TakeCount { get; set; }

        private List<Film> films = null;

        [DataMember(Order = 4)]
        public List<Film> Films
        {
            get
            {
                if (films == null)
                {
                    films = new List<Film>();
                }
                return films;
            }
            set
            {
                films = value;
            }
        }


    }
}