using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WIMF_ClassLibrary;

namespace WIMF_REST_Service
{
    [ServiceContract]
    public interface IWIMF_REST_service_imp
    {
        ListeFilms Films { get; set; }


        //WIMF
        //WIMF  /utilisateur


        [OperationContract]
        [WebInvoke(Method = "GET",
        UriTemplate = "utilisateur/all",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare)]
        List<Utilisateur> GetAllUtilisateur();

        [OperationContract]
        [WebInvoke(Method = "GET",
        UriTemplate = "utilisateur/amis/{utilisateurId}",
        RequestFormat = WebMessageFormat.Xml)]
        List<Ami> GetUser_amis(string utilisateurId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        UriTemplate = "utilisateur/messages/{utilisateurId}",
        ResponseFormat = WebMessageFormat.Xml)]
        List<Message> GetUser_messages(string utilisateurId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        UriTemplate = "utilisateur/{utilisateurId}",
        ResponseFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare)]
        Utilisateur GetUtilisateur(string utilisateurId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
        UriTemplate = "utilisateur/{utilisateurId}",
        RequestFormat = WebMessageFormat.Xml)]
        Utilisateur UpdateUtilisateur(string utilisateurId,
        Utilisateur updatedUtilisateur);

        [OperationContract]
        [WebInvoke(Method = "POST",
        UriTemplate = "utilisateur",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml)]
        void CreateUtilisateur(Utilisateur newUtilisateur);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
        UriTemplate = "utilisateur/{utilisateur_id}")]
        void DeleteUtilisateur(string utilisateur_id);

        //WIMF  /messages

        [OperationContract]
        [WebGet(UriTemplate = "messages/{messageId}",
        ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        Message GetMessage(string messageId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "messages", ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml)]
        void CreateMessage(Message newMessage);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "messages/{messageId}", RequestFormat = WebMessageFormat.Xml)]
        void UpdateMessage(string messageId, Message updatedMessage);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "messages/{messageId}")]
        void DeleteMessage(string messageId);






        //Example
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "xml/{id}")]
        string XMLData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/{id}")]
        string JSONData(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "auth")]
        ResponseData Auth(RequestData rData);

        [OperationContract]
        [WebGet(UriTemplate = "aide")]
        Stream GetIndex();

        [OperationContract]
        [WebGet(UriTemplate = "films", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        ListeFilms GetFilms();

        [OperationContract]
        [WebGet(UriTemplate = "films/{filmId}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        Film GetFilm(string filmId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "films", ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml)]
        void CreateFilm(Film newFilm);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "films/{filmId}", RequestFormat = WebMessageFormat.Xml)]
        void UpdateFilm(string filmId, Film updatedFilm);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "films/{filmId}")]
        void DeleteFilm(string filmId);

    }

    [DataContract (Namespace = "http://www.eysnap.com/mPlayer")]
    public class RequestData
    {
        [DataMember]
        public string details { get; set; }
    }

    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Age { get; set; }

        [DataMember]
        public string Exp { get; set; }

        [DataMember]
        public string Technology { get; set; }
    }
}
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IWIMF_REST_service_imp" à la fois dans le code et le fichier de configuration.
