using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WIMF_ClassLibrary;

namespace WIMF_REST_Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class WIMF_REST_service_imp : IWIMF_REST_service_imp
    {
        private List<Film> listeFilms;

        private List<Film> ListeFilms
        {
            get
            {
                if (listeFilms == null) //si la liste est null on la crée
                {
                    //HTTP://fr.wikipedia.org/wiki/Box-office_français
                    listeFilms = new List<Film> {
                new Film { Id = 1, Titre = "Titanic", Annee = 1997, Entrees = 20.64},
                new Film { Id = 2, Titre = "La Grande Vadrouille", Annee = 1968, Entrees = 17.27},
                //etc.
            };

                    //on crée l'uri pour accéder à la ressource
                    foreach (Film film in listeFilms)
                    {
                        film.Uri = new System.Uri(
                        System.ServiceModel.OperationContext.Current.EndpointDispatcher.EndpointAddress.Uri.ToString()
                         + "/films/" + film.Id.ToString());
                    }
                }
                return listeFilms;
            }
        }

        string IWIMF_REST_service_imp.XMLData(string id)
        {
            return "You request product " + id;
        }

        string IWIMF_REST_service_imp.JSONData(string id)
        {
            return "You request product " + id;
        }

        public ResponseData Auth(RequestData rData)
        {
            // Call BLL here
            var data = rData.details.Split('|');
            var response = new ResponseData
            {
                Name = data[0],
                Age = data[1],
                Exp = data[2],
                Technology = data[3]
            };
            return response;
        }

        ResponseData IWIMF_REST_service_imp.Auth(RequestData rData)
        {
            throw new NotImplementedException();
        }

        Stream IWIMF_REST_service_imp.GetIndex()
        {
            MemoryStream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
            writer.Write("Coucou tuto REST C#");
            writer.Flush();

            stream.Position = 0;
            //on indique le type de contenu
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return stream;
        }

        //liste des paramètres autorisés dans l'URI ListeFilms
        private HashSet<String> paramsAutorisesGetFilms;

        public HashSet<String> ParamsAutorisesGetFilms
        {
            get
            {
                if (paramsAutorisesGetFilms == null)
                {
                    paramsAutorisesGetFilms = new HashSet<String>(new String[] { "skip", "take" },
                        StringComparer.InvariantCultureIgnoreCase);
                }
                return paramsAutorisesGetFilms;
            }
        }

        //renvoie true sur la liste des paramètres autorisés est un sur-ensemble de la liste des paramètres reçus
        //HTTP://badger.developpez.com/tutoriels/dotnet/hashset/
        private bool VerifierParamsGetfilms()
        {
            WebOperationContext context = WebOperationContext.Current;
            UriTemplateMatch uriMatch = context.IncomingRequest.UriTemplateMatch;
            return ParamsAutorisesGetFilms.IsSupersetOf(uriMatch.QueryParameters.AllKeys);
        }

        //affecte à "value" la valeur du paramètre "name" et renvoie true si réussite
        private bool TryGetIntFromQueryString(string name, out int value)
        {
            value = 0;
            WebOperationContext context = WebOperationContext.Current;
            UriTemplateMatch uriMatch = context.IncomingRequest.UriTemplateMatch;
            string strValue = uriMatch.QueryParameters[name];

            if (!String.IsNullOrEmpty(strValue))
            {
                return Int32.TryParse(strValue, out value);
            }
            return false;
        }

        ListeFilms Films
        {
            get
            {
                //on vérifie la validité des noms des paramètres
                if (!VerifierParamsGetfilms())
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return null;
                }

                int skipInt;
                int takeInt;
                //s'il n'y a pas de paramètre "skip"
                if (!TryGetIntFromQueryString("skip", out skipInt))
                {
                    skipInt = 0; //valeur par défaut
                }
                //s'il n'y a pas de paramètre "take"
                if (!TryGetIntFromQueryString("take", out takeInt))
                {
                    takeInt = ListeFilms.Count - skipInt; //valeur par défaut
                }

                //on vérifie la validité des valeurs des paramètres
                if ((skipInt >= 0) && (skipInt < ListeFilms.Count) && (takeInt > 0) && (takeInt <= ListeFilms.Count)
                 && (skipInt + takeInt <= ListeFilms.Count))
                {
                    ListeFilms lf = new ListeFilms();
                    lf.Films = ListeFilms.Skip<Film>(skipInt).Take<Film>(takeInt).OrderByDescending(f => f.Entrees).ToList();
                    lf.Start = skipInt + 1;
                    lf.TotalCount = ListeFilms.Count;
                    lf.TakeCount = lf.Films.Count;

                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    return lf;
                }
                else //mauvaise requête
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return null;
                }
            }
        }

        ListeFilms IWIMF_REST_service_imp.Films
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        Film IWIMF_REST_service_imp.GetFilm(string filmId)
        {
                int filmIdInt;
                Film film = null;

                if (!int.TryParse(filmId, out filmIdInt)) //si filmId n'est pas un int
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return null;
                }

                film = ListeFilms.FirstOrDefault(f => f.Id == filmIdInt);

                if (film == null) //si le film n'existe pas
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return null;
                }

                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return film;
        }


        Message IWIMF_REST_service_imp.GetMessage(string messageId)
        {
            int messageIdInt;
            Message_db db_msg = new Message_db();
            Message message = null;

            if (!int.TryParse(messageId, out messageIdInt)) //si messageId n'est pas un int
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            message = db_msg.getMessage(messageIdInt);

            if (message == null) //si le message n'existe pas
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return null;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return message;
        }

        Utilisateur IWIMF_REST_service_imp.GetUtilisateur(string utilisateurId)
        {
            int utilisateurIdInt;
            Utilisateur_db db_u = new Utilisateur_db();
            Utilisateur utilisateur = null;

            if (!int.TryParse(utilisateurId, out utilisateurIdInt)) //si utilisateurId n'est pas un int
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            utilisateur = db_u.getUtilisateur(utilisateurIdInt);

            if (utilisateur == null) //si le utilisateur n'existe pas
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return null;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return utilisateur;
        }

        void IWIMF_REST_service_imp.CreateFilm(Film newFilm)
        {
            if (newFilm != null)
            {
                //calcul du nouvel identifiant
                newFilm.Id = ListeFilms.Max(f => f.Id) + 1;

                System.UriTemplateMatch match = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
                System.UriTemplate template = new System.UriTemplate("/films/{id}");
                newFilm.Uri = template.BindByPosition(match.BaseUri, newFilm.Id.ToString());

                ListeFilms.Add(newFilm);

                //on affecte la valeur du champ d'entête HTTP "Location" avec l'uri du film créé.
                WebOperationContext.Current.OutgoingResponse.SetStatusAsCreated(newFilm.Uri);
            }
            else //erreur dans la requête
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }

        void IWIMF_REST_service_imp.UpdateFilm(string filmId, Film updatedFilm)
        {
            int filmIdInt;
            Film film = null;

            //on parse l'identifiant envoyé en entier
            if (int.TryParse(filmId, out filmIdInt))
            {
                //on recherche le film à modifier
                film = ListeFilms.FirstOrDefault(f => f.Id == filmIdInt);
                //s'il existe on le modifie et on renvoie le statut Created
                if (film != null)
                {
                    film.Titre = updatedFilm.Titre;
                    film.Annee = updatedFilm.Annee;
                    film.Entrees = updatedFilm.Entrees;
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else //sinon on envoie une erreur
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else  //si l'identifiant n'est pas un entier
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }

        void IWIMF_REST_service_imp.DeleteFilm(string filmId)
        {
            int filmIdInt;
            Film film = null;

            //on parse l'identifiant envoyé en entier
            if (int.TryParse(filmId, out filmIdInt))
            {
                //on recherche le film à supprimer
                film = ListeFilms.FirstOrDefault(f => f.Id == filmIdInt);
                //s'il existe on le supprime et on renvoie le statut OK
                if (film != null)
                {
                    ListeFilms.Remove(film);
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else //sinon on envoie une erreur
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else //si l'identifiant n'est pas un entier
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }

        ListeFilms IWIMF_REST_service_imp.GetFilms()
        {

            //on vérifie la validité des noms des paramètres
            if (!VerifierParamsGetfilms())
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            int skipInt;
            int takeInt;
            //s'il n'y a pas de paramètre "skip"
            if (!TryGetIntFromQueryString("skip", out skipInt))
            {
                skipInt = 0; //valeur par défaut
            }
            //s'il n'y a pas de paramètre "take"
            if (!TryGetIntFromQueryString("take", out takeInt))
            {
                takeInt = ListeFilms.Count - skipInt; //valeur par défaut
            }

            //on vérifie la validité des valeurs des paramètres
            if ((skipInt >= 0) && (skipInt < ListeFilms.Count) && (takeInt > 0) && (takeInt <= ListeFilms.Count)
             && (skipInt + takeInt <= ListeFilms.Count))
            {
                ListeFilms lf = new ListeFilms();
                lf.Films = ListeFilms.Skip<Film>(skipInt).Take<Film>(takeInt).OrderByDescending(f => f.Entrees).ToList();
                lf.Start = skipInt + 1;
                lf.TotalCount = ListeFilms.Count;
                lf.TakeCount = lf.Films.Count;

                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return lf;
            }
            else //mauvaise requête
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }
        }

        void IWIMF_REST_service_imp.CreateMessage(Message newMessage)
        {
            throw new NotImplementedException();
        }


        void IWIMF_REST_service_imp.DeleteMessage(string messageId)
        {
            throw new NotImplementedException();
        }

        List<Message> IWIMF_REST_service_imp.GetUser_messages(string utilisateurId)
        {
            int utilisateurIdInt;
            Message_db db_msg = new Message_db();
            List<Message> user_messages = null;

            if (!int.TryParse(utilisateurId, out utilisateurIdInt)) //si idUser n'est pas un int
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            user_messages = db_msg.getUser_messages(utilisateurIdInt);

            if (user_messages == null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return null;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return user_messages;
        }

        List<Utilisateur> IWIMF_REST_service_imp.GetAllUtilisateur()
        {
            Utilisateur_db db_u = new Utilisateur_db();
            List<Utilisateur> utilisateurs = null;

            utilisateurs = db_u.getUtilisateur_all();

            if (utilisateurs == null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return null;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return utilisateurs;
        }

        List<Ami> IWIMF_REST_service_imp.GetUser_amis(string utilisateurId)
        {
            int iutilisateurIdInt;
            Ami_db db_ami = new Ami_db();
            List<Ami> amis = null;

            if (!int.TryParse(utilisateurId, out iutilisateurIdInt)) //si utilisateurId n'est pas un int
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return null;
            }

            amis = db_ami.getUtilisateur_amis(iutilisateurIdInt);

            if (amis == null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return null;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return amis;
        }


        Utilisateur IWIMF_REST_service_imp.UpdateUtilisateur(string utilisateur_id, Utilisateur updatedUtilisateur)
        {
            int utilisateur_id_int;
            Utilisateur utilisateur = null;
            //on parse l'identifiant envoyé en entier
            if (int.TryParse(utilisateur_id, out utilisateur_id_int))
            {
                Utilisateur_db db_u = new Utilisateur_db();
                utilisateur = db_u.getUtilisateur(utilisateur_id_int);
                //s'il existe on le modifie et on renvoie le statut Created
                if (utilisateur != null)
                {
                    string where = "idU = " + utilisateur_id;
                    List<string> columns_values = new List<string>();
                    if (updatedUtilisateur.Nom != null)
                    {
                        columns_values.Add("nom = '" + updatedUtilisateur.Nom+"'");
                    }
                    if (updatedUtilisateur.Tel != null)
                    {
                        columns_values.Add("tel = '" + updatedUtilisateur.Tel + "'");
                    }
                    if (updatedUtilisateur.Gps != null || updatedUtilisateur.Gps.Length > 0)
                    {
                        columns_values.Add("gps ='" + updatedUtilisateur.Gps + "'");
                    }
                    if (updatedUtilisateur.Password != null)
                    {
                        columns_values.Add("password = '" + updatedUtilisateur.Password + "'");
                    }
                    if (updatedUtilisateur.DateMaj != null)
                    {
                        columns_values.Add("dateMaj = CURRENT_TIMESTAMP");
                    }
                    db_u.Update(columns_values, where);
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    utilisateur =  db_u.getUtilisateur(utilisateur_id_int);
                }
                else //sinon on envoie une erreur
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else  //si l'identifiant n'est pas un entier
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return utilisateur;
        }

        void IWIMF_REST_service_imp.CreateUtilisateur(Utilisateur newUtilisateur)
        {
            if (newUtilisateur != null)
            {
                Utilisateur_db db_u = new Utilisateur_db();
                List<string> columns = new List<string>();
                columns.Add("nom");
                columns.Add("password");
                columns.Add("tel");
                columns.Add("dateMaj");

                List<string> values = new List<string>();
                values.Add("'"+newUtilisateur.Nom + "'");
                values.Add("'" + (newUtilisateur.Password + "'"));
                values.Add(("'" + newUtilisateur.Tel + "'"));
                values.Add("CURRENT_TIMESTAMP");

                db_u.Insert(columns, values);
            }
            else //erreur dans la requête
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }




        void IWIMF_REST_service_imp.DeleteUtilisateur(string utilisateur_id)
        {
            int utilisateur_id_int;
            Utilisateur utilisateur = null;
            //on parse l'identifiant envoyé en entier
            if (int.TryParse(utilisateur_id, out utilisateur_id_int))
            {
                Utilisateur_db db_u = new Utilisateur_db();
                utilisateur = db_u.getUtilisateur(utilisateur_id_int);
                //s'il existe on le modifie et on renvoie le statut Created
                if (utilisateur != null)
                {
                    string where = "idU = " + utilisateur_id;
                    db_u.Delete(where);
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else //sinon on envoie une erreur
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else  //si l'identifiant n'est pas un entier
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
        }

        void IWIMF_REST_service_imp.UpdateMessage(string messageId, Message updatedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
 // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "WIMF_REST_service_imp" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce servicwe, sélectionnez WIMF_REST_service_imp.svc ou WIMF_REST_service_imp.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
