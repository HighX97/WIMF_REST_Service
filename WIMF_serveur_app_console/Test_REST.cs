using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using WIMF_serveur_app_console;

namespace WIMF_server_app_console
{
    class Test_REST
    {
        [OperationContract]
        [WebGet(UriTemplate = "films/{filmId}", 
            ResponseFormat = WebMessageFormat.Xml, 
            BodyStyle = WebMessageBodyStyle.Bare)]
        Film GetFilm(string filmId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "films/{filmId}")]
        void DeleteFilm(string filmId);
    }
}
