using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HCS;
using HCS.BaseTypes;
using HCS.Framework;
using HCS.Framework.Core;
using HCS.Framework.Implement;
using HCS.Helpers;
using HCS.Globals;
using HCS.Service.Async.HouseManagement.v11_10_0_13;
using HCS.Framework.Interfaces;

namespace HCS.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // выбераем сертификат
            var cert = GetCert();
            if (cert == null)
                return;

            Console.WriteLine("Укажите pin ЭЦП");
            string pin = string.Empty;//Console.ReadLine();

            var config = new ClientConfig {
                UseTunnel = false,
                IsPPAK = false,
                CertificateThumbprint = cert.Item2,
                OrgPPAGUID = "b14c8b87-6d0d-4854-a97c-74d34e1a8ca1",
                OrgEntityGUID = "c3ffd8b6-cda3-4eb5-9696-30fee607c8b3",
                Role = Globals.OrganizationRole.UK
            };

            // иницализируем менеджер конечных точек
            ServicePointConfig.InitConfig(cert.Item2, pin, cert.Item1);

            var story = new MessageStory();
            var broker = new MessageBroker(config,story);
           

            broker.AddHanbler(typeof(exportAccountResultType), ExportAccountResultHandler);
            broker.AddHanbler(typeof(exportHouseResultType), ExportHouseResultHandler);

            

            var request = new exportHouseDataRequest {
                RequestHeader = RequestHelper.Create<RequestHeader>(config.OrgPPAGUID, config.Role),
                exportHouseRequest = new exportHouseRequest {
                    Id = Constants.SignElementId,
                    FIASHouseGuid = "7263796e-1d5a-4535-8def-93315e8975db",
                }
            };
            var request2 = new exportAccountDataRequest {
                RequestHeader = RequestHelper.Create<RequestHeader>(config.OrgPPAGUID, config.Role),
                exportAccountRequest = new exportAccountRequest {
                    Id = Constants.SignElementId,
                    FIASHouseGuid = "7263796e-1d5a-4535-8def-93315e8975db",
                }
            };



            Console.WriteLine("Добавляем сообщения в очередь");
            for (int i = 0 ; i < 10; i++) {
                request.RequestHeader.MessageGUID = Guid.NewGuid().ToString().ToLower();
                broker.CreateMessage(request, EndPoints.HouseManagementAsync);
            }

            //broker.CreateMessage(request, EndPoints.HouseManagementAsync);
            broker.CreateMessage(request2, EndPoints.HouseManagementAsync);


            broker.SendMessage();
            Console.WriteLine("Отправлено");


            broker.CheckResult();

            Console.WriteLine("Обработка");
            broker.Process();

            Console.ReadKey();
        }

        static Tuple<int, string> GetCert()
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try {
                store.Open(OpenFlags.ReadOnly);
                var collection = store.Certificates
                    .OfType<X509Certificate2>()
                    .Where(x => x.HasPrivateKey && x.IsGostPrivateKey());
                var cert = X509Certificate2UI.SelectFromCollection(new X509Certificate2Collection(collection.ToArray()), "Выбор сертификата", "", X509SelectionFlag.SingleSelection)[0];
                Console.WriteLine($"Криптопровайдер:{cert.GetProviderType().Item2}");
                return new Tuple<int, string>(cert.GetProviderType().Item1, cert.Thumbprint);
            }
            catch {
                return null;
            }
            finally {
                store.Close();
            }
        }


        static void ExportAccountResultHandler(IEnumerable<object> items,IMessageType message)
        {
            Console.WriteLine($"Получен ответ для сообщения:{message.MessageGUID}");
            foreach (var item in items.OfType<exportAccountResultType>().Take(10)) {
                 
                Console.WriteLine(item.AccountGUID);
            }
        }

        static void ExportHouseResultHandler(IEnumerable<object> items,IMessageType message)
        {
            Console.WriteLine($"Получен ответ для сообщения:{message.MessageGUID}");
            foreach (var item in items.OfType<exportHouseResultType>()) {
                Console.WriteLine(item.HouseUniqueNumber);
            }
        }

      
    }
}
