using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WIMF_REST_Service
{
    [DataContract(Namespace = "HTTP://schemas.developpez.com/BoxOfficeREST/2008/03", Name = "Film")]
    public class Film
    {



        //TEST PUSH
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public String Titre { get; set; }

        [DataMember(Order = 3)]
        public int Annee { get; set; } //année de sortie

        [DataMember(Order = 4)]
        public double Entrees { get; set; }  //nombre d'entrées en millions

        [DataMember(Order = 5)]
        public Uri Uri { get; set; }
    }
}
