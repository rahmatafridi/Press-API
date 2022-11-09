using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using PressDot.Service.Infrastructure;

namespace PressDot.Service.Implementation
{
    public class MessagingService : IMessagingService
    {
        private readonly FirebaseMessaging messaging;

        public MessagingService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("key.json")
                        .CreateScoped("https://www.googleapis.com/auth/firebase.messaging")
                });
            }
            messaging = FirebaseMessaging.GetMessaging(FirebaseApp.DefaultInstance);
            
        }

        private Message CreateNotification(string title, string notificationBody, string token, int userId)
        {
            var data = new Dictionary<string, string>();
                data.Add("UserId", userId.ToString());
                data.Add("Body",notificationBody);
                data.Add("Title",title);
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                },
                Data = data
            };
        }

        public async Task SendNotification(string token, string title, string body,int userId)
        {
            var result = await messaging.SendAsync(CreateNotification(title, body, token,userId));
            //do something with result
        }
    }
}
