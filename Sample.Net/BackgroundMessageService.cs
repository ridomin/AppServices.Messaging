﻿using AppServices.Messaging.Core;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace AppServices.Messaging.Sample.Net
{
    public class BackgroundMessageService : BaseMessageService
    {
        protected override string ORIGINATOR_ID => "background";
        protected override string APP_SERVICE_NAME => "com.messaging.appservices";
        protected override string PACKAGE_FAMILY_NAME => "b752f9a1-ff97-45e6-ae35-8d32c1f2df3c_80c4904e66sn0";

        public static readonly BackgroundMessageService Instance = new BackgroundMessageService();

        private BackgroundMessageService() { }

        /// <summary>
        /// Send a request and wait for a response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async override Task<ValueSet> SendMessageForResponseAsync(ValueSet message)
        {
            // Update the message data if necessary
            //message["key"] = "value";

            // Call the base to send the updated message
            return await base.SendMessageForResponseAsync(message);
        }

        /// <summary>
        /// Respond to an incoming message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var message = args.Request.Message;
            var response = new ValueSet()
            {
                ["request"] = message,
                ["response"] = "I received your message. Thanks."
            };

            await SendMessageForResponseAsync(response);
        }
    }
}
