using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.ServiceModel;

namespace WIMF_serveur_app_console
{
    class Program
    {

        static void Main(string[] args)
        {
            UriTemplate template = new UriTemplate("{rubrique}/articles/{langage}?sujet={nomsujet}");
            Console.WriteLine("Coucou");
            Uri prefix = new Uri("HTTP://developpez.com");
            Uri fullUri = new Uri("HTTP://developpez.com/dotnet/articles/csharp?sujet=WCF");

            UriTemplateMatch results = template.Match(prefix, fullUri);

            if (results != null)
            {
                foreach (string variableName in results.BoundVariables.Keys)
                {
                    Console.WriteLine("{0}: {1}", variableName, results.BoundVariables[variableName]);
                }
            }

            
        Console.Read();
        }
    }
}
